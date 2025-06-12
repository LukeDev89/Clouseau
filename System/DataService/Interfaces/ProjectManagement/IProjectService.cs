

using DataContext;
using DataModel.Dto;

namespace DataService.Interfaces.ProjectManagement
{
    public interface IProjectService
    {
        Task<List<Project>> GetAsync();

        Task AddAsync(Project project);

        Task EditAsync(Project project);

        Task DeleteAsync(long id);

        Task<List<ProgresoTarea>> GetProgresoTareasPorFecha(DateTime dateTime, long clientId);

        Task<List<ProgresoHoras>> GetProgresoHorasPorFecha(DateTime dateTime, long clientId);

        Task<List<HorasTareaRol>> GetHorasTareasRol(DateTime dateTime, long clientId);
    }
}
