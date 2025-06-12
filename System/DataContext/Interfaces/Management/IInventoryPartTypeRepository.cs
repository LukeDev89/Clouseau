using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IInventoryPartTypeRepository
    {
        Task<List<InventoryPartType>> GetAsync();

        Task AddAsync(InventoryPartType partType);

        Task EditAsync(InventoryPartType partType);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long inventoryPartTypeId);
    }
}
