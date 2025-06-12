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
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;

        public InventoryService(IInventoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Inventory>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(Inventory inventory) => await _repository.AddAsync(inventory);
		public async Task AddAsyncMultiple(Inventory inventory) => await _repository.AddAsyncMultiple(inventory);
		public async Task AddAsync(InventoryUser inventory) => await _repository.AddAsync(inventory);
        public async Task AddAsync(InventoryBrand inventory) => await _repository.AddAsync(inventory);

        public async Task EditAsync(Inventory inventory) => await _repository.EditAsync(inventory);

        public async Task DeleteAsync(long id) => await _repository.DeleteAsync(id);

        public async Task DefinitiveDeleteAsync(long inventoryId) => await _repository.DefinitiveDeleteAsync(inventoryId);

		public async Task DeleteUsersAsync(long inventoryUserId, long userId) => await _repository.DeleteUsersAsync(inventoryUserId, userId);

		public async Task<List<InventoryBrand>> GetBrands() => await _repository.GetBrands();

        public async Task<List<InventoryModel>> GetInventoryModels() => await _repository.GetInventoryModels();

        public async Task<List<InventoryUser>> GetInventoryUsers() => await _repository.GetInventoryUsers();

        public async Task<List<InventoryHistory>> GetInventoryHistoryByInventoryId(long id) => await _repository.GetInventoryHistoryByInventoryId(id);

        public async Task<List<User>> GetUsers() => await _repository.GetUsers();

		public async Task AddUsersTeamAsync(List<InventoryUser> users) => await _repository.AddUsersTeamAsync(users);

		public async Task AddUsersAsync(long inventoryId, long userId) => await _repository.AddUsersAsync(inventoryId, userId);

		public async Task<List<User>> GetUsersWithoutNotebook() => await _repository.GetUsersWithoutNotebook();
	}
}
