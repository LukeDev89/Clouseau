using DataContext.Interfaces.ProjectManagement;
using DataModel.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Repositories.ProjectManagement
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;

        public ProjectRepository(IDbContextFactory<ClouseauContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Project>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            return await _context.Projects
            .Include(x => x.Client).ThenInclude(x => x.ClientResponsibles)
            .Include(x => x.ProjectTasks)
            .Include(x => x.Team)
            .AsSplitQuery()
            .ToListAsync();
        }

        public async Task AddAsync(Project project)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            project.TeamId = project.TeamId < 0 ? null : project.TeamId;

            await _context.Projects.AddAsync(project);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Project project)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            project.TeamId = project.TeamId < 0 ? null : project.TeamId;
            project.Team = null!;
            project.Client = null!;
            project.Deleted = project.Active ? null : DateTime.Now;

            _context.Projects.Update(project);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var project = await _context.Projects.FirstAsync(x => x.Id == id);

            project.Active = false;
            project.Deleted = DateTime.Now;

            _context.Projects.Update(project);

            await _context.SaveChangesAsync();
        }

        public async Task GetExtraHoursPerDate(int month)
        {
            using var _context = _contextFactory.CreateDbContext();
        }

        public async Task<List<ProgresoTarea>> GetProgresoTareasPorFecha(DateTime dateTime, long clientId)
        {
            try
            {
                using var _context = _contextFactory.CreateDbContext();

                var progresoTarea = await _context.GetProgresoTareasPorFechaAsync(dateTime, clientId);

                return progresoTarea;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<ProgresoHoras>> GetProgresoHorasPorFecha(DateTime dateTime, long clientId)
        {
            using var _context = _contextFactory.CreateDbContext();

            var progresoTarea = await _context.GetProgresoHorasPorFechaAsync(dateTime, clientId);

            return progresoTarea;
        }

        public async Task<List<HorasTareaRol>> GetHorasTareasRol(DateTime dateTime, long clientId)
        {
            using var _context = _contextFactory.CreateDbContext();

            var progresoTarea = await _context.GetHorasTareasRolAsync(dateTime, clientId);

            return progresoTarea;
        }
    }
}
