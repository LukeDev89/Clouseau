
using DataModel.Enums;

namespace DataContext.Interfaces.Management
{
    public interface IUsersAccountRepository
    {
        Task<List<UserAccount>> GetAsync();

        Task DeleteAsync(long id);

        Task AddAsync(long userId, string username, string email, AccountType type);
    }
}
