using DataContext;
using DataContext.Extension;
using DataContext.Interfaces.Management;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataContext.Repositories.Management
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;
        private readonly string RecoveryToken;

        public UserRepository(IDbContextFactory<ClouseauContext> context, IConfiguration configuration)
        {
            _contextFactory = context;

            RecoveryToken = configuration.GetSection("Tokens:RecoveryToken").Value;
        }

        public async Task<List<User>> GetAsync()
        {
            try
            {
                using var _context = await _contextFactory.CreateDbContextAsync();
                var users = await _context.Users
                    .Include(x => x.Profile).ThenInclude(x => x.ProfilePermissions).ThenInclude(x => x.Section)
                    .Include(x => x.Role)
                    .Include(x => x.TaskProgresses)
                    .Include(x => x.UserTeams)
                    .Include(x => x.UserAccounts)
                    .Include(x => x.UserNonworkingDays)
                    .Include(x => x.CustomPermissions).ThenInclude(x => x.Section)
                    .AsSplitQuery()
                    .ToListAsync();

                return users;
            }
            catch (Exception e)
            {
                return new List<User>();
            }
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            return await _context.Users
                .Include(x => x.UserTeams)
                    .ThenInclude(z => z.Team)
                    .ThenInclude(z => z.Projects)
                .Include(x => x.Profile).ThenInclude(x => x.ProfilePermissions).ThenInclude(x => x.Section)
                .Include(x => x.CustomPermissions).ThenInclude(x => x.Section)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task AddAsync(User user)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            user.Password = PasswordHelper.HashPassword(user.Password);
            user.Superior = user.Superior < 0 ? null : user.Superior;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(User user)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var userToEdit = _context.Users.FirstOrDefault(x => x.Id == user.Id);

            userToEdit.Firstname = user.Firstname;
            userToEdit.Lastname = user.Lastname;
            userToEdit.Email = user.Email;
            userToEdit.Username = user.Username;
            userToEdit.IsActive = user.IsActive;
            userToEdit.Birth = user.Birth;
            userToEdit.PhoneNumber = user.PhoneNumber;
            userToEdit.Superior = user.Superior != null && user.Superior < 0 ? null : user.Superior;
            userToEdit.SuperiorNavigation = null;
            userToEdit.InverseSuperiorNavigation = null!;
            userToEdit.RoleId = user.RoleId;
            userToEdit.Province = user.Province;
            userToEdit.IsExternal = user.IsExternal;
            userToEdit.ExternalDetail = user.ExternalDetail;
            userToEdit.ProfileId = user.ProfileId;
            userToEdit.Seniority = user.Seniority;

            _context.Users.Update(userToEdit);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var u = await _context.Users.FirstAsync(x => x.Id == id);
            u.IsActive = false;
            u.Outcome = DateTime.Now;

            _context.Users.Update(u);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ResetPassword(string username)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var u = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (u == null) return false;

            u.Password = PasswordHelper.HashPassword(RecoveryToken);

            _context.Users.Update(u);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<bool> ChangePassword(long id, string password)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var u = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (u == null) return false;

            u.Password = PasswordHelper.HashPassword(password);

            _context.Users.Update(u);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
