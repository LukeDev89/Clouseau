using DataModel.Dto;

namespace DataContext.Interfaces.ProjectManagement
{
    public interface IProjectRepository
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
