using DataContext.Interfaces.Management;
using DataModel.Enums;
using DataModel.Request;
using DataModel.Response;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace DataContext.Repositories.Management
{
    public class UserNonworkingDaysRepository : IUserNonworkingDaysRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;

        // TODO: These should be configurable or determined from the database
        private const long DEFAULT_LICENSE_TASK_ID = 1; // This should be the ID of a task for license hours
        private const long DEFAULT_LICENSE_TASK_TYPE_ID = 1; // This should be the ID of a task type for license hours
        private const decimal DEFAULT_HOURS_PER_DAY = 8m; // Default hours per working day

        public UserNonworkingDaysRepository(IDbContextFactory<ClouseauContext> context)
        {
            _contextFactory = context;
        }

        public async Task<List<UserNonworkingDay>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            var nondays = await _context.UserNonworkingDays
                .Include(u => u.User).ThenInclude(x => x.UserTeams)
                .Include(u => u.Type).ToListAsync();

            var expiredDays = false;

            foreach (var item in nondays)
            {
                item.State = item.State == (short)NonworkingStatus.Pending && item.Created.AddDays(45) < DateTime.Now ?
                    (short)NonworkingStatus.Expired : item.State;

                expiredDays = true;
            }

            if (expiredDays)
            {
                _context.UpdateRange(nondays.Where(x => x.State == (short)NonworkingStatus.Expired).ToList());
                await _context.SaveChangesAsync();
            }

            return nondays;
        }

        public async Task AddAsync(long userId, long type, DateTime from, DateTime to, string? comment, string? fileExtension, string? fileBase64)
        {
            if (from > to || userId == -1 || type == -1) return;

            using var _context = await _contextFactory.CreateDbContextAsync();
            var user = await _context.Users.FirstAsync(x => x.Id == userId);
            
            var basePath = @$"c:\Luke\Clouseau\Archivos\{user.Username}\";
            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var fullPath = Path.Combine(basePath, fileName);

            if (!Directory.Exists(basePath)) Directory.CreateDirectory(basePath);

            if (!string.IsNullOrEmpty(fileBase64))
            {
                byte[] fileBytes = Convert.FromBase64String(fileBase64);
                await File.WriteAllBytesAsync(fullPath, fileBytes);
            }

            await _context.UserNonworkingDays.AddAsync(new UserNonworkingDay()
            {
                UserId = userId,
                TypeId = type,
                DateFrom = from,
                DateTo = to,
                Comment = comment,
                Path = !string.IsNullOrEmpty(fileBase64) ? fullPath : null
            });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long Id)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var userWorkingDay = await _context.UserNonworkingDays.FirstAsync(x => x.Id == Id);

            _context.Remove(userWorkingDay);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateState(long Id, short state, string commentCfo)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var userNonworkingDay = await _context.UserNonworkingDays.FirstAsync(x => x.Id == Id);
            var previousState = userNonworkingDay.State;
            
            userNonworkingDay.State = state;
            userNonworkingDay.CommentCfo = commentCfo;

            _context.UserNonworkingDays.Update(userNonworkingDay);

            // Handle license approval/revocation logic
            if (state == (short)NonworkingStatus.Approved && previousState != (short)NonworkingStatus.Approved)
            {
                // License approved - create TaskProgress records for working days
                await CreateLicenseTaskProgressRecords(userNonworkingDay);
            }
            else if (previousState == (short)NonworkingStatus.Approved && state != (short)NonworkingStatus.Approved)
            {
                // License revoked - remove TaskProgress records for this license
                await RemoveLicenseTaskProgressRecords(userNonworkingDay);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<FileResponse> DownloadFile(long Id)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var userNonworkingDay = await _context.UserNonworkingDays.FirstOrDefaultAsync(x => x.Id == Id);

            if (userNonworkingDay == null || !File.Exists(userNonworkingDay.Path))
            {
                throw new FileNotFoundException("El archivo no fue encontrado o no existe.");
            }

            var fileBytes = await File.ReadAllBytesAsync(userNonworkingDay.Path);
            var fileBase64 = Convert.ToBase64String(fileBytes);

            var fileName = Path.GetFileName(userNonworkingDay.Path);
            var fileExtension = Path.GetExtension(userNonworkingDay.Path).TrimStart('.');

            return new FileResponse
            {
                FileName = fileName,
                FileExtension = fileExtension,
                FileBytes = fileBase64
            };
        }

        public async Task UploadFile(long Id, string fileExtension, string fileBase64)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var userNonworkingDay = await _context.UserNonworkingDays.FirstOrDefaultAsync(x => x.Id == Id);
            var user = await _context.Users.FirstAsync(x => x.Id == userNonworkingDay.UserId);

            if (userNonworkingDay == null) return;

            var basePath = @$"c:\Luke\Clouseau\Archivos\{user.Username}\";
            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var fullPath = Path.Combine(basePath, fileName);

            if (!Directory.Exists(basePath)) Directory.CreateDirectory(basePath);

            if (!string.IsNullOrEmpty(fileBase64))
            {
                byte[] fileBytes = Convert.FromBase64String(fileBase64);
                await File.WriteAllBytesAsync(fullPath, fileBytes);
            }

            userNonworkingDay.Path = fullPath;

            _context.UserNonworkingDays.Update(userNonworkingDay);

            await _context.SaveChangesAsync();
        }

        private async Task CreateLicenseTaskProgressRecords(UserNonworkingDay userNonworkingDay)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            // Get holidays to exclude from working days
            var holidays = await _context.NonworkingDays.Select(x => x.Day.Date).ToListAsync();

            // Calculate working days within the license period
            var workingDays = GetWorkingDaysInPeriod(userNonworkingDay.DateFrom.Date, userNonworkingDay.DateTo.Date, holidays);

            // Create TaskProgress records for each working day
            var taskProgressRecords = workingDays.Select(day => new TaskProgress
            {
                TaskId = DEFAULT_LICENSE_TASK_ID,
                UserId = userNonworkingDay.UserId,
                Hours = DEFAULT_HOURS_PER_DAY,
                Comment = $"Licencia automática - {userNonworkingDay.Type?.Name ?? "Licencia"}",
                TaskTypeId = DEFAULT_LICENSE_TASK_TYPE_ID,
                Date = day
            }).ToList();

            if (taskProgressRecords.Any())
            {
                await _context.TaskProgresses.AddRangeAsync(taskProgressRecords);
                await _context.SaveChangesAsync();
            }
        }

        private async Task RemoveLicenseTaskProgressRecords(UserNonworkingDay userNonworkingDay)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            // Get holidays to exclude from working days
            var holidays = await _context.NonworkingDays.Select(x => x.Day.Date).ToListAsync();

            // Calculate working days within the license period
            var workingDays = GetWorkingDaysInPeriod(userNonworkingDay.DateFrom.Date, userNonworkingDay.DateTo.Date, holidays);

            // Find and remove TaskProgress records for this license period
            var existingRecords = await _context.TaskProgresses
                .Where(tp => tp.UserId == userNonworkingDay.UserId &&
                           tp.TaskId == DEFAULT_LICENSE_TASK_ID &&
                           workingDays.Contains(tp.Date.Date))
                .ToListAsync();

            if (existingRecords.Any())
            {
                _context.TaskProgresses.RemoveRange(existingRecords);
                await _context.SaveChangesAsync();
            }
        }

        private List<DateTime> GetWorkingDaysInPeriod(DateTime startDate, DateTime endDate, List<DateTime> holidays)
        {
            return Enumerable.Range(0, (endDate - startDate).Days + 1)
                            .Select(offset => startDate.AddDays(offset))
                            .Where(date => !IsWeekend(date) && !holidays.Contains(date))
                            .ToList();
        }

        private bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}
