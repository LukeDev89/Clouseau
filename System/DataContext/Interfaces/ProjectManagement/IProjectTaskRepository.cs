using DataContext.Repositories.Management;
using DataModel.Dto;

namespace DataContext.Interfaces.ProjectManagement
{
    public interface IProjectTaskRepository
    {
        Task<List<ProjectTask>> GetAsync();

        Task<List<ProjectTask>> GetByTeamsIdAsync(List<long> teamIds);

        Task<List<ProjectTask>> GetByUserIdAsync(long userId);

        

        Task EditAsync(ProjectTask projectTask);

        Task AddAsync(ProjectTask projectTask);

        Task<List<HoursPerDayModel>> UserCalendar(long userId, bool force = false);

        Task<List<HoursPerDayModel>> UserCalendar(long userId);

        Task<List<HoursPerTaskPerDayModel>> UserCalendarPerTask(long userId, bool force = false);

        Task<List<HoursPerTaskPerDayModel>> UserCalendarPerTask(long userId);
        Task FinishAsync(long Id);
    }
}
