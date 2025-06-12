using DataContext;
using DataContext.Interfaces.Management;
using DataModel.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repositories.Management
{
    public class UsersAccountRepository : IUsersAccountRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;
        public UsersAccountRepository(IDbContextFactory<ClouseauContext> context)
        {
            _contextFactory = context;
        }

        public async Task<List<UserAccount>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            return await _context.UserAccounts.Include(x => x.User).ToListAsync();
        }

        public async Task DeleteAsync(long id)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var a = await _context.UserAccounts.FirstAsync(x => x.Id == id);

            _context.UserAccounts.Remove(a);

            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(long userId, string username, string email, AccountType type)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            await _context.UserAccounts.AddAsync(new UserAccount()
            {
                UserId = userId,
                Username = username,
                Email = email,
                ServiceId = (short)type
            });

            await _context.SaveChangesAsync();
        }
    }
}
