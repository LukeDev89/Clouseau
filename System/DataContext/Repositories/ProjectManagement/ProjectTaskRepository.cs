using DataContext.Interfaces.ProjectManagement;
using DataModel.Dto;
using DataModel.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataContext.Repositories.ProjectManagement
{
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;

        private User _user { get; set; } = new User();
        private List<UserNonworkingDay> _userLicence { get; set; } = new List<UserNonworkingDay>();
        private List<DateTime> _holidays { get; set; } = new List<DateTime>();
        private List<TaskProgress> _progress { get; set; } = new List<TaskProgress>();

        public ProjectTaskRepository(IDbContextFactory<ClouseauContext> context)
        {
            _contextFactory = context;
        }

        public async Task<List<ProjectTask>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            return await _context.ProjectTasks
				.Include(x => x.Project).ThenInclude(x => x.Client).ThenInclude(x => x.ClientResponsibles)
	            .Include(x => x.Project).ThenInclude(x => x.Team)
				.Include(x => x.Client)
				.Include(x => x.TaskProgresses)
                .Include(x => x.TaskType)
                .Include(x => x.ProjectTaskEstimations)
                    .ThenInclude(x => x.Role)
                .Where(x => x.Active)
                .OrderByDescending(x => x.CreationDate)
                .AsSplitQuery()
				.ToListAsync();
        }

        public async Task<List<ProjectTask>> GetByTeamsIdAsync(List<long> teamIds)
        {
            using var _context = _contextFactory.CreateDbContext();

            return await _context.ProjectTasks
				.Include(x => x.Project).ThenInclude(x => x.Client).ThenInclude(x => x.ClientResponsibles)
				.Include(x => x.Project).ThenInclude(x => x.Team)
                .Include(x => x.Client)
				.Include(x => x.TaskProgresses)
                .Include(x => x.TaskType)
                .Include(x => x.ProjectTaskEstimations)
                    .ThenInclude(x => x.Role)
                .Where(x => x.Active && teamIds.Contains(x.Project.Team.Id))
                .OrderByDescending(x => x.CreationDate)
				.AsSplitQuery()
				.ToListAsync();
        }

        public async Task<List<ProjectTask>> GetByUserIdAsync(long userId)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var user = await _context.Users
                .Include(x => x.UserTeams)
                    .ThenInclude(x => x.Team)
                    .ThenInclude(x => x.Projects)
					.ThenInclude(x => x.ProjectTasks)
                    .ThenInclude(x => x.ProjectTaskEstimations)
                    .ThenInclude(x => x.Role)
                .Include(x => x.UserTeams)
                    .ThenInclude(x => x.Team)
                    .ThenInclude(x => x.Projects)
                    .ThenInclude(x => x.ProjectTasks)
                    .ThenInclude(x => x.TaskProgresses)
                    .ThenInclude(x => x.TaskType)
                .Include(x => x.UserTeams)
                    .ThenInclude(x => x.Team)
                    .ThenInclude(x => x.Projects)
                    .ThenInclude(x => x.Client)
                    .ThenInclude(x => x.ClientResponsibles)
				.Include(x => x.UserTeams)
					.ThenInclude(x => x.Team)
					.ThenInclude(x => x.Projects)
					.ThenInclude(x => x.ProjectTasks)
					.ThenInclude(x => x.Client)
					.ThenInclude(x => x.ClientResponsibles)
				.AsSplitQuery()
                .FirstAsync(x => x.Id == userId);

            var projects = user.UserTeams.SelectMany(x => x.Team.Projects).DistinctBy(x => x.TeamId).ToList();

            return projects.SelectMany(x => x.ProjectTasks).Where(x => x.Active).ToList();
        }

        public async Task AddAsync(ProjectTask projectTask)
        {
            try
            {
                using var _context = await _contextFactory.CreateDbContextAsync();
                projectTask.TaskTypeId = projectTask.TaskTypeId != -1 ? projectTask.TaskTypeId : null;

                await _context.ProjectTasks.AddAsync(projectTask);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
            }
        }

        public async Task EditAsync(ProjectTask projectTask)
        {
            try
            {
                using var _context = await _contextFactory.CreateDbContextAsync();

                var entity = await _context.ProjectTasks.AsNoTracking().FirstAsync(x => x.Id == projectTask.Id);

                entity.Name = projectTask.Name;
                entity.Description = projectTask.Description;
                entity.Active = projectTask.Active;
                entity.CreationDate = projectTask.CreationDate;
                entity.InitDate = projectTask.InitDate;
                entity.EndDate = projectTask.EndDate;
                entity.DeliverDate = projectTask.DeliverDate;
                entity.KanbanId = projectTask.KanbanId;
                entity.TaskTypeId = projectTask.TaskTypeId != -1 ? projectTask.TaskTypeId : null;

                _context.ProjectTasks.Update(entity);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<List<HoursPerDayModel>> UserCalendar(long userId, bool force = false)
        {
            Task<User> userTask = null;
            Task<List<UserNonworkingDay>> userLicenceTask = null;
            Task<List<DateTime>> holidaysTask = null;
            Task<List<TaskProgress>> progressTask = null;

            if (force)
            {
                var context = _contextFactory.CreateDbContext();

                _user = await context.Users.FirstAsync(x => x.Id == userId);
                _userLicence = await context.UserNonworkingDays.Where(x => x.UserId == userId).ToListAsync();
                _holidays = await context.NonworkingDays.Select(nd => nd.Day).ToListAsync();
                _progress = await context.TaskProgresses
                    .Where(x => x.UserId == userId)
                    .Include(x => x.Task)
                        .ThenInclude(t => t.Project)
                    .Include(x => x.TaskType)
                    .Include(x => x.User)
                    .AsSplitQuery()
                    .ToListAsync();
            }
            else
            {
                if (_user == null || _user.Id != userId)
                {
                    var context = _contextFactory.CreateDbContext();
                    userTask = context.Users.FirstAsync(x => x.Id == userId);
                }

                if (!_userLicence.Any())
                {
                    var context = _contextFactory.CreateDbContext();
                    userLicenceTask = context.UserNonworkingDays.Where(x => x.UserId == userId).ToListAsync();
                }

                if (!_holidays.Any())
                {
                    var context = _contextFactory.CreateDbContext();
                    holidaysTask = context.NonworkingDays.Select(nd => nd.Day).ToListAsync();
                }

                if (!_progress.Any())
                {
                    var context = _contextFactory.CreateDbContext();
                    progressTask = context.TaskProgresses
                        .Where(x => x.UserId == userId)
                        .Include(x => x.Task)
                            .ThenInclude(t => t.Project)
                        .Include(x => x.TaskType)
                        .Include(x => x.User)
                        .AsSplitQuery()
                        .ToListAsync();
                }

                var tasksToWait = new List<Task>();
                if (userTask != null) tasksToWait.Add(userTask);
                if (userLicenceTask != null) tasksToWait.Add(userLicenceTask);
                if (holidaysTask != null) tasksToWait.Add(holidaysTask);
                if (progressTask != null) tasksToWait.Add(progressTask);

                await Task.WhenAll(tasksToWait);

                if (userTask != null) _user = await userTask;
                if (userLicenceTask != null) _userLicence = await userLicenceTask;
                if (holidaysTask != null) _holidays = await holidaysTask;
                if (progressTask != null) _progress = await progressTask;
            }

            var licenceDates = new List<DateTime>();

            foreach (var item in _userLicence)
            {
                if (item.State != (short)NonworkingStatus.Approved) continue;

                var licStart = item.DateFrom.Date;
                var licEnds = item.DateTo.Date;

                var dates = Enumerable.Range(0, (licEnds - licStart).Days + 1)
                                     .Select(offset => licStart.AddDays(offset))
                                     .Where(date => !IsWeekend(date))
                                     .ToList();

                licenceDates.AddRange(dates);
            }

            var startDate = _user.Income.Date;
            var endDate = _user.Outcome.HasValue ? _user.Outcome.Value.Date : DateTime.Now.Date;

            var workDays = Enumerable.Range(0, (endDate - startDate).Days + 1)
                                     .Select(offset => startDate.AddDays(offset))
                                     .Where(date => !IsWeekend(date) && !_holidays.Contains(date) && !licenceDates.Contains(date))
                                     .ToList();

            var hoursPerDay = new List<HoursPerDayModel>();

            foreach (var day in workDays)
            {
                var hours = _progress.Where(x => x.Date.Date == day.Date).Sum(x => x.Hours);
                var tasks = _progress.Where(x => x.Date.Date == day.Date).Select(x => x.Task.Name).ToList();

                hoursPerDay.Add(new HoursPerDayModel()
                {
                    Date = DateOnly.FromDateTime(day),
                    Hours = hours,
                    Tasks = tasks
                });
            }

            var workDaysSet = new HashSet<DateTime>(workDays);

            var extraDays = _progress
                .Where(p => !workDaysSet.Contains(p.Date.Date))
                .Select(p => p.Date.Date)
                .Distinct()
                .ToList();

            var extraDaysNotIncluded = extraDays
                .Where(extraDay => !hoursPerDay.Exists(hpd => hpd.Date.Equals(DateOnly.FromDateTime(extraDay))))
                .ToList();

            foreach (var extraDay in extraDaysNotIncluded)
            {
                var hours = _progress.Where(x => x.Date.Date == extraDay).Sum(x => x.Hours);
                var tasks = _progress.Where(x => x.Date.Date == extraDay)
                        .Select(x => $"{x.Task.Project.Name}/{x.Task.Name}").ToList();

                hoursPerDay.Add(new HoursPerDayModel()
                {
                    Date = DateOnly.FromDateTime(extraDay),
                    Hours = hours,
                    Tasks = tasks
                });
            }

            hoursPerDay = hoursPerDay.OrderByDescending(x => x.Date).ToList();

            return hoursPerDay.OrderByDescending(x => x.Date).ToList();
        }

        public async Task<List<HoursPerDayModel>> UserCalendar(long userId)
        {
            Task<User> userTask = null;
            Task<List<UserNonworkingDay>> userLicenceTask = null;
            Task<List<DateTime>> holidaysTask = null;
            Task<List<TaskProgress>> progressTask = null;

            var context = _contextFactory.CreateDbContext();

            var user = await context.Users.FirstAsync(x => x.Id == userId);
            var userLicence = await context.UserNonworkingDays.Where(x => x.UserId == userId).ToListAsync();
            var holidays = await context.NonworkingDays.Select(nd => nd.Day).ToListAsync();
            var progress = await context.TaskProgresses
                .Where(x => x.UserId == userId)
                .Include(x => x.Task)
                    .ThenInclude(t => t.Project)
                .Include(x => x.TaskType)
                .Include(x => x.User)
                .AsSplitQuery()
                .ToListAsync();

            var licenceDates = new List<DateTime>();

            foreach (var item in userLicence)
            {
                if (item.State != (short)NonworkingStatus.Approved) continue;

                var licStart = item.DateFrom.Date;
                var licEnds = item.DateTo.Date;

                var dates = Enumerable.Range(0, (licEnds - licStart).Days + 1)
                                     .Select(offset => licStart.AddDays(offset))
                                     .Where(date => !IsWeekend(date))
                                     .ToList();

                licenceDates.AddRange(dates);
            }

            var startDate = user.Income.Date;
            var endDate = user.Outcome.HasValue ? user.Outcome.Value.Date : DateTime.Now.Date;

            var workDays = Enumerable.Range(0, (endDate - startDate).Days + 1)
                                     .Select(offset => startDate.AddDays(offset))
                                     .Where(date => !IsWeekend(date) && !holidays.Contains(date) && !licenceDates.Contains(date))
                                     .ToList();

            var hoursPerDay = new List<HoursPerDayModel>();

            foreach (var day in workDays)
            {
                var hours = progress.Where(x => x.Date.Date == day.Date).Sum(x => x.Hours);
                var tasks = progress.Where(x => x.Date.Date == day.Date).Select(x => x.Task.Name).ToList();

                hoursPerDay.Add(new HoursPerDayModel()
                {
                    Date = DateOnly.FromDateTime(day),
                    Hours = hours,
                    Tasks = tasks
                });
            }

            var workDaysSet = new HashSet<DateTime>(workDays);

            var extraDays = progress
                .Where(p => !workDaysSet.Contains(p.Date.Date))
                .Select(p => p.Date.Date)
                .Distinct()
                .ToList();

            var extraDaysNotIncluded = extraDays
                .Where(extraDay => !hoursPerDay.Exists(hpd => hpd.Date.Equals(DateOnly.FromDateTime(extraDay))))
                .ToList();

            foreach (var extraDay in extraDaysNotIncluded)
            {
                var hours = progress.Where(x => x.Date.Date == extraDay).Sum(x => x.Hours);
                var tasks = progress.Where(x => x.Date.Date == extraDay)
                        .Select(x => $"{x.Task.Project.Name}/{x.Task.Name}").ToList();

                hoursPerDay.Add(new HoursPerDayModel()
                {
                    Date = DateOnly.FromDateTime(extraDay),
                    Hours = hours,
                    Tasks = tasks
                });
            }

            hoursPerDay = hoursPerDay.OrderByDescending(x => x.Date).ToList();

            return hoursPerDay.OrderByDescending(x => x.Date).ToList();
        }

        public async Task<List<HoursPerTaskPerDayModel>> UserCalendarPerTask(long userId, bool force = false)
        {
            Task<User> userTask = null;
            Task<List<UserNonworkingDay>> userLicenceTask = null;
            Task<List<DateTime>> holidaysTask = null;
            Task<List<TaskProgress>> progressTask = null;

            if (force)
            {
                var context = _contextFactory.CreateDbContext();

                _user = await context.Users.FirstAsync(x => x.Id == userId);
                _userLicence = await context.UserNonworkingDays.Where(x => x.UserId == userId).ToListAsync();
                _holidays = await context.NonworkingDays.Select(nd => nd.Day).ToListAsync();
                _progress = await context.TaskProgresses
                    .Where(x => x.UserId == userId)
                    .Include(x => x.Task)
                        .ThenInclude(t => t.Project)
                    .Include(x => x.TaskType)
                    .Include(x => x.User)
                    .AsSplitQuery()
                    .ToListAsync();
            }
            else
            {
                if (_user == null || _user.Id != userId)
                {
                    var context = _contextFactory.CreateDbContext();
                    userTask = context.Users.FirstAsync(x => x.Id == userId);
                }

                if (!_userLicence.Any())
                {
                    var context = _contextFactory.CreateDbContext();
                    userLicenceTask = context.UserNonworkingDays.Where(x => x.UserId == userId).ToListAsync();
                }

                if (!_holidays.Any())
                {
                    var context = _contextFactory.CreateDbContext();
                    holidaysTask = context.NonworkingDays.Select(nd => nd.Day).ToListAsync();
                }

                if (!_progress.Any())
                {
                    var context = _contextFactory.CreateDbContext();
                    progressTask = context.TaskProgresses
                        .Where(x => x.UserId == userId)
                        .Include(x => x.Task)
                            .ThenInclude(t => t.Project)
                        .Include(x => x.TaskType)
                        .Include(x => x.User)
                        .AsSplitQuery()
                        .ToListAsync();
                }

                var tasksToWait = new List<Task>();
                if (userTask != null) tasksToWait.Add(userTask);
                if (userLicenceTask != null) tasksToWait.Add(userLicenceTask);
                if (holidaysTask != null) tasksToWait.Add(holidaysTask);
                if (progressTask != null) tasksToWait.Add(progressTask);

                await Task.WhenAll(tasksToWait);

                if (userTask != null) _user = await userTask;
                if (userLicenceTask != null) _userLicence = await userLicenceTask;
                if (holidaysTask != null) _holidays = await holidaysTask;
                if (progressTask != null) _progress = await progressTask;
            }

            var licenceDates = new List<DateTime>();

            foreach (var item in _userLicence)
            {
                if (item.State != (short)NonworkingStatus.Approved) continue;

                var licStart = item.DateFrom.Date;
                var licEnds = item.DateTo.Date;
                var dates = Enumerable.Range(0, (licEnds - licStart).Days + 1)
                                      .Select(offset => licStart.AddDays(offset))
                                      .Where(date => !IsWeekend(date))
                                      .ToList();

                licenceDates.AddRange(dates);
            }

            var startDate = _user.Income.Date;
            var endDate = _user.Outcome.HasValue ? _user.Outcome.Value.Date : DateTime.Now.Date;

            var workDays = Enumerable.Range(0, (endDate - startDate).Days + 1)
                                     .Select(offset => startDate.AddDays(offset))
                                     .Where(date => !IsWeekend(date) && !_holidays.Contains(date) && !licenceDates.Contains(date))
                                     .ToList();

            var hoursPerTaskPerDay = new List<HoursPerTaskPerDayModel>();

            foreach (var day in workDays)
            {
                var tasksOfDay = _progress.Where(x => x.Date.Date == day.Date);
                foreach (var taskProgress in tasksOfDay)
                {
                    hoursPerTaskPerDay.Add(new HoursPerTaskPerDayModel()
                    {
                        Date = DateOnly.FromDateTime(day),
                        Hours = taskProgress.Hours,
                        Task = taskProgress.Task.Name,
                        Project = taskProgress.Task.Project.Name,
                        TaskType = taskProgress.TaskType.Name,
                        UserId = taskProgress.UserId,
                        User = taskProgress.User.UserFullName
                    });
                }
            }

            var workDaysSet = new HashSet<DateTime>(workDays);
            var extraDays = _progress
                .Where(p => !workDaysSet.Contains(p.Date.Date))
                .Select(p => p.Date.Date)
                .Distinct();

            foreach (var extraDay in extraDays)
            {
                var tasksOfExtraDay = _progress.Where(x => x.Date.Date == extraDay);
                foreach (var taskProgress in tasksOfExtraDay)
                {
                    hoursPerTaskPerDay.Add(new HoursPerTaskPerDayModel()
                    {
                        Date = DateOnly.FromDateTime(extraDay),
                        Hours = taskProgress.Hours,
                        Task = taskProgress.Task.Name,
                        Project = taskProgress.Task.Project.Name,
                        TaskType = taskProgress.TaskType.Name
                    });
                }
            }

            return hoursPerTaskPerDay.OrderByDescending(x => x.Date).ToList();
        }

        public async Task<List<HoursPerTaskPerDayModel>> UserCalendarPerTask(long userId)
        {
            Task<User> userTask = null;
            Task<List<UserNonworkingDay>> userLicenceTask = null;
            Task<List<DateTime>> holidaysTask = null;
            Task<List<TaskProgress>> progressTask = null;

            var context = _contextFactory.CreateDbContext();

            var user = await context.Users.FirstAsync(x => x.Id == userId);
            var userLicence = await context.UserNonworkingDays.Where(x => x.UserId == userId).ToListAsync();
            var holidays = await context.NonworkingDays.Select(nd => nd.Day).ToListAsync();
            var progress = await context.TaskProgresses
                .Where(x => x.UserId == userId)
                .Include(x => x.Task)
                    .ThenInclude(t => t.Project)
                .Include(x => x.TaskType)
                .Include(x => x.User)
                .AsSplitQuery()
                .ToListAsync();

            var licenceDates = new List<DateTime>();

            foreach (var item in userLicence)
            {
                if (item.State != (short)NonworkingStatus.Approved) continue;

                var licStart = item.DateFrom.Date;
                var licEnds = item.DateTo.Date;
                var dates = Enumerable.Range(0, (licEnds - licStart).Days + 1)
                                      .Select(offset => licStart.AddDays(offset))
                                      .Where(date => !IsWeekend(date))
                                      .ToList();

                licenceDates.AddRange(dates);
            }

            var startDate = user.Income.Date;
            var endDate = user.Outcome.HasValue ? user.Outcome.Value.Date : DateTime.Now.Date;

            var workDays = Enumerable.Range(0, (endDate - startDate).Days + 1)
                                     .Select(offset => startDate.AddDays(offset))
                                     .Where(date => !IsWeekend(date) && !holidays.Contains(date) && !licenceDates.Contains(date))
                                     .ToList();

            var hoursPerTaskPerDay = new List<HoursPerTaskPerDayModel>();

            foreach (var day in workDays)
            {
                var tasksOfDay = progress.Where(x => x.Date.Date == day.Date);
                foreach (var taskProgress in tasksOfDay)
                {
                    hoursPerTaskPerDay.Add(new HoursPerTaskPerDayModel()
                    {
                        Date = DateOnly.FromDateTime(day),
                        Hours = taskProgress.Hours,
                        Task = taskProgress.Task.Name,
                        Project = taskProgress.Task.Project.Name,
                        TaskType = taskProgress.TaskType.Name,
                        UserId = taskProgress.UserId,
                        User = taskProgress.User.UserFullName
                    });
                }
            }

            var workDaysSet = new HashSet<DateTime>(workDays);
            var extraDays = progress
                .Where(p => !workDaysSet.Contains(p.Date.Date))
                .Select(p => p.Date.Date)
                .Distinct();

            foreach (var extraDay in extraDays)
            {
                var tasksOfExtraDay = progress.Where(x => x.Date.Date == extraDay);
                foreach (var taskProgress in tasksOfExtraDay)
                {
                    hoursPerTaskPerDay.Add(new HoursPerTaskPerDayModel()
                    {
                        Date = DateOnly.FromDateTime(extraDay),
                        Hours = taskProgress.Hours,
                        Task = taskProgress.Task.Name,
                        Project = taskProgress.Task.Project.Name,
                        TaskType = taskProgress.TaskType.Name
                    });
                }
            }

            return hoursPerTaskPerDay.OrderByDescending(x => x.Date).ToList();
        }

        bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        public async Task FinishAsync(long Id)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var entity = await _context.ProjectTasks.FirstAsync(x => x.Id == Id);

            entity.Finished = true;

            _context.ProjectTasks.Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}
