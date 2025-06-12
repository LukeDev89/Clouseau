

using DataContext;

namespace DataService.Interfaces.Management
{
    public interface ITaskTypeService
    {
        Task<List<TaskType>> GetAsync();

        Task DeleteAsync(long id);

        Task EditAsync(TaskType taskType);

        Task AddAsync(TaskType taskType);
    }
}
