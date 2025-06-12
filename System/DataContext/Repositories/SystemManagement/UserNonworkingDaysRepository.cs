using DataContext.Interfaces.Management;
using DataModel.Enums;
using DataModel.Response;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace DataContext.Repositories.Management
{
    public class UserNonworkingDaysRepository : IUserNonworkingDaysRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;

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
            userNonworkingDay.State = state;
            userNonworkingDay.CommentCfo = commentCfo;

            _context.UserNonworkingDays.Update(userNonworkingDay);

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
    }
}
