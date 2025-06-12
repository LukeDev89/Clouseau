

namespace DataContext.Interfaces.Management
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAsync();

        Task AddAsync(Role role);

        Task EditAsync(Role role);

        Task DeleteAsync(long id);
    }
}
