
using DataModel.Dto;
using DataModel.Request;

namespace DataContext.Interfaces.Management
{
    public interface ITaskProgressRepository
    {
        Task<List<TaskProgress>> GetAsync();

        Task AddAsync(List<TaskProgressRequest> request);

        Task EditAsync(List<TaskProgressRequest> request);

        Task<List<TaskProgress>> GetByTaskIdAsync(long taskId);

        Task DeletHourById(long id);

        Task<List<ExtraHoursView>> GetExtraHoursView(DateTime desde, DateTime hasta);

        Task<List<IncompleteHoursView>> GetIncompleteWorkHoursView(long userId, DateTime desde, DateTime hasta);

        Task<string> GetIncompleteWorkHoursMessage(long userId, DateTime desde, DateTime hasta);
    }
}
