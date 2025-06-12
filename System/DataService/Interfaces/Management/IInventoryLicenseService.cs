using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces.Management
{
    public interface IInventoryLicenseService
    {
        Task<List<InventoryLicense>> GetAsync();

        Task AddAsync(InventoryLicense inventory);

        Task EditAsync(InventoryLicense inventory);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long inventoryLicenseId);
    }
}
