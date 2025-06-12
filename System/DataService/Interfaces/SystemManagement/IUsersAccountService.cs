using DataContext;
using DataModel.Enums;

namespace DataService.Interfaces.Management
{
    public interface IUsersAccountService
    {
        Task<List<UserAccount>> GetAsync();

        Task DeleteAsync(long id);

        Task AddAsync(long userId, string username, string email, AccountType type);
    }
}
