
using DataContext;
using DataModel.Dto;
using DataModel.Request;

namespace DataService.Interfaces.Management
{
    public interface ITaskProgressService
    {
        Task<List<ProjectTaskProgress>> GetAsync();

        Task AddAsync(List<TaskProgressRequest> request);

        Task EditAsync(List<TaskProgressRequest> request);

        Task<List<ProjectTaskProgress>> GetByTaskIdAsync(long taskId);

        Task DeletHourById(long id);

        Task<List<ExtraHoursView>> GetExtraHoursView(DateTime desde, DateTime hasta);

        Task<List<IncompleteHoursView>> GetIncompleteWorkHoursView(long userId, DateTime desde, DateTime hasta);

        Task<string> GetIncompleteWorkHoursMessage(long userId, DateTime desde, DateTime hasta);
    }
}
