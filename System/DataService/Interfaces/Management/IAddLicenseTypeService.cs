using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces.Management
{
    public interface IAddLicenseTypeService
    {
        Task<List<InventoryLicenseType>> GetAsync();

        Task AddAsync(InventoryLicenseType inventory);

        Task EditAsync(InventoryLicenseType inventory);

        Task DefinitiveDeleteAsync(long licenseTypeId);

        Task<List<InventoryLicenseType>> GetInventoryLicenseTypes();

    }
}