

namespace DataContext.Interfaces.Management
{
    public interface ITaskTypeRepository
    {
        Task<List<TaskType>> GetAsync();

        Task DeleteAsync(long id);

        Task EditAsync(TaskType taskType);

        Task AddAsync(TaskType taskType);
    }
}
