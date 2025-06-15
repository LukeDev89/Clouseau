

namespace DataContext.Interfaces.Management
{
    public interface ITaskTypeRepository
    {
        Task<List<ProjectTaskType>> GetAsync();

        Task DeleteAsync(long id);

        Task EditAsync(ProjectTaskType taskType);

        Task AddAsync(ProjectTaskType taskType);
    }
}
