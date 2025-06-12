
using DataContext;
using DataContext.Interfaces.Management;
using DataService.Interfaces.Management;

namespace DataService.Services.Management
{
    public class ProjectTaskEstimationService : IProjectTaskEstimationService
    {
        private readonly IProjectTaskEstimationRepository _repository;

        public ProjectTaskEstimationService(IProjectTaskEstimationRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(ProjectTaskEstimation taskEstimation) => await _repository.AddAsync(taskEstimation);
        public async Task DeleteRolesAsync(long projectTaskId, long roleId) => await _repository.DeleteRolesAsync(projectTaskId, roleId);
        public async Task EditAsync(ProjectTaskEstimation taskEstimation) => await _repository.EditAsync(taskEstimation);
        public async Task<List<ProjectTaskEstimation>> GetAsync() => await _repository.GetAsync();
    }
}
