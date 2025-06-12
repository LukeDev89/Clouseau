
using DataContext;
using DataContext.Interfaces.Management;
using DataService.Interfaces.Management;
using Microsoft.Extensions.Configuration;

namespace DataService.Services.Management
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(User user) => await _repository.AddAsync(user);

        public async Task DeleteAsync(long id) => await _repository.DeleteAsync(id);

        public async Task EditAsync(User user) => await _repository.EditAsync(user);

        public async Task<List<User>> GetAsync() => await _repository.GetAsync();

        public async Task<bool> ResetPassword(string username) => await _repository.ResetPassword(username);

        public async Task<User?> GetUserByUsername(string username) => await _repository.GetUserByUsername(username);

        public async Task<bool> ChangePassword(long id, string password) => await _repository.ChangePassword(id, password);
    }
}
