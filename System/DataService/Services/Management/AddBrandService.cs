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
    public class AddBrandService : IAddBrandService
    {
        private readonly IAddBrandRepository _repository;

        public AddBrandService(IAddBrandRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InventoryBrand>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(InventoryBrand inventory) => await _repository.AddAsync(inventory);

        public async Task EditAsync(InventoryBrand inventory) => await _repository.EditAsync(inventory);

        public async Task DefinitiveDeleteAsync(long brandId) => await _repository.DefinitiveDeleteAsync(brandId); 

        public async Task<List<InventoryBrand>> GetBrands() => await _repository.GetBrands();
        
    }
}
