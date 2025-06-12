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
    public class AddModelService : IAddModelService
    {
        private readonly IAddModelRepository _repository;

        public AddModelService(IAddModelRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InventoryModel>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(InventoryModel inventory) => await _repository.AddAsync(inventory);

        public async Task EditAsync(InventoryModel inventory) => await _repository.EditAsync(inventory);

        public async Task DefinitiveDeleteAsync(long modelId) => await _repository.DefinitiveDeleteAsync(modelId); 

        public async Task<List<InventoryBrand>> GetBrands() => await _repository.GetBrands();

        public async Task<List<InventoryModel>> GetModels() => await _repository.GetModels();
    }
}
