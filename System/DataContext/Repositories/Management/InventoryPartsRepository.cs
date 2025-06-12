using DataContext.Interfaces.Management;
using DataModel.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Repositories.Management
{
    public class InventoryPartsRepository : IInventoryPartsRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;

        public InventoryPartsRepository(IDbContextFactory<ClouseauContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<InventoryPart>> GetAsync()
        {
            using var _context = _contextFactory.CreateDbContext();

            return await _context.InventoryParts
            .Include(x => x.PartType)
            .ToListAsync();
        }

        public async Task<List<User>> GetUsersWithPcRepairAsync()
        {
            using var _context = _contextFactory.CreateDbContext();

            var userIds = await _context.InventoryUsers
                .Where(iu => _context.Inventories.Any(i => i.Id == iu.InventoryId && i.State == (short)InventoryStatus.Reparacion)).Select(iu => iu.UserId).Distinct()
                .ToListAsync();

            return await _context.Users
                .Where(u => userIds.Contains(u.Id)).ToListAsync();
        }

        public async Task<List<Inventory>> GetSerialAsync()
        {
            using var _context = _contextFactory.CreateDbContext();

            return await _context.Inventories
                .Include(i => i.Model)
                    .ThenInclude(m => m.Brand)
                .Where(i => i.State == (short)InventoryStatus.Reparacion)
                .ToListAsync();
        }

        public async Task AddAsync(InventoryPart Part)
        {
            using var _context = _contextFactory.CreateDbContext();

            await _context.InventoryParts.AddAsync(Part);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(InventoryPart part)
        {
            using var _context = _contextFactory.CreateDbContext();

            
            var PartToUpdate = await _context.InventoryParts.FirstOrDefaultAsync(x => x.Id == part.Id);

            PartToUpdate.Name = part.Name;
            PartToUpdate.Description = part.Description;
            PartToUpdate.Serial = part.Serial;
            PartToUpdate.PartTypeId = part.PartTypeId;
            PartToUpdate.Active = part.Active;
            

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            using var _context = _contextFactory.CreateDbContext();

            var PartToUpdate = await _context.InventoryParts.FirstAsync(x => x.Id == id);

            PartToUpdate.Active = false;

            _context.InventoryParts.Update(PartToUpdate);

            await _context.SaveChangesAsync();
        }

        public async Task DefinitiveDeleteAsync(long inventoryPartId)
        {
            using var _context = _contextFactory.CreateDbContext();

            var part = await _context.InventoryParts.FirstAsync(x => x.Id == inventoryPartId);

            _context.InventoryParts.Remove(part);

            await _context.SaveChangesAsync();
        }
    }
}
