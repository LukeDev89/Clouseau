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
    public class FeedBackStandarTypeRepository : IFeedBackStandarTypeRepository
    {

        private readonly IDbContextFactory<ClouseauContext> _contextFactory;
        private readonly string RecoveryToken;

        public FeedBackStandarTypeRepository(IDbContextFactory<ClouseauContext> context, IConfiguration configuration)
        {
            _contextFactory = context;

            RecoveryToken = configuration.GetSection("Tokens:RecoveryToken").Value;
        }

        public async Task<List<FeedbackStandarType>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            return await _context.FeedbackStandarTypes
				.Include(x => x.FeedbackStandar)
				.ToListAsync();     
        }

        public async Task AddAsync(FeedbackStandarType standarType)            
        {
            try
            {
                using var _context = await _contextFactory.CreateDbContextAsync();
                FeedbackStandarType userCreate = new FeedbackStandarType 
                {
					FeedbackStandarId = standarType.FeedbackStandarId,
                    Active = standarType.Active,
                    Description = standarType.Description,
                    Name = standarType.Name
                };

                await _context.FeedbackStandarTypes.AddAsync(userCreate);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task EditAsync(FeedbackStandarType standarType)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();


            _context.Update(standarType);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var standar = await _context.FeedbackStandarTypes.FirstAsync(x => x.Id == id);

            standar.Active = false;


            _context.Update(standar);

            await _context.SaveChangesAsync();
        }

        public async Task DefinitiveDeleteAsync(long standarTypeId)  
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var standarType = await _context.FeedbackStandarTypes.FirstAsync(x => x.Id == standarTypeId);

            _context.FeedbackStandarTypes.Remove(standarType);

            await _context.SaveChangesAsync();
        }
      
        public async Task OnSelectedStatusChanged (FeedbackStandarType feedbackStatus)

        {
            try
            {
                using var _context = await _contextFactory.CreateDbContextAsync();
                FeedbackStandarType userCreate = new FeedbackStandarType
                {
                    Id = feedbackStatus.Id,                 
                    Name = feedbackStatus.Name
                };

                await _context.FeedbackStandarTypes.AddAsync(userCreate);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
