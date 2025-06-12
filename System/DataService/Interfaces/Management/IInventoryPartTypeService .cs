using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces.Management
{
    public interface IInventoryPartTypeService
    {
        Task<List<InventoryPartType>> GetAsync();

        Task AddAsync(InventoryPartType inventoryPartType);

        Task EditAsync(InventoryPartType inventoryPartType);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long inventoryPartTypeId);
    }
}
