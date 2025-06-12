using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces.Management
{
    public interface IInventoryPartsService
    {
        Task<List<InventoryPart>> GetAsync(); 
        Task<List<User>> GetUsersWithPcRepairAsync();
        Task<List<Inventory>> GetSerialAsync();
        Task AddAsync(InventoryPart inventory);

        Task EditAsync(InventoryPart inventory);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long inventoryPartId);
    }
}
