using DataContext.Interfaces.Management;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Repositories.Management
{
    public class FeedBackCommentRepository : IFeedBackCommentRepository
    {

        private readonly IDbContextFactory<ClouseauContext> _contextFactory;
        private readonly string RecoveryToken;

        public FeedBackCommentRepository(IDbContextFactory<ClouseauContext> context, IConfiguration configuration)
        {
            _contextFactory = context;

            RecoveryToken = configuration.GetSection("Tokens:RecoveryToken").Value;
        }

        public async Task<List<FeedBackComment>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            var prueba = await _context.FeedbackComments.Include(x => x.User) .Include(x => x.FeedbackUser)
				.AsSplitQuery()
				.ToListAsync();
					
			return prueba;

		}

        public async Task AddAsync(FeedBackComment comment)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            await _context.FeedbackComments.AddAsync(comment);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(FeedBackComment comment)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();


            _context.Update(comment);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var standar = await _context.FeedbackComments.FirstAsync(x => x.Id == id);

            _context.Update(standar);

            await _context.SaveChangesAsync();
        }

		public async Task DefinitiveDeleteAsync(long commentId)
		{
			using var _context = await _contextFactory.CreateDbContextAsync();

			var comment = await _context.FeedbackComments.FirstAsync(x => x.Id == commentId);

			_context.FeedbackComments.Remove(comment);

			await _context.SaveChangesAsync();
		}
	}
}
