using DataContext.Interfaces.Management;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repositories.Management
{
    public class LicenseTypeRepository : ILicenseTypeRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;
        public LicenseTypeRepository(IDbContextFactory<ClouseauContext> context)
        {
            _contextFactory = context;
        }

        public async Task<List<LicenseType>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            return await _context.LicenseTypes.ToListAsync();
        }

        public async Task AddAsync(LicenseType license)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            await _context.LicenseTypes.AddAsync(license);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(LicenseType license)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var licenseType = await _context.LicenseTypes.FirstAsync(x => x.Id == license.Id);

            licenseType.Name = license.Name;
            licenseType.HelperMessage = license.HelperMessage;
            licenseType.ConsecutiveDays = license.ConsecutiveDays;
            licenseType.FileRequired = license.FileRequired;
            licenseType.MaxDaysPerYear = license.MaxDaysPerYear;

            _context.Update(licenseType);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var licenseType = await _context.LicenseTypes.FirstAsync(x => x.Id == id);

            _context.Remove(licenseType);

            await _context.SaveChangesAsync();
        }
    }
}
