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
    public class InventoryPartTypeService : IInventoryPartTypeService
    {
        private readonly IInventoryPartTypeRepository _repository;

        public InventoryPartTypeService(IInventoryPartTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InventoryPartType>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(InventoryPartType inventory) => await _repository.AddAsync(inventory);

        public async Task EditAsync(InventoryPartType inventory) => await _repository.EditAsync(inventory);

        public async Task DeleteAsync(long id) => await _repository.DeleteAsync(id);

        public async Task DefinitiveDeleteAsync(long inventoryPartTypeId) => await _repository.DefinitiveDeleteAsync(inventoryPartTypeId);
    }
}
