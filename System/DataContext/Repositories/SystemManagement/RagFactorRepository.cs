using DataContext;

using DataContext.Interfaces.Management;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Repositories.Management
{
    public class RagFactorRepository : IRagFactorRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;
        public RagFactorRepository(IDbContextFactory<ClouseauContext> context)
        {
            _contextFactory = context;
        }

        public async Task<List<RagFactor>> GetAsync()
        {
            using var _context = _contextFactory.CreateDbContext();

            return await _context.RagFactors.ToListAsync();
        }

        public async Task AddAsync(RagFactor factor)
        {
            using var _context = _contextFactory.CreateDbContext();
            await _context.AddAsync(factor);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(RagFactor factor)
        {
            using var _context = _contextFactory.CreateDbContext();
            var _factor = await _context.RagFactors.FirstAsync(x => x.Id == factor.Id);
            
            _factor.Id = factor.Id;
            _factor.Name = factor.Name;
            _factor.Description = factor.Description;
            _factor.Objetive = factor.Objetive;
            _factor.Active = factor.Active;

            _context.Update(_factor);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            using var _context = _contextFactory.CreateDbContext();
            var _factorActive = await _context.RagFactors.FirstAsync(x => x.Id == id);

            _factorActive.Active = false;

            _context.Update(_factorActive);

            await _context.SaveChangesAsync();
        }

        public async Task DefinitiveDeleteAsync(long id)
        {
            using var _context = _contextFactory.CreateDbContext();

            var factor = await _context.RagFactors.FirstAsync(x => x.Id == id);

            _context.RagFactors.Remove(factor);

            await _context.SaveChangesAsync();
        }

		
	}
}
