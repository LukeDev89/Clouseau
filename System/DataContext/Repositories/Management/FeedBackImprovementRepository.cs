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
    public class FeedBackImprovementRepository : IFeedBackImprovementRepository
    {

        private readonly IDbContextFactory<ClouseauContext> _contextFactory;
        private readonly string RecoveryToken;

        public FeedBackImprovementRepository(IDbContextFactory<ClouseauContext> context, IConfiguration configuration)
        {
            _contextFactory = context;

            RecoveryToken = configuration.GetSection("Tokens:RecoveryToken").Value;
        }

        public async Task<List<FeedBackImprovement>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            return await _context.FeedbackImprovements.ToListAsync();
        }

        public async Task AddAsync(FeedBackImprovement improvement)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            await _context.FeedbackImprovements.AddAsync(improvement);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(FeedBackImprovement improvement)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            _context.Update(improvement);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var standar = await _context.FeedbackImprovements.FirstAsync(x => x.Id == id);

            standar.Active = false; 
            _context.Update(standar);

            await _context.SaveChangesAsync();
        }

        public async Task DefinitiveDeleteAsync(long improvementId)  
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var improvement = await _context.FeedbackImprovements.FirstAsync(x => x.Id == improvementId);

            _context.FeedbackImprovements.Remove(improvement);

            await _context.SaveChangesAsync();
        }
    }
}
