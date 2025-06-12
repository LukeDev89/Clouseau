
using DataContext;
using DataContext.Interfaces.Management;
using DataService.Interfaces.Management;

namespace DataService.Services.Management
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Role role) => await _repository.AddAsync(role);

        public async Task DeleteAsync(long id) => await _repository.DeleteAsync(id);

        public async Task EditAsync(Role role) => await _repository.EditAsync(role);

        public async Task<List<Role>> GetAsync() => await _repository.GetAsync();
    }
}
