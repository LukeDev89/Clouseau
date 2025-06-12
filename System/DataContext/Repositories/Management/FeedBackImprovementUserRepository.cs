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
	public class FeedBackImprovementUserRepository : IFeedBackImprovementUserRepository
	{

		private readonly IDbContextFactory<ClouseauContext> _contextFactory;
		private readonly string RecoveryToken;

		public FeedBackImprovementUserRepository(IDbContextFactory<ClouseauContext> context, IConfiguration configuration)
		{
			_contextFactory = context;

			RecoveryToken = configuration.GetSection("Tokens:RecoveryToken").Value;
		}

		public async Task<List<FeedbackImprovementUser>> GetAsync() 
		{
			using var _context = await _contextFactory.CreateDbContextAsync();

			return await _context.FeedbackImprovementUsers
			   
				.Include(x => x.FeedbackUser)
				.ThenInclude(x => x.User)
			   .Include(x => x.FeedbackImprovement)
			   .AsSplitQuery()
			   .ToListAsync();

		}

		public async Task AddAsync(FeedbackImprovementUser improvementUser)
		{
			using var _context = await _contextFactory.CreateDbContextAsync();

			await _context.FeedbackImprovementUsers.AddAsync(improvementUser);

			await _context.SaveChangesAsync();
		}

		public async Task EditAsync(FeedbackImprovementUser improvementUser)
		{
			using var _context = await _contextFactory.CreateDbContextAsync();


			_context.Update(improvementUser);

			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(long id)
		{
			using var _context = await _contextFactory.CreateDbContextAsync();

			var standar = await _context.FeedbackImprovementUsers.FirstAsync(x => x.Id == id);


			_context.Update(standar);

			await _context.SaveChangesAsync();
		}

		public async Task DefinitiveDeleteAsync(long improvementUserId)
		{
			using var _context = await _contextFactory.CreateDbContextAsync();

			var improvementUser = await _context.FeedbackImprovementUsers.FirstAsync(x => x.Id == improvementUserId);

			_context.FeedbackImprovementUsers.Remove(improvementUser);

			await _context.SaveChangesAsync();
		}
	}
}
