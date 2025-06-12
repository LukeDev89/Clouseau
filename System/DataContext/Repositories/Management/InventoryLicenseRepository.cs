using DataContext.Interfaces.Management;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repositories.Management
{
    public class InventoryLicenseRepository : IInventoryLicenseRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;

        public InventoryLicenseRepository(IDbContextFactory<ClouseauContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<InventoryLicense>> GetAsync()
        {
            using var _context = _contextFactory.CreateDbContext();

            return await _context.InventoryLicenses
            .Include(x => x.LicenseType)
            .Include(x => x.User)
            .AsSplitQuery()
            .ToListAsync();
        }

        public async Task AddAsync(InventoryLicense license)
        {
            using var _context = _contextFactory.CreateDbContext();

            await _context.InventoryLicenses.AddAsync(license);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(InventoryLicense license)
        {
            using var _context = _contextFactory.CreateDbContext();

           
            var licenseToUpdate = await _context.InventoryLicenses
                .FirstOrDefaultAsync(x => x.Id == license.Id);

            if (licenseToUpdate != null)
            {
                
                licenseToUpdate.LicenseTypeId = license.LicenseTypeId;

                
                licenseToUpdate.Active = license.Active;
                licenseToUpdate.Description = license.Description;
                licenseToUpdate.Name = license.Name;
                licenseToUpdate.Serial = license.Serial;
                licenseToUpdate.RequestDate = license.RequestDate;
                licenseToUpdate.RequestFinished = license.RequestFinished;

                await _context.SaveChangesAsync();
            }
            
        }

        public async Task DeleteAsync(long id)
        {
            using var _context = _contextFactory.CreateDbContext();

            var licenseToUpdate = await _context.InventoryLicenses.FirstAsync(x => x.Id == id);

            licenseToUpdate.Active = false;

            _context.InventoryLicenses.Update(licenseToUpdate);

            await _context.SaveChangesAsync();
        }

        public async Task DefinitiveDeleteAsync(long inventoryLicenseId)
        {
            using var _context = _contextFactory.CreateDbContext();

            var license = await _context.InventoryLicenses.FirstAsync(x => x.Id == inventoryLicenseId);

            _context.InventoryLicenses.Remove(license);

            await _context.SaveChangesAsync();
        }
    }
}
