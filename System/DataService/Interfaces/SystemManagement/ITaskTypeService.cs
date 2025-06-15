

using DataContext;

namespace DataService.Interfaces.Management
{
    public interface ITaskTypeService
    {
        Task<List<ProjectTaskType>> GetAsync();

        Task DeleteAsync(long id);

        Task EditAsync(ProjectTaskType taskType);

        Task AddAsync(ProjectTaskType taskType);
    }
}
