using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IInventoryLicenseRepository
    {
        Task<List<InventoryLicense>> GetAsync();

        Task AddAsync(InventoryLicense license);

        Task EditAsync(InventoryLicense license);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long inventoryLicenseId);
    }
}
