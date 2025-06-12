
using DataContext;
using DataContext.Interfaces.Management;
using DataService.Interfaces.Management;

namespace DataService.Services.Management
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Client>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(Client client) => await _repository.AddAsync(client);

        public async Task EditAsync(Client client) => await _repository.EditAsync(client);

        public async Task DeleteAsync(long id) => await _repository.DeleteAsync(id);
    }
}
