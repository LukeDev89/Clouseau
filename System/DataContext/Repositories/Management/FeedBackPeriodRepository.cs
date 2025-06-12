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
    public class FeedBackPeriodRepository : IFeedBackPeriodRepository
    {

        private readonly IDbContextFactory<ClouseauContext> _contextFactory;
        private readonly string RecoveryToken;

        public FeedBackPeriodRepository(IDbContextFactory<ClouseauContext> context, IConfiguration configuration)
        {
            _contextFactory = context;

            RecoveryToken = configuration.GetSection("Tokens:RecoveryToken").Value;
        }

        public async Task<List<FeedBackPeriod>> GetAsync()  
        {
            using var _context = await _contextFactory.CreateDbContextAsync();  

			var periods = await _context.FeedbackPeriods
                .Include(x => x.FeedbackStandarType).ThenInclude(x=> x.FeedbackStandar)
                .Include(x => x.FeedbackItem)
				.AsSplitQuery()
				.ToListAsync();
	
			return periods;
		}
   
        public async Task AddAsync(FeedBackPeriod period)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            await _context.FeedbackPeriods.AddAsync(period);

            await _context.SaveChangesAsync();
        }

		public async Task EditAsync(FeedBackPeriod period)
		{
			try
			{
				using var _context = await _contextFactory.CreateDbContextAsync();

                var p = await _context.FeedbackPeriods.FirstAsync(x => x.Id == period.Id);

                p.FeedbackStandarTypeId = period.FeedbackStandarTypeId;
				
				_context.Update(p);
				
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}		
    }
}
