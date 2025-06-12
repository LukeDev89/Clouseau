
using DataContext.Interfaces.Management;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repositories.Management
{
    public class NonworkingDaysRepository : INonworkingDaysRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;

        public NonworkingDaysRepository(IDbContextFactory<ClouseauContext> context)
        {
            _contextFactory = context;
        }

        public async Task<List<NonworkingDay>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            return await _context.NonworkingDays.ToListAsync();
        }

        public async Task AddAsync(NonworkingDay nonworkingDay)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            await _context.NonworkingDays.AddAsync(nonworkingDay);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long Id)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var nonworkingDay = await _context.NonworkingDays.FirstAsync(x => x.Id == Id);

            _context.Remove(nonworkingDay);

            await _context.SaveChangesAsync();
        }
        
		public async Task UpdateState(long Id, short state, string commentCfo)
		{
			using var _context = _contextFactory.CreateDbContext();

			var userNonworkingDay = await _context.UserNonworkingDays.FirstAsync(x => x.Id == Id);
			userNonworkingDay.State = state;
			userNonworkingDay.CommentCfo = commentCfo;

			_context.UserNonworkingDays.Update(userNonworkingDay);

			await _context.SaveChangesAsync();
		}
	}
}
