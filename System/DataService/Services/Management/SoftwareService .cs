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
    public class SoftwareService : ISoftwareService
    {
        private readonly ISoftwareRepository _repository;

        public SoftwareService(ISoftwareRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InventorySoftware>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(InventorySoftware inventory) => await _repository.AddAsync(inventory);

        public async Task EditAsync(InventorySoftware inventory) => await _repository.EditAsync(inventory);

        public async Task DefinitiveDeleteAsync(long modelId) => await _repository.DefinitiveDeleteAsync(modelId); 

    }
}
