

namespace DataContext.Interfaces.Management
{
    public interface IUserRepository
    {
        Task<List<User>> GetAsync();

        Task<User?> GetByUsernameAsync(string username);

        Task AddAsync(User user);

        Task EditAsync(User user);

        Task DeleteAsync(long id);

        Task<bool> ResetPassword(string username);

        Task<User?> GetUserByUsername(string username);

        Task<bool> ChangePassword(long id, string password);

    }
}
