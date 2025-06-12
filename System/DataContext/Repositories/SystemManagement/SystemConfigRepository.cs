using DataContext.Interfaces.Management;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repositories.Management
{
    public class SystemConfigRepository : ISystemConfigRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;

        public SystemConfigRepository(IDbContextFactory<ClouseauContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<SystemConfig>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            return await _context.SystemConfigs.ToListAsync();
        }

        public async Task<SystemConfig> GetByKeyAsync(string key)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            return await _context.SystemConfigs.FirstAsync(x => x.Name == key);
        }

        public async Task AddAsync(SystemConfig config)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            await _context.SystemConfigs.AddAsync(config);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(SystemConfig config)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var conf = await _context.SystemConfigs.FirstAsync(x => x.Id == config.Id);

            conf.Name = config.Name;
            conf.Description = config.Description;
            conf.DataType = config.DataType;
            conf.PreviousValue = conf.DataValue;
            conf.DataValue = config.DataValue;

            _context.SystemConfigs.Update(conf);

            await _context.SaveChangesAsync();
        }
    }
}
