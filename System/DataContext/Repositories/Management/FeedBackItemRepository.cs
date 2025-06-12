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
    public class FeedBackItemRepository : IFeedBackItemRepository
    {

        private readonly IDbContextFactory<ClouseauContext> _contextFactory;
        private readonly string RecoveryToken;

        public FeedBackItemRepository(IDbContextFactory<ClouseauContext> context, IConfiguration configuration)
        {
            _contextFactory = context;

            RecoveryToken = configuration.GetSection("Tokens:RecoveryToken").Value;
        }

		public async Task<List<FeedbackItem>> GetAsync()
		{
			using var _context = await _contextFactory.CreateDbContextAsync();
			return await _context.FeedbackItems
				   .Include(x => x.FeedbackStandar)
				.ToListAsync();
		}

		public async Task AddAsync(FeedbackItem item)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            await _context.FeedbackItems.AddAsync(item);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(FeedbackItem standar)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();       

            _context.Update(standar);         
              
            await _context.SaveChangesAsync();
        }


		public async Task DeleteAsync(long id)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var item = await _context.FeedbackItems.FirstAsync(x => x.Id == id);

            item.Active = false;


            _context.Update(item);

            await _context.SaveChangesAsync();
        }

        public async Task DefinitiveDeleteAsync(long itemId)  
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var item = await _context.FeedbackItems.FirstAsync(x => x.Id == itemId);

            _context.FeedbackItems.Remove(item);

            await _context.SaveChangesAsync();
        }
    }
}
