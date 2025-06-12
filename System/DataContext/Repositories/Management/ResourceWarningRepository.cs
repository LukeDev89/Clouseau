using DataContext.Interfaces.Management;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataContext.Repositories.Management
{
    public class ResourceWarningRepository : IResourceWarningRepository
    {

        private readonly IDbContextFactory<ClouseauContext> _contextFactory;
        private readonly string RecoveryToken;

        public ResourceWarningRepository(IDbContextFactory<ClouseauContext> context, IConfiguration configuration)
        {
            _contextFactory = context;

            RecoveryToken = configuration.GetSection("Tokens:RecoveryToken").Value;
        }

        public async Task<List<ResourceWarning>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            return await _context.ResourceWarnings

            .Include(x => x.User)      
            .Include(x => x.Advertiser)
			.Include(x => x.ResourceWarningHistories).ThenInclude(x => x.User)
			   .AsSplitQuery()
               .ToListAsync();
        }

        public async Task AddAsync(ResourceWarning resourceWarning)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            await _context.ResourceWarnings.AddAsync(resourceWarning);

            await _context.SaveChangesAsync();
        }

		public async Task EditAsync(ResourceWarning resourceWarning)
		{
			using var _context = _contextFactory.CreateDbContext();

			var ResourceToUpdate = await _context.ResourceWarnings.FirstOrDefaultAsync(x => x.Id == resourceWarning.Id);

			ResourceToUpdate.UserId = resourceWarning.UserId;
            ResourceToUpdate.State = resourceWarning.State;
            ResourceToUpdate.Comment = resourceWarning.Comment;
            
			await _context.SaveChangesAsync();
		}

		public async Task DefinitiveDeleteAsync(long resourceWarningId)
		{
			using var _context = await _contextFactory.CreateDbContextAsync();

			var fuser = _context.ResourceWarnings.FirstAsync(x => x.Id == resourceWarningId);
			await fuser;

			var fcomment = _context.ResourceWarningHistories.Where(x => x.ResourceWarningId == resourceWarningId).ToListAsync();
			await fcomment;

			if (fcomment.Result != null)
			{
				_context.ResourceWarningHistories.RemoveRange(fcomment.Result);
				await _context.SaveChangesAsync();
			}			

			_context.ResourceWarnings.Remove(fuser.Result);
			await _context.SaveChangesAsync();
		}
	}
}
