using DataContext.Interfaces.Management;
using DataModel.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repositories.Management
{
    public class AddBrandRepository : IAddBrandRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;

        public AddBrandRepository(IDbContextFactory<ClouseauContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<InventoryBrand>> GetAsync()
        { 
            using var _context = _contextFactory.CreateDbContext();

            return await _context.InventoryBrands
            .ToListAsync();
        }

        public async Task AddAsync(InventoryBrand inventory)
        {
            using var _context = _contextFactory.CreateDbContext();          

            await _context.InventoryBrands.AddAsync(inventory);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(InventoryBrand inventory)
        {
            using var _context = _contextFactory.CreateDbContext();

            var ToUpdate = await _context.InventoryBrands.FirstAsync(x => x.Id == inventory.Id);
            ToUpdate.Name = inventory.Name;
                     

            await _context.SaveChangesAsync();
        }       

        public async Task DefinitiveDeleteAsync(long brandId)  
        {
            using var _context = _contextFactory.CreateDbContext();

            var brand = await _context.InventoryBrands.FirstAsync(x => x.Id == brandId);

            _context.InventoryBrands.Remove(brand);

            await _context.SaveChangesAsync();
        }

        public async Task<List<InventoryBrand>> GetBrands()
        {
            using var _context = _contextFactory.CreateDbContext();

            return await _context.InventoryBrands.ToListAsync();
        }
    }
}
