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
    public class ResponsibleRepository : IResponsibleRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;
        public ResponsibleRepository(IDbContextFactory<ClouseauContext> context)
        {
            _contextFactory = context;
        }

        public async Task<List<ClientResponsible>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
			return await _context.ClientResponsibles.
				Include(x => x.Client).
				ToListAsync();			
        }

        public async Task AddAsync(ClientResponsible clientResponsible)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            await _context.ClientResponsibles.AddAsync(clientResponsible);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(ClientResponsible clientResponsible)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();			

			var Responsible = await _context.ClientResponsibles.FirstOrDefaultAsync(x => x.Id == clientResponsible.Id);

			Responsible.Name = clientResponsible.Name;
			Responsible.Mail = clientResponsible.Mail;
			Responsible.ClientId = clientResponsible.ClientId;

			//_context.Update(clientResponsible);
            await _context.SaveChangesAsync();
        }

		public async Task DefinitiveDeleteAsync(long responsibleId)
		{
			using var _context = await _contextFactory.CreateDbContextAsync();

			var responsible = await _context.ClientResponsibles.FirstAsync(x => x.Id == responsibleId);

			_context.ClientResponsibles.Remove(responsible);

			await _context.SaveChangesAsync();
		}
	}
}
