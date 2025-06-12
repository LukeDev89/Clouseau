using DataContext.Interfaces.Management;
using DataModel.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repositories.Management
{
    public class SoftwareRepository : ISoftwareRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;

        public SoftwareRepository(IDbContextFactory<ClouseauContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<InventorySoftware>> GetAsync()
        { 
            using var _context = _contextFactory.CreateDbContext();

            return await _context.InventorySoftwares
                      
            .ToListAsync();
        }

        public async Task AddAsync(InventorySoftware inventory)
        {
            using var _context = _contextFactory.CreateDbContext();           

            await _context.InventorySoftwares.AddAsync(inventory);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(InventorySoftware inventory)
        {
            using var _context = _contextFactory.CreateDbContext();

            var ToUpdate = await _context.InventorySoftwares.FirstAsync(x => x.Id == inventory.Id);

            await _context.SaveChangesAsync();
        }

        public async Task DefinitiveDeleteAsync(long softwareId)  
        {
            using var _context = _contextFactory.CreateDbContext();

            var software = await _context.InventorySoftwares.FirstAsync(x => x.Id == softwareId);

            _context.InventorySoftwares.Remove(software);

            await _context.SaveChangesAsync();
        }

    }
}
