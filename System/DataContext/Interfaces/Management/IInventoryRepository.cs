using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IInventoryRepository
    {
        Task<List<Inventory>> GetAsync();

        Task AddAsync(Inventory inventory);
		Task AddAsyncMultiple(Inventory inventory);
		Task AddAsync(InventoryUser inventory);
        Task AddAsync(InventoryBrand inventory);

        Task EditAsync(Inventory inventory);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long inventoryId);
		Task DeleteUsersAsync(long inventoryUserId, long userId);
		Task<List<InventoryBrand>> GetBrands();
        Task<List<InventoryModel>> GetInventoryModels();
        Task<List<InventoryUser>> GetInventoryUsers();
        Task<List<InventoryHistory>> GetInventoryHistoryByInventoryId(long id);
        Task<List<User>> GetUsers();


		Task AddUsersTeamAsync(List<InventoryUser> users);

		Task AddUsersAsync(long inventoryId, long userId);
        Task<List<User>> GetUsersWithoutNotebook();
	}
}
