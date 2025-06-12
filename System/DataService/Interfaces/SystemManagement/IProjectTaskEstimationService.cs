using DataContext;

namespace DataService.Interfaces.Management
{
    public interface IProjectTaskEstimationService
    {
        Task<List<ProjectTaskEstimation>> GetAsync();

        Task EditAsync(ProjectTaskEstimation taskEstimation);

        Task AddAsync(ProjectTaskEstimation taskEstimation);

        Task DeleteRolesAsync(long projectTaskId, long roleId);
    }
}
