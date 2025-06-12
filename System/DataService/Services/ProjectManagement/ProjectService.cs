
using DataContext;
using DataContext.Interfaces.ProjectManagement;
using DataService.Interfaces.ProjectManagement;

namespace DataService.Services.ProjectManagement
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;

        public ProjectService(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Project>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(Project project) => await _repository.AddAsync(project);

        public async Task DeleteAsync(long id) => await _repository.DeleteAsync(id);

        public async Task EditAsync(Project project) => await _repository.EditAsync(project);

        public async Task<List<DataModel.Dto.ProgresoTarea>> GetProgresoTareasPorFecha(DateTime dateTime, long clientId) => await _repository.GetProgresoTareasPorFecha(dateTime, clientId);

        public async Task<List<DataModel.Dto.ProgresoHoras>> GetProgresoHorasPorFecha(DateTime dateTime, long clientId) => await _repository.GetProgresoHorasPorFecha(dateTime, clientId);

        public async Task<List<DataModel.Dto.HorasTareaRol>> GetHorasTareasRol(DateTime dateTime, long clientId) => await _repository.GetHorasTareasRol(dateTime, clientId);
    }
}
