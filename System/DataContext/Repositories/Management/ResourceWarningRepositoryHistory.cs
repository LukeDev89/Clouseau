using DataContext.Interfaces.Management;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Repositories.Management
{
    public class ResourceWarningHistoryRepository : IResourceWarningHistoryRepository
    {

        private readonly IDbContextFactory<ClouseauContext> _contextFactory;
        private readonly string RecoveryToken;

        public ResourceWarningHistoryRepository(IDbContextFactory<ClouseauContext> context, IConfiguration configuration)
        {
            _contextFactory = context;

            RecoveryToken = configuration.GetSection("Tokens:RecoveryToken").Value;
        }

        public async Task<List<ResourceWarningHistory>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            return await _context.ResourceWarningHistories.ToListAsync();
        }

        public async Task AddAsync(ResourceWarningHistory resourceWarning)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            await _context.ResourceWarningHistories.AddAsync(resourceWarning);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(ResourceWarningHistory resourceWarning)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            _context.Update(resourceWarning);

            await _context.SaveChangesAsync();
        }
    }
}
