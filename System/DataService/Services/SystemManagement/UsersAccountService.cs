using DataContext;

using DataContext.Interfaces.Management;
using DataModel.Enums;
using DataService.Interfaces.Management;

namespace DataService.Services.Management
{
    public class UsersAccountService : IUsersAccountService
    {
        private readonly IUsersAccountRepository _repository;

        public UsersAccountService(IUsersAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(long userId, string username, string email, AccountType type) => await _repository.AddAsync(userId, username, email, type);

        public async Task DeleteAsync(long id) => await _repository.DeleteAsync(id);

        public async Task<List<UserAccount>> GetAsync() => await _repository.GetAsync();
    }
}
