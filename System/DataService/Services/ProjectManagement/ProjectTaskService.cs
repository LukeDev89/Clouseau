
using DataContext;
using DataContext.Interfaces.ProjectManagement;
using DataContext.Repositories.Management;
using DataModel.Dto;
using DataService.Interfaces.ProjectManagement;

namespace DataService.Services.ProjectManagement
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository _repository;

        public ProjectTaskService(IProjectTaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProjectTask>> GetAsync() => await _repository.GetAsync();

        public async Task<List<ProjectTask>> GetByTeamsIdAsync(List<long> teamIds) => await _repository.GetByTeamsIdAsync(teamIds);

        public async Task<List<ProjectTask>> GetByUserIdAsync(long userId) => await _repository.GetByUserIdAsync(userId);


        public async Task EditAsync(ProjectTask projectTask) => await _repository.EditAsync(projectTask);

        public async Task AddAsync(ProjectTask projectTask) => await _repository.AddAsync(projectTask);

        public async Task<List<HoursPerDayModel>> UserCalendar(long userId, bool force = false) => await _repository.UserCalendar(userId, force);
        public async Task<List<HoursPerDayModel>> UserCalendar(long userId) => await _repository.UserCalendar(userId);

        public async Task<List<HoursPerTaskPerDayModel>> UserCalendarPerTask(long userId, bool force = false) => await _repository.UserCalendarPerTask(userId, force);
        public async Task<List<HoursPerTaskPerDayModel>> UserCalendarPerTask(long userId) => await _repository.UserCalendarPerTask(userId);

        public async Task FinishAsync(long Id) => await _repository.FinishAsync(Id);
    }
}
