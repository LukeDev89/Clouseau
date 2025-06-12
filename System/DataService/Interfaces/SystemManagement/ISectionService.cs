

using DataContext;

namespace DataService.Interfaces.Management
{
    public interface ISectionService
    {
        Task<List<Section>> GetAsync();

        Task<List<CustomPermission>> GetCustomPermissionAsync();

        Task AddCustomPermissionAsync(CustomPermission customPermission);

        Task DeleteCustomPermissionAsync(long permissionId);

        Task<List<ProfilePermission>> GetProfilePermissionAsync();

        Task AddProfilePermissionAsync(ProfilePermission profilePermission);

        Task DeleteProfilePermissionAsync(long permissionId);



    }
}
