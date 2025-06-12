
using DataContext.Interfaces.Management;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repositories.Management
{
    public class ProjectTaskEstimationRepository : IProjectTaskEstimationRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;
        public ProjectTaskEstimationRepository(IDbContextFactory<ClouseauContext> context)
        {
            _contextFactory = context;
        }

        public async Task<List<ProjectTaskEstimation>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            return await _context.ProjectTaskEstimations.
                Include(x => x.ProjectTask)
                .Include(x=> x.Role).
                ToListAsync();
        }

        public async Task EditAsync(ProjectTaskEstimation taskEstimation)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            _context.ProjectTaskEstimations.Update(taskEstimation);

            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(ProjectTaskEstimation taskEstimation)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            await _context.ProjectTaskEstimations.AddAsync(taskEstimation);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteRolesAsync(long projectTaskId, long roleId)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var taskEstimation = await _context.ProjectTaskEstimations
                .FirstOrDefaultAsync(x => x.ProjectTaskId == projectTaskId && x.RoleId == roleId);

            if (taskEstimation != null)
            {
                _context.ProjectTaskEstimations.Remove(taskEstimation);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("La asignación de rol no fue encontrada.");
            }
        }
    }
}
