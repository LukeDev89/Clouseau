using DataContext.Interfaces.Management;
using DataModel.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repositories.Management
{
    public class AddLicenseTypeRepository : IAddLicenseTypeRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;

        public AddLicenseTypeRepository(IDbContextFactory<ClouseauContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<InventoryLicenseType>> GetAsync()
        {
            using var _context = _contextFactory.CreateDbContext();

            return await _context.InventoryLicenseTypes
            .ToListAsync();
        }

        public async Task AddAsync(InventoryLicenseType inventory)
        {
            using var _context = _contextFactory.CreateDbContext();

            await _context.InventoryLicenseTypes.AddAsync(inventory);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(InventoryLicenseType inventory)
        {
            using var _context = _contextFactory.CreateDbContext();

            var ToUpdate = await _context.InventoryLicenseTypes.FirstAsync(x => x.Id == inventory.Id);
            ToUpdate.Name = inventory.Name;


            await _context.SaveChangesAsync();
        }

        public async Task DefinitiveDeleteAsync(long licenseTypeId)
        {
            using var _context = _contextFactory.CreateDbContext();

            var licenseType = await _context.InventoryLicenseTypes.FirstAsync(x => x.Id == licenseTypeId);

            _context.InventoryLicenseTypes.Remove(licenseType);

            await _context.SaveChangesAsync();
        }

        public async Task<List<InventoryLicenseType>> GetInventoryLicenseTypes()
        {
            using var _context = _contextFactory.CreateDbContext();

            return await _context.InventoryLicenseTypes.ToListAsync();
        }
    }
}