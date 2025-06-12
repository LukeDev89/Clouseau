

namespace DataContext.Interfaces.Management
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAsync();

        Task AddAsync(Client client);

        Task EditAsync(Client client);

        Task DeleteAsync(long id);
    }
}
