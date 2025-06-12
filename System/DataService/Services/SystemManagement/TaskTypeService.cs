
using DataContext;
using DataContext.Interfaces.Management;
using DataService.Interfaces.Management;

namespace DataService.Services.Management
{
    public class TaskTypeService : ITaskTypeService
    {
        private readonly ITaskTypeRepository _repository;

        public TaskTypeService(ITaskTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(TaskType taskType) => await _repository.AddAsync(taskType);

        public async Task DeleteAsync(long id) => await _repository.DeleteAsync(id);

        public async Task EditAsync(TaskType taskType) => await _repository.EditAsync(taskType);

        public async Task<List<TaskType>> GetAsync() => await _repository.GetAsync();
    }
}
