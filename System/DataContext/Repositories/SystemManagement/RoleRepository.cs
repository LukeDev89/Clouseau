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
    public class RoleRepository : IRoleRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;
        public RoleRepository(IDbContextFactory<ClouseauContext> context)
        {
            _contextFactory = context;
        }

        public async Task<List<Role>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            return await _context.Roles.ToListAsync();
        }

        public async Task AddAsync(Role role)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Role role)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var _role = await _context.Roles.FirstAsync(x => x.Id == role.Id);

            _role.Name = role.Name;
            _role.Description = role.Description;
            _role.Active = role.Active;
            _role.Hierarchy = role.Hierarchy;

            _context.Update(_role);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var role = await _context.Roles.FirstAsync(x => x.Id == id);

            role.Active = false;

            _context.Update(role);

            await _context.SaveChangesAsync();
        }
    }
}
