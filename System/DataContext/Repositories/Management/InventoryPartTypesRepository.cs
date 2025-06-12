using DataContext.Interfaces.Management;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Repositories.Management
{
    public class InventoryPartTypeRepository : IInventoryPartTypeRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;

        public InventoryPartTypeRepository(IDbContextFactory<ClouseauContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<InventoryPartType>> GetAsync()
        {
            using var _context = _contextFactory.CreateDbContext();

            return await _context.InventoryPartTypes
            .Include(x => x.InventoryParts)
            .ToListAsync();
        }

        public async Task AddAsync(InventoryPartType partTypes)
        {
            using var _context = _contextFactory.CreateDbContext();

            await _context.InventoryPartTypes.AddAsync(partTypes);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(InventoryPartType partTypes)
        {
            using var _context = _contextFactory.CreateDbContext();

            var PartTypesToUpdate = await _context.InventoryPartTypes.FirstAsync(x => x.Id == partTypes.Id);

            PartTypesToUpdate.Name = partTypes.Name;
            

            _context.InventoryPartTypes.Update(PartTypesToUpdate);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            using var _context = _contextFactory.CreateDbContext();

            var PartToUpdate = await _context.InventoryPartTypes.FirstAsync(x => x.Id == id);

            

            _context.InventoryPartTypes.Update(PartToUpdate);

            await _context.SaveChangesAsync();
        }

        public async Task DefinitiveDeleteAsync(long inventoryPartTypesId)
        {
            using var _context = _contextFactory.CreateDbContext();

            var partTypes = await _context.InventoryPartTypes.FirstAsync(x => x.Id == inventoryPartTypesId);

            _context.InventoryPartTypes.Remove(partTypes);

            await _context.SaveChangesAsync();
        }
    }
}
