using DataContext;

namespace DataService.Interfaces.Management
{
    public interface IClientService
    {
        Task<List<Client>> GetAsync();

        Task AddAsync(Client client);

        Task EditAsync(Client client);

        Task DeleteAsync(long id);
    }
}
