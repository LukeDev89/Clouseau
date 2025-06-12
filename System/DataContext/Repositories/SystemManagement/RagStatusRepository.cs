using DataContext.Interfaces.Management;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repositories.Management
{
    public class RagStatusRepository : IRagStatusRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;

        public RagStatusRepository(IDbContextFactory<ClouseauContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<RagStatus>> GetAsync()
        {
            using var _context = _contextFactory.CreateDbContext();

            return await _context.RagStatuses
            .Include(x => x.Factor)
            .Include(x => x.User)
            .Include(x => x.Project)
            .Include(x => x.Template)
			.AsSplitQuery()
			.ToListAsync();
        }

        public async Task AddAsync(RagStatus ragStatus)
        {
            using var _context = _contextFactory.CreateDbContext();

            await _context.RagStatuses.AddAsync(ragStatus);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(RagStatus ragStatus)
        {
            using var _context = _contextFactory.CreateDbContext();

            var status = await _context.RagStatuses.FirstAsync(x => x.Id == ragStatus.Id);

            status.CalificationId = ragStatus.CalificationId;
            status.Comment = ragStatus.Comment;
            status.UserId = ragStatus.UserId;
            status.Date = DateTime.Now;
            
            _context.RagStatuses.Update(status);

            await _context.SaveChangesAsync();
        }
    }
}
