using DataContext;

using DataContext.Interfaces.Management;
using DataModel.Dto;
using DataModel.Request;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repositories.Management
{
    public class TaskProgressRepository : ITaskProgressRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;
        public TaskProgressRepository(IDbContextFactory<ClouseauContext> context)
        {
            _contextFactory = context;
        }

        public async Task<List<TaskProgress>> GetAsync()
        {
            using var _context = _contextFactory.CreateDbContext();
            return await _context.TaskProgresses.ToListAsync();
        }

        public async Task<List<TaskProgress>> GetByTaskIdAsync(long taskId)
        {
            using var _context = _contextFactory.CreateDbContext();

            return await _context.TaskProgresses.Where(x => x.TaskId == taskId).Include(c => c.User).Include(x => x.TaskType).ToListAsync();
        }

        public async Task AddAsync(List<TaskProgressRequest> request)
        {
            try
            {
                using var _context = _contextFactory.CreateDbContext();

                foreach (var r in request)
                {
                    if (r.Date > DateTime.Now) break;

                    await _context.TaskProgresses.AddAsync(new TaskProgress()
                    {
                        TaskId = r.TaskId,
                        UserId = r.UserId,
                        Hours = r.Hours,
                        Comment = string.IsNullOrEmpty(r.Comment) ? "" : r.Comment,
                        TaskTypeId = r.TaskTypeId,
                        Date = r.Date
                    });
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task EditAsync(List<TaskProgressRequest> request)
        {
            using var _context = _contextFactory.CreateDbContext();

            foreach (var r in request)
            {
                var taskProgress = await _context.TaskProgresses.FirstAsync(x => x.Id == r.ProgressId);

                taskProgress.Hours = r.Hours;
                taskProgress.Comment = string.IsNullOrEmpty(r.Comment) ? "" : r.Comment;
                taskProgress.TaskTypeId = r.TaskTypeId;
                taskProgress.Date = r.Date;

                _context.TaskProgresses.Update(taskProgress);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeletHourById(long id)
        {
            using var _context = _contextFactory.CreateDbContext();

            var taskProgress = await _context.TaskProgresses.FindAsync(id);

            _context.TaskProgresses.Remove(taskProgress);

            await _context.SaveChangesAsync();
        }

        public async Task<List<ExtraHoursView>> GetExtraHoursView(DateTime desde, DateTime hasta)
        {
            using var _context = _contextFactory.CreateDbContext();

            var query = @$"
                DECLARE @InicioPeriodo DATE = '{desde:yyyy-MM-dd}';  -- Fecha desde
                DECLARE @FinPeriodo DATE = '{hasta:yyyy-MM-dd}';    -- Fecha hasta

                -- Horas extras incurridas en días hábiles (horas cargadas - 8).
                SELECT 
                    (RTRIM(u.lastname) + ', ' + RTRIM(u.firstname)) AS Integrante,
                    CONVERT(DATE, dh.Fecha) AS Fecha,
                    COALESCE(SUM(a.hours) - 8, 0) AS HorasExtras,  -- Calcula horas extras
                    CASE 
                        WHEN COALESCE(SUM(a.hours), 0) > 8 THEN 'Horas extra'
                        ELSE 'Horas normales'
                    END AS Estado
                FROM 
                    [dbo].[Users] u 
                CROSS JOIN 
                    dbo.fn_ObtenerDiasHabilesInicioFin(@InicioPeriodo, @FinPeriodo) AS dh
                LEFT JOIN 
                    [dbo].[TaskProgress] a ON a.userid = u.id AND CAST(a.date AS DATE) = dh.Fecha
                GROUP BY 
                    u.lastname, u.firstname, dh.Fecha
                HAVING 
                    COALESCE(SUM(a.hours), 0) > 8

                UNION ALL

                -- Horas incurridas en días no hábiles (total horas).
                SELECT 
                    (RTRIM(u.lastname) + ', ' + RTRIM(u.firstname)) AS Integrante,
                    CAST(a.date AS DATE) AS Fecha,
                    COALESCE(SUM(a.hours), 0) AS HorasCargadas,  -- Total de horas en días no hábiles
                    'Trabajo en día no hábil' AS Estado
                FROM 
                    [dbo].[Users] u 
                LEFT JOIN 
                    [dbo].[TaskProgress] a ON a.userid = u.id
                WHERE 
                    CAST(a.date AS DATE) BETWEEN @InicioPeriodo AND @FinPeriodo AND 
                    CAST(a.date AS DATE) NOT IN (SELECT Fecha FROM dbo.fn_ObtenerDiasHabilesInicioFin(@InicioPeriodo, @FinPeriodo))
                GROUP BY 
                    u.lastname, u.firstname, CAST(a.date AS DATE)
                HAVING 
                    COALESCE(SUM(a.hours), 0) > 0

                ORDER BY 
                    Integrante, Fecha;
            ";

            using var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;

            _context.Database.OpenConnection();

            using var result = command.ExecuteReader();

            var horasExtrasResults = new List<ExtraHoursView>();

            while (result.Read())
            {
                horasExtrasResults.Add(new ExtraHoursView
                {
                    Integrante = result.GetString(0),
                    Fecha = result.GetDateTime(1),
                    Horas = result.GetDecimal(2),
                    Estado = result.GetString(3),
                });
            }

            return horasExtrasResults;
        }

        public async Task<List<IncompleteHoursView>> GetIncompleteWorkHoursView(long userId, DateTime desde, DateTime hasta)
        {
            using var _context = _contextFactory.CreateDbContext();

            var query = @$"
                DECLARE @UserId INT = {userId};  -- ID del usuario
                DECLARE @InicioPeriodo DATE = '{desde:yyyy-MM-dd}';  -- Fecha desde
                DECLARE @FinPeriodo DATE = '{hasta:yyyy-MM-dd}';    -- Fecha hasta

               -- Crear una tabla temporal para almacenar todos los días en el rango
                DECLARE @Dias TABLE (Fecha DATE);

                -- Llenar la tabla con los días del periodo
                ;WITH DateSequence AS
                (
                    SELECT @InicioPeriodo AS Fecha
                    UNION ALL
                    SELECT DATEADD(DAY, 1, Fecha)
                    FROM DateSequence
                    WHERE Fecha < @FinPeriodo
                )
                INSERT INTO @Dias
                SELECT Fecha FROM DateSequence
                OPTION (MAXRECURSION 32767);  -- Aumentamos el límite de recursión

                -- Asegurarnos que todos los días generados sean correctamente evaluados con los progresos
                SELECT 
                    (RTRIM(u.lastname) + ', ' + RTRIM(u.firstname)) AS Integrante,
                    d.Fecha,
                    COALESCE(SUM(a.hours), 0) AS HorasCargadas,  -- Total de horas cargadas
                    CASE 
                        WHEN COALESCE(SUM(a.hours), 0) > 0 AND COALESCE(SUM(a.hours), 0) < 8 THEN 'Menos de 8 horas trabajadas'
                        ELSE 'Sin progreso'
                    END AS Estado
                FROM 
                    [dbo].[Users] u 
                CROSS JOIN 
                    @Dias d
                LEFT JOIN 
                    [dbo].[TaskProgress] a ON a.userid = u.id AND CAST(a.date AS DATE) = d.Fecha
                WHERE 
                    u.id = @UserId  -- Filtro por el userId proporcionado
                    AND DATEPART(WEEKDAY, d.Fecha) NOT IN (1, 7)  -- Excluir fines de semana (1 = domingo, 7 = sábado)
                GROUP BY 
                    u.lastname, u.firstname, d.Fecha
                HAVING 
                    COALESCE(SUM(a.hours), 0) < 8  -- Solo días con horas menores a 8 o sin progreso
                ORDER BY 
                    Integrante, Fecha;
            ";

            using var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;

            _context.Database.OpenConnection();

            using var result = command.ExecuteReader();

            var incompleteHoursResults = new List<IncompleteHoursView>();

            while (result.Read())
            {
                incompleteHoursResults.Add(new IncompleteHoursView
                {
                    Integrante = result.GetString(0),
                    Fecha = result.GetDateTime(1),
                    Horas = result.GetDecimal(2),
                    Estado = result.GetString(3),
                });
            }

            return incompleteHoursResults;
        }

        public async Task<string> GetIncompleteWorkHoursMessage(long userId, DateTime desde, DateTime hasta)
        {
            var startDate = desde < DateTime.Now.AddMonths(-6) ? DateTime.Now.AddMonths(-6) : desde;

            var incompleteDays = await GetIncompleteWorkHoursView(userId, startDate, hasta);

            if (!incompleteDays.Any())
            {
                return "";
            }

            var groupedByMonth = incompleteDays
                .GroupBy(d => new { d.Fecha.Year, d.Fecha.Month })
                .Select(g => new { Year = g.Key.Year, Month = g.Key.Month })
                .OrderBy(g => g.Year).ThenBy(g => g.Month)
                .ToList();

            var cultureInfo = new System.Globalization.CultureInfo("es-ES");

            var monthGroups = groupedByMonth
                .GroupBy(g => g.Year)
                .ToDictionary(g => g.Key, g => g.Select(m => cultureInfo.DateTimeFormat.GetMonthName(m.Month)));

            var resultMessages = new List<string>();

            foreach (var yearGroup in monthGroups)
            {
                var months = string.Join(", ", yearGroup.Value);
                resultMessages.Add($"{months} {yearGroup.Key}");
            }

            var result = string.Join("<br />", resultMessages);

            return result;
        }
    }
}
