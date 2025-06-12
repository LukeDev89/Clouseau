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
    public class ProfileRepository : IProfileRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;
        public ProfileRepository(IDbContextFactory<ClouseauContext> context)
        {
            _contextFactory = context;
        }

        public async Task<List<Profile>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            return await _context.Profiles
                .Include(x => x.ProfilePermissions)
                .ToListAsync();
        }

        public async Task AddAsync(Profile profile)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            await _context.AddAsync(profile);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Profile profile)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            //var profile = await _context.Profiles.FirstAsync(x => x.Id == id);

            //profile.Name = name;
            //profile.Description = description;
            //profile.Active = active;

            _context.Update(profile);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            var profile = await _context.Profiles.FirstAsync(x => x.Id == id);

            profile.Active = false;

            _context.Update(profile);

            await _context.SaveChangesAsync();
        }
    }
}
