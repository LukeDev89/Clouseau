

using DataContext;

namespace DataService.Interfaces.Management
{
    public interface IRoleService
    {
        Task<List<Role>> GetAsync();

        Task AddAsync(Role role);

        Task EditAsync(Role role);

        Task DeleteAsync(long id);
    }
}
