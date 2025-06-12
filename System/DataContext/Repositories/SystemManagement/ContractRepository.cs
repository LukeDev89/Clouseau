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
	public class ContractRepository : IContractRepository
	{
		private readonly IDbContextFactory<ClouseauContext> _contextFactory;
		public ContractRepository(IDbContextFactory<ClouseauContext> context)
		{
			_contextFactory = context;
		}

		public async Task<List<ClientContract>> GetAsync()
		{
			using var _context = await _contextFactory.CreateDbContextAsync();
			return await _context.ClientContracts
				.Include(x => x.Client)
				.ToListAsync();
		}

        public async Task<List<UserClientContract>> GetUserClientContractAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            return await _context.UserClientContracts
				.Include(x => x.ClientContract)
                .ToListAsync();
        }

        public async Task AddAsync(ClientContract clientContract)
		{
			using var _context = await _contextFactory.CreateDbContextAsync();

			await _context.ClientContracts.AddAsync(clientContract);

			await _context.SaveChangesAsync();
		}

        public async Task AddUserClientContractAsync(UserClientContract userClientContract)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            await _context.UserClientContracts.AddAsync(userClientContract);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(ClientContract clientContract)
		{
			using var _context = await _contextFactory.CreateDbContextAsync();

			var Contract = await _context.ClientContracts.FirstOrDefaultAsync(x => x.Id == clientContract.Id);

			Contract.Name = clientContract.Name;
			Contract.Description = clientContract.Description;
			Contract.ClientId = clientContract.ClientId;

			//_context.Update(clientContract);
			await _context.SaveChangesAsync();
		}

        public async Task EditContractAsync(UserClientContract userContract)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var Contract = await _context.UserClientContracts.FirstOrDefaultAsync(x => x.Id == userContract.Id);

            Contract.ClientContractId = userContract.ClientContractId;
            Contract.Hours = userContract.Hours;

            await _context.SaveChangesAsync();
        }

        public async Task DefinitiveDeleteAsync(long contractId)
		{
			using var _context = await _contextFactory.CreateDbContextAsync();

			var contract = await _context.ClientContracts.FirstAsync(x => x.Id == contractId);

			_context.ClientContracts.Remove(contract);

			await _context.SaveChangesAsync();
		}

        public async Task DeleteUserContractAsync(long userContractId)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var userContract = await _context.UserClientContracts.FirstAsync(x => x.Id == userContractId);

            _context.UserClientContracts.Remove(userContract);

            await _context.SaveChangesAsync();
        }

    }
}
