
using DataContext;
using DataContext.Interfaces.Management;
using DataService.Interfaces.Management;

namespace DataService.Services.Management
{
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository _repository;

        public SectionService(ISectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Section>> GetAsync() => await _repository.GetAsync();

        public async Task<List<CustomPermission>> GetCustomPermissionAsync() => await _repository.GetCustomPermissionAsync();

        public async Task AddCustomPermissionAsync(CustomPermission customPermission) => await _repository.AddCustomPermissionAsync(customPermission);

        public async Task DeleteCustomPermissionAsync(long permissionId) => await _repository.DeleteCustomPermissionAsync(permissionId);

        public async Task<List<ProfilePermission>> GetProfilePermissionAsync() => await _repository.GetProfilePermissionAsync();

        public async Task AddProfilePermissionAsync(ProfilePermission profilePermission) => await _repository.AddProfilePermissionAsync(profilePermission);

        public async Task DeleteProfilePermissionAsync(long permissionId) => await _repository.DeleteProfilePermissionAsync(permissionId);

    }
}
