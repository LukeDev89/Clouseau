using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IInventoryPartsRepository
    {
        Task<List<InventoryPart>> GetAsync();

        Task<List<User>> GetUsersWithPcRepairAsync();
        Task<List<Inventory>> GetSerialAsync();
        Task AddAsync(InventoryPart part);

        Task EditAsync(InventoryPart part);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long inventoryPartId);
    }
}
