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
    public class FeedBackStandarRepository : IFeedBackStandarRepository
    {

        private readonly IDbContextFactory<ClouseauContext> _contextFactory;
        private readonly string RecoveryToken;

        public FeedBackStandarRepository(IDbContextFactory<ClouseauContext> context, IConfiguration configuration)
        {
            _contextFactory = context;

            RecoveryToken = configuration.GetSection("Tokens:RecoveryToken").Value;
        }

        public async Task<List<FeedbackStandar>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            return await _context.FeedbackStandars.ToListAsync();
        }

        public async Task AddAsync(FeedbackStandar standar)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            await _context.FeedbackStandars.AddAsync(standar);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(FeedbackStandar standar)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();


            _context.Update(standar);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var standar = await _context.FeedbackStandars.FirstAsync(x => x.Id == id);

            standar.Active = false;


            _context.Update(standar);

            await _context.SaveChangesAsync();
        }

        public async Task DefinitiveDeleteAsync(long standarId) 
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var standar = await _context.FeedbackStandars.FirstAsync(x => x.Id == standarId);

            _context.FeedbackStandars.Remove(standar);

            await _context.SaveChangesAsync();
        }
    }
}
