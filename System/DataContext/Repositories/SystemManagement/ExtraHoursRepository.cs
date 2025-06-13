using DataContext;
using DataContext.Interfaces.SystemManagement;
using DataModel.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataContext.Repositories.SystemManagement
{
    public class ExtraHoursRepository : IExtraHoursRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;

        public ExtraHoursRepository(IDbContextFactory<ClouseauContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<ExtraHours>> GetAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.ExtraHours
                .Include(x => x.User)
                .Include(x => x.Task)
                .Include(x => x.TaskType)
                .Include(x => x.ReviewedByUser)
                .OrderByDescending(x => x.Created)
                .ToListAsync();
        }

        public async Task<List<ExtraHours>> GetByUserIdAsync(long userId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.ExtraHours
                .Include(x => x.User)
                .Include(x => x.Task)
                .Include(x => x.TaskType)
                .Include(x => x.ReviewedByUser)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Created)
                .ToListAsync();
        }

        public async Task<List<ExtraHours>> GetByDateRangeAsync(DateTime from, DateTime to)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.ExtraHours
                .Include(x => x.User)
                .Include(x => x.Task)
                .Include(x => x.TaskType)
                .Include(x => x.ReviewedByUser)
                .Where(x => x.Date >= from && x.Date <= to)
                .OrderByDescending(x => x.Created)
                .ToListAsync();
        }

        public async Task<List<ExtraHours>> GetByStatusAsync(ExtraHoursStatus status)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.ExtraHours
                .Include(x => x.User)
                .Include(x => x.Task)
                .Include(x => x.TaskType)
                .Include(x => x.ReviewedByUser)
                .Where(x => x.Status == (short)status)
                .OrderByDescending(x => x.Created)
                .ToListAsync();
        }

        public async Task<List<ExtraHours>> GetPendingForSupervisorAsync(long supervisorId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.ExtraHours
                .Include(x => x.User)
                .Include(x => x.Task)
                .Include(x => x.TaskType)
                .Include(x => x.ReviewedByUser)
                .Where(x => x.Status == (short)ExtraHoursStatus.EnEvaluacion && x.User.Superior == supervisorId)
                .OrderByDescending(x => x.Created)
                .ToListAsync();
        }

        public async Task<ExtraHours?> GetByIdAsync(long id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.ExtraHours
                .Include(x => x.User)
                .Include(x => x.Task)
                .Include(x => x.TaskType)
                .Include(x => x.ReviewedByUser)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(ExtraHours extraHours)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            extraHours.Status = (short)ExtraHoursStatus.EnEvaluacion;
            extraHours.Created = DateTime.Now;
            await context.ExtraHours.AddAsync(extraHours);
            await context.SaveChangesAsync();
        }

        public async Task EditAsync(ExtraHours extraHours)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var existing = await context.ExtraHours.FirstAsync(x => x.Id == extraHours.Id);
            
            existing.Hours = extraHours.Hours;
            existing.Comment = extraHours.Comment;
            existing.Date = extraHours.Date;
            existing.TaskId = extraHours.TaskId;
            existing.TaskTypeId = extraHours.TaskTypeId;
            
            context.Update(existing);
            await context.SaveChangesAsync();
        }

        public async Task ApproveAsync(long id, long reviewedBy, string? reviewComment = null)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var extraHours = await context.ExtraHours.FirstAsync(x => x.Id == id);
            
            extraHours.Status = (short)ExtraHoursStatus.Aprobadas;
            extraHours.ReviewedBy = reviewedBy;
            extraHours.Reviewed = DateTime.Now;
            extraHours.ReviewComment = reviewComment;
            
            context.Update(extraHours);
            await context.SaveChangesAsync();
        }

        public async Task RejectAsync(long id, long reviewedBy, string? reviewComment = null)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var extraHours = await context.ExtraHours.FirstAsync(x => x.Id == id);
            
            extraHours.Status = (short)ExtraHoursStatus.Rechazadas;
            extraHours.ReviewedBy = reviewedBy;
            extraHours.Reviewed = DateTime.Now;
            extraHours.ReviewComment = reviewComment;
            
            context.Update(extraHours);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var extraHours = await context.ExtraHours.FirstAsync(x => x.Id == id);
            
            context.ExtraHours.Remove(extraHours);
            await context.SaveChangesAsync();
        }
    }
}