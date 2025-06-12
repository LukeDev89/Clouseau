using DataContext.Interfaces.Management;
using DataContext;
using DataService.Interfaces.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Services.Management
{
    public class InventoryLicenseService : IInventoryLicenseService
    {
        private readonly IInventoryLicenseRepository _repository;

        public InventoryLicenseService(IInventoryLicenseRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InventoryLicense>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(InventoryLicense inventory) => await _repository.AddAsync(inventory);

        public async Task EditAsync(InventoryLicense inventory) => await _repository.EditAsync(inventory);

        public async Task DeleteAsync(long id) => await _repository.DeleteAsync(id);

        public async Task DefinitiveDeleteAsync(long inventoryLicenseId) => await _repository.DefinitiveDeleteAsync(inventoryLicenseId);
    }
}
