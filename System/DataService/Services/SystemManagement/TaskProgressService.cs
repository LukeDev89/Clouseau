
using DataContext;
using DataContext.Interfaces.Management;
using DataModel.Dto;
using DataModel.Request;
using DataService.Interfaces.Management;

namespace DataService.Services.Management
{
    public class TaskProgressService : ITaskProgressService
    {
        private readonly ITaskProgressRepository _repository;

        public TaskProgressService(ITaskProgressRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TaskProgress>> GetAsync() => await _repository.GetAsync();

        public async Task<List<TaskProgress>> GetByTaskIdAsync(long taskId) => await _repository.GetByTaskIdAsync(taskId);

        public async Task AddAsync(List<TaskProgressRequest> request) => await _repository.AddAsync(request);

        public async Task EditAsync(List<TaskProgressRequest> request) => await _repository.EditAsync(request);

        public async Task DeletHourById(long id) => await _repository.DeletHourById(id);

        public async Task<List<ExtraHoursView>> GetExtraHoursView(DateTime desde, DateTime hasta) => await _repository.GetExtraHoursView(desde, hasta);

        public async Task<List<IncompleteHoursView>> GetIncompleteWorkHoursView(long userId, DateTime desde, DateTime hasta) => await _repository.GetIncompleteWorkHoursView(userId, desde, hasta);

        public async Task<string> GetIncompleteWorkHoursMessage(long userId, DateTime desde, DateTime hasta) => await _repository.GetIncompleteWorkHoursMessage(userId, desde, hasta);
    }
}
