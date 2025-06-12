using DataContext.Interfaces.Management;
using DataModel.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repositories.Management
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;

        public InventoryRepository(IDbContextFactory<ClouseauContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Inventory>> GetAsync() // Fallaba acá
        {           

            using var _context = _contextFactory.CreateDbContext();

            var lista = new List<Inventory>();

            try
            {
                lista = await _context.Inventories
                    .Include(x => x.Model)
                        .ThenInclude(x => x.Brand)
                    .Include(x => x.InventorySoftwares)
                        .ThenInclude(x => x.InventoryLicense)
                    .Include(x => x.InventoryHistories)
                        .ThenInclude(x => x.InventoryPart)
                    .Include(x => x.InventoryHistories)
                        .ThenInclude(x => x.InventoryLicense)
                    .Include(x => x.InventoryUsers)
                        .ThenInclude(x => x.User)
                    .Include(x => x.InventoryComments)
                    .ToListAsync();

                var historial = await _context.InventoryHistories.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lista;
        }

        public async Task AddAsync(Inventory inventory)
        {
            using var _context = _contextFactory.CreateDbContext();

            inventory.StartDate = DateTime.Now;

            await _context.Inventories.AddAsync(inventory);

            await _context.SaveChangesAsync();
        }

        public async Task AddAsyncMultiple(Inventory inventory)
        {
            using var _context = _contextFactory.CreateDbContext();

            inventory.StartDate = DateTime.Now;

            await _context.Inventories.AddAsync(inventory);

            await _context.SaveChangesAsync();
        }
        public async Task AddAsync(InventoryUser inventory)
        {
            using var _context = _contextFactory.CreateDbContext();

            inventory.DateFrom = DateTime.Now;

            await _context.InventoryUsers.AddAsync(inventory);

            await _context.SaveChangesAsync();
        }
        public async Task AddAsync(InventoryBrand inventory)
        {
            using var _context = _contextFactory.CreateDbContext();

            await _context.InventoryBrands.AddAsync(inventory);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Inventory inventory)
        {
            using var _context = _contextFactory.CreateDbContext();

            var ToUpdate = await _context.Inventories.FirstAsync(x => x.Id == inventory.Id);


            var observations = new List<string>();


            if (ToUpdate.Serial != inventory.Serial)
            {
                observations.Add($"- Se cambia el serial a {inventory.Serial}");
                ToUpdate.Serial = inventory.Serial;
            }

            if (ToUpdate.Processor != inventory.Processor)
            {
                observations.Add($"- Se cambia el procesador a {inventory.Processor}");
                ToUpdate.Processor = inventory.Processor;
            }

            if (ToUpdate.Username != inventory.Username)
            {
                observations.Add($"- Se cambia el usuario a {inventory.Username}");
                ToUpdate.Username = inventory.Username;
            }

            if (ToUpdate.Password != inventory.Password)
            {
                observations.Add("- Se actualiza la contraseña");
                ToUpdate.Password = inventory.Password;
            }

            if (ToUpdate.Domain != inventory.Domain)
            {
                observations.Add($"- Se cambia el dominio a {inventory.Domain}");
                ToUpdate.Domain = inventory.Domain;
            }

            if (ToUpdate.Memory != inventory.Memory)
            {
                observations.Add($"- Se cambia la memoria a {inventory.Memory}");
                ToUpdate.Memory = inventory.Memory;
            }

            if (ToUpdate.Storage != inventory.Storage)
            {
                observations.Add($"- Se cambia el almacenamiento a {inventory.Storage}");
                ToUpdate.Storage = inventory.Storage;
            }

            if (ToUpdate.ModelId != inventory.ModelId)
            {
                observations.Add($"- Se cambia el modelo a {inventory.ModelId}");
                ToUpdate.ModelId = inventory.ModelId;
            }


            if (ToUpdate.State != inventory.State)
            {
                var newState = (InventoryStatus)inventory.State;
                observations.Add($"- El estado de la máquina cambia a {newState}");
                ToUpdate.State = inventory.State;

                ToUpdate.EndDate = inventory.State == (short)InventoryStatus.Asignada ? (DateTime?)null :
                    inventory.State == (short)InventoryStatus.Disponible ? (DateTime?)null :
                    inventory.State == (short)InventoryStatus.Robada ? DateTime.Now :
                    inventory.State == (short)InventoryStatus.Reparacion ? DateTime.Now : DateTime.Now;
            }


            _context.Inventories.Update(ToUpdate);


            if (observations.Count > 0)
            {

                var observationText = string.Join("\n", observations);
                await _context.InventoryHistories.AddAsync(new InventoryHistory
                {
                    InventoryId = inventory.Id,
                    Observation = observationText,
                    Date = DateTime.Now
                });
            }


            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(long id)
        {
            using var _context = _contextFactory.CreateDbContext();

            var ToUpdate = await _context.Inventories.FirstAsync(x => x.Id == id);

            ToUpdate.State = (short)InventoryStatus.Inactiva;

            _context.Inventories.Update(ToUpdate);

            await _context.SaveChangesAsync();
        }

        public async Task DefinitiveDeleteAsync(long inventoryId)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var inventory = _context.Inventories.FirstAsync(x => x.Id == inventoryId);
            await inventory;

            var inventoryHistory = _context.InventoryHistories.Where(x => x.InventoryId == inventoryId).ToListAsync();
            await inventoryHistory;

            var inventoryUser = _context.InventoryUsers.Where(x => x.InventoryId == inventoryId).ToListAsync();
            await inventoryUser;

            

            if (inventoryHistory.Result != null)
            {
                _context.InventoryHistories.RemoveRange(inventoryHistory.Result);
                await _context.SaveChangesAsync();
            }

            if (inventoryUser.Result != null)
            {
                _context.InventoryUsers.RemoveRange(inventoryUser.Result);
                await _context.SaveChangesAsync();
            }

           

            _context.Inventories.Remove(inventory.Result);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUsersAsync(long inventoryUserId, long userId)
        {
            using var _context = _contextFactory.CreateDbContext();


            var inventoryUser = await _context.InventoryUsers.FirstAsync(x => x.Id == inventoryUserId && x.UserId == userId);


            _context.InventoryUsers.Remove(inventoryUser);


            var inventory = await _context.Inventories.FirstAsync(x => x.Id == inventoryUser.InventoryId);
            inventory.State = (short)InventoryStatus.Disponible;

            var user = await _context.Users.FirstAsync(x => x.Id == userId);

            await _context.InventoryHistories.AddAsync(new InventoryHistory
            {
                InventoryId = inventoryUser.InventoryId,
                Observation = $"Se ha desasignado a {user.UserFullName} de la máquina.",
                Date = DateTime.Now
            });


            await _context.SaveChangesAsync();
        }

        public async Task<List<InventoryBrand>> GetBrands()
        {
            using var _context = _contextFactory.CreateDbContext();

            return await _context.InventoryBrands.ToListAsync();
        }

        public async Task<List<InventoryModel>> GetInventoryModels()
        {
            using var _context = _contextFactory.CreateDbContext();

            return await _context.InventoryModels.ToListAsync();
        }

        public async Task<List<InventoryUser>> GetInventoryUsers()
        {
            using var _context = _contextFactory.CreateDbContext();

            return await _context.InventoryUsers.ToListAsync();
        }

        public async Task<List<InventoryHistory>> GetInventoryHistoryByInventoryId(long id)
        {
            using var _context = _contextFactory.CreateDbContext();

            return await _context.InventoryHistories.Where(x => x.InventoryId == id).ToListAsync();
        }

        public async Task<List<User>> GetUsers()
        {
            using var _context = _contextFactory.CreateDbContext();

            return await _context.Users.ToListAsync();
        }

        public async Task<List<User>> GetUsersWithoutNotebook()
        {
            using var _context = _contextFactory.CreateDbContext();

            return await _context.Users.Where(user => !_context.InventoryUsers.Any(iu => iu.UserId == user.Id)).ToListAsync();
        }


        public async Task AddUsersTeamAsync(List<InventoryUser> users)
        {
            using var _context = _contextFactory.CreateDbContext();

            foreach (var user in users)
            {
                await _context.InventoryUsers.AddAsync(new InventoryUser()
                {
                    InventoryId = user.InventoryId,
                    UserId = user.Id,
                    DateFrom = DateTime.Now
                });

                await _context.InventoryHistories.AddAsync(new InventoryHistory()
                {
                    InventoryId = user.InventoryId,
                    Observation = $"Usuario asignado: {user.User.UserFullName}",
                    Date = DateTime.Now
                });

            }

            await _context.SaveChangesAsync();
        }

        public async Task AddUsersAsync(long inventoryId, long userId)
        {
            using var _context = _contextFactory.CreateDbContext();

            var existInInventory = await _context.InventoryUsers.AnyAsync(x => x.InventoryId == inventoryId && x.UserId == userId);

            if (existInInventory) return;

            var newInventoryUser = new InventoryUser()
            {
                InventoryId = inventoryId,
                UserId = userId,
                DateFrom = DateTime.Now
            };

            await _context.InventoryUsers.AddAsync(newInventoryUser);

            var inventory = await _context.Inventories.FirstAsync(x => x.Id == inventoryId);
            inventory.State = (short)InventoryStatus.Asignada;


            var user = await _context.Users.FirstAsync(x => x.Id == userId);

            await _context.InventoryHistories.AddAsync(new InventoryHistory()
            {
                InventoryId = inventoryId,
                Observation = $"Asignado usuario {user.UserFullName}.",
                Date = DateTime.Now
            });

            await _context.SaveChangesAsync();
        }
    }
}
