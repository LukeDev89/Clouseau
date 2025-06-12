

using DataContext;

namespace DataService.Interfaces.Management
{
    public interface IUserService
    {
        Task<List<User>> GetAsync();

        Task AddAsync(User user);

        Task EditAsync(User user);

        Task DeleteAsync(long id);

        Task<bool> ResetPassword(string username);

        Task<User?> GetUserByUsername(string username);

        Task<bool> ChangePassword(long id, string password);
    }
}
