using DataContext.Interfaces.Management;
using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService.Interfaces.Management;

namespace DataService.Services.Management
{
    public class AddLicenseTypeService : IAddLicenseTypeService
    {
        private readonly IAddLicenseTypeRepository _repository;

        public AddLicenseTypeService(IAddLicenseTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InventoryLicenseType>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(InventoryLicenseType inventory) => await _repository.AddAsync(inventory);

        public async Task EditAsync(InventoryLicenseType inventory) => await _repository.EditAsync(inventory);

        public async Task DefinitiveDeleteAsync(long licenseTypeId) => await _repository.DefinitiveDeleteAsync(licenseTypeId);

        public async Task<List<InventoryLicenseType>> GetInventoryLicenseTypes() => await _repository.GetInventoryLicenseTypes();

    }
}
