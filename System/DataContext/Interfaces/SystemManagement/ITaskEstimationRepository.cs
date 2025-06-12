

namespace DataContext.Interfaces.Management
{
    public interface IProjectTaskEstimationRepository
    {
        Task<List<ProjectTaskEstimation>> GetAsync();

        Task EditAsync(ProjectTaskEstimation taskEstimation);

        Task AddAsync(ProjectTaskEstimation taskEstimation);

        Task DeleteRolesAsync(long projectTaskId, long roleId);
    }
}
