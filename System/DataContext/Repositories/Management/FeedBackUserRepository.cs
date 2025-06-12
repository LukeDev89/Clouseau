using DataContext.Interfaces.Management;
using DataModel.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Repositories.Management
{
    public class FeedBackUserRepository : IFeedBackUserRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;

        public FeedBackUserRepository(IDbContextFactory<ClouseauContext> context, IConfiguration configuration)
        {
            _contextFactory = context;
        }

        public async Task<List<FeedbackUser>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            var prueba = await _context.FeedbackUsers
                .Include(x => x.User)
                .Include(x => x.FeedbackComments).ThenInclude(x => x.User)
                .Include(x => x.FeedbackImprovementUsers).ThenInclude(x => x.FeedbackImprovement)
                .Include(x => x.FeedbackPeriods).ThenInclude(x => x.FeedbackStandarType)
                .Include(x => x.FeedbackPeriods).ThenInclude(x => x.FeedbackItem).ThenInclude(x => x.FeedbackStandar)
                .AsSplitQuery()
                .ToListAsync();
            return prueba;
        }

        public async Task<List<FeedbackUser>> GetByUserIdAsync(long userId)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var user = await _context.Users
                .Include(x => x.UserTeams)
                .FirstAsync(x => x.Id == userId);

            if (user.ProfileId == (long)ProfileTypes.Estandar)
            {
                return await _context.FeedbackUsers
                .Include(x => x.User)
                .Include(x => x.FeedbackComments).ThenInclude(x => x.User)
                .Include(x => x.FeedbackImprovementUsers).ThenInclude(x => x.FeedbackImprovement)
                .Include(x => x.FeedbackPeriods).ThenInclude(x => x.FeedbackStandarType)
                .Include(x => x.FeedbackPeriods).ThenInclude(x => x.FeedbackItem).ThenInclude(x => x.FeedbackStandar)
                .Where(x => x.UserId == userId)
                .ToListAsync();
            }

            else if (user.ProfileId == (long)ProfileTypes.GestorProyectos)
            {
                var loggedInUserTeams = user.UserTeams
                    .Where(ut => ut.TeamId != 2) // No contempla Factory (teamId=2)
                    .Select(ut => ut.TeamId)
                    .ToList();
               
                var usersInTeams = await _context.Users
                    .Where(u => u.UserTeams.Any(ut => loggedInUserTeams.Contains(ut.TeamId))
                                && u.ProfileId != (long)ProfileTypes.GestorProyectos // Excluye otros Gestores
                                && u.ProfileId != (long)ProfileTypes.Administrador) // Excluye Admins
                    .Select(x => x.Id)
                    .ToListAsync();
              
                var usersWithSuperior = await _context.Users
                    .Where(u => u.Superior == user.Id) // Filtra por los que tienen al usuario logueado como superior
                    .Select(x => x.Id)
                    .ToListAsync();

                var allRelevantUsers = usersInTeams  
                    .Union(usersWithSuperior)
                    .Distinct()
                    .ToList();
              
                if (!allRelevantUsers.Contains(user.Id))
                {
                    allRelevantUsers.Add(user.Id);
                }

                return await _context.FeedbackUsers
                    .Include(x => x.User)
                    .Include(x => x.FeedbackComments).ThenInclude(x => x.User)
                    .Include(x => x.FeedbackImprovementUsers).ThenInclude(x => x.FeedbackImprovement)
                    .Include(x => x.FeedbackPeriods).ThenInclude(x => x.FeedbackStandarType)
                    .Include(x => x.FeedbackPeriods).ThenInclude(x => x.FeedbackItem).ThenInclude(x => x.FeedbackStandar)
                    .Where(x => allRelevantUsers.Contains(x.UserId))
                    .ToListAsync();
            }

            else
            {
                return await _context.FeedbackUsers
                .Include(x => x.User)
                .Include(x => x.FeedbackComments).ThenInclude(x => x.User)
                .Include(x => x.FeedbackImprovementUsers).ThenInclude(x => x.FeedbackImprovement)
                .Include(x => x.FeedbackPeriods).ThenInclude(x => x.FeedbackStandarType)
                .Include(x => x.FeedbackPeriods).ThenInclude(x => x.FeedbackItem).ThenInclude(x => x.FeedbackStandar)
                .ToListAsync();
            }
        }

        public async Task AddAsync(FeedbackUser user)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            await _context.FeedbackUsers.AddAsync(user);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(FeedbackUser user)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            _context.Update(user);

            await _context.SaveChangesAsync(); // get grid
        }

        public async Task DeleteAsync(long id)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var standar = await _context.FeedbackUsers.FirstAsync(x => x.Id == id);

            standar.Active = false;

            _context.FeedbackUsers.Update(standar);

            await _context.SaveChangesAsync();
        }

        public async Task DefinitiveDeleteAsync(long userId)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var fuser = _context.FeedbackUsers.FirstAsync(x => x.Id == userId);
            await fuser;

            var fcomment = _context.FeedbackComments.Where(x => x.FeedbackUserId == userId).ToListAsync();
            await fcomment;

            var fperiod = _context.FeedbackPeriods.Where(x => x.FeedbackUserId == userId).ToListAsync();
            await fperiod;

            var fiuser = _context.FeedbackImprovementUsers.Where(x => x.FeedbackUserId == userId).ToListAsync();
            await fiuser;

            if (fcomment.Result != null)
            {
                _context.FeedbackComments.RemoveRange(fcomment.Result);
                await _context.SaveChangesAsync();
            }

            if (fperiod.Result != null)
            {
                _context.FeedbackPeriods.RemoveRange(fperiod.Result);
                await _context.SaveChangesAsync();
            }

            if (fiuser.Result != null)
            {
                _context.FeedbackImprovementUsers.RemoveRange(fiuser.Result);
                await _context.SaveChangesAsync();
            }

            _context.FeedbackUsers.Remove(fuser.Result);
            await _context.SaveChangesAsync();
        }
    }
}
