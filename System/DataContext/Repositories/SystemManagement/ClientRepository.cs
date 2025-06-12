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
    public class ClientRepository : IClientRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;
        public ClientRepository(IDbContextFactory<ClouseauContext> context)
        {
            _contextFactory = context;
        }

        public async Task<List<Client>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            return await _context.Clients.ToListAsync();
        }

        public async Task AddAsync(Client client)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            await _context.Clients.AddAsync(client);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Client client)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            client.Outcome = client.Active ? null : DateTime.Now;

            _context.Update(client);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var client = await _context.Clients.FirstAsync(x => x.Id == id);

            client.Active = false;
            client.Outcome = DateTime.Now;

            _context.Update(client);

            await _context.SaveChangesAsync();
        }
    }
}
