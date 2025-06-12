
using DataContext.Interfaces.Management;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repositories.Management
{
    public class TaskTypeRepository : ITaskTypeRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;
        public TaskTypeRepository(IDbContextFactory<ClouseauContext> context)
        {
            _contextFactory = context;
        }

        public async Task<List<TaskType>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            return await _context.TaskTypes.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task DeleteAsync(long id)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var tt = await _context.TaskTypes.FirstAsync(x => x.Id == id);
            var ttUndefined = (await _context.TaskTypes.FirstAsync(x => x.Name == "Indefinida")).Id;
            var tasks = await _context.TaskProgresses.Where(x => x.TaskTypeId == id).ToListAsync();

            foreach (var task in tasks)
            {
                task.TaskTypeId = ttUndefined;
                _context.TaskProgresses.Update(task);
            }

            _context.TaskTypes.Remove(tt);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(TaskType taskType)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            //var tt = await _context.TaskTypes.FirstAsync(x => x.Id == id);
            
            //tt.Name = name;
            //tt.Description = description;

            _context.TaskTypes.Update(taskType);

            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(TaskType taskType)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            await _context.TaskTypes.AddAsync(taskType);

            await _context.SaveChangesAsync();
        }
    }
}
