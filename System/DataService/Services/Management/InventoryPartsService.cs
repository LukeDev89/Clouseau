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
    public class InventoryPartsService : IInventoryPartsService
    {
        private readonly IInventoryPartsRepository _repository;

        public InventoryPartsService(IInventoryPartsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InventoryPart>> GetAsync() => await _repository.GetAsync();
        public async Task<List<User>> GetUsersWithPcRepairAsync() => await _repository.GetUsersWithPcRepairAsync();

        public async Task<List<Inventory>> GetSerialAsync() => await _repository.GetSerialAsync();
        public async Task AddAsync(InventoryPart inventory) => await _repository.AddAsync(inventory);

        public async Task EditAsync(InventoryPart inventory) => await _repository.EditAsync(inventory);

        public async Task DeleteAsync(long id) => await _repository.DeleteAsync(id);

        public async Task DefinitiveDeleteAsync(long inventoryPartId) => await _repository.DefinitiveDeleteAsync(inventoryPartId);
    }
}
