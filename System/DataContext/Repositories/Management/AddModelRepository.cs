using DataContext.Interfaces.Management;
using DataModel.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repositories.Management
{
    public class AddModelRepository : IAddModelRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;

        public AddModelRepository(IDbContextFactory<ClouseauContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<InventoryModel>> GetAsync()
        { 
            using var _context = _contextFactory.CreateDbContext();

            return await _context.InventoryModels
            .Include(x => x.Brand)            
            .ToListAsync();
        }

        public async Task AddAsync(InventoryModel inventory)
        {
            using var _context = _contextFactory.CreateDbContext();           

            await _context.InventoryModels.AddAsync(inventory);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(InventoryModel inventory)
        {
            using var _context = _contextFactory.CreateDbContext();

            var ToUpdate = await _context.InventoryModels.FirstAsync(x => x.Id == inventory.Id);

            ToUpdate.Name = inventory.Name;            

            _context.InventoryModels.Update(ToUpdate);

            await _context.SaveChangesAsync();
        }

        public async Task DefinitiveDeleteAsync(long modelId)  
        {
            using var _context = _contextFactory.CreateDbContext();

            var model = await _context.InventoryModels.FirstAsync(x => x.Id == modelId);

            _context.InventoryModels.Remove(model);

            await _context.SaveChangesAsync();
        }

        public async Task<List<InventoryBrand>> GetBrands()
        {
            using var _context = _contextFactory.CreateDbContext();

            return await _context.InventoryBrands.ToListAsync();
        }

        public async Task<List<InventoryModel>> GetModels()
        {
            using var _context = _contextFactory.CreateDbContext();

            return await _context.InventoryModels.ToListAsync();
        }
    }
}
