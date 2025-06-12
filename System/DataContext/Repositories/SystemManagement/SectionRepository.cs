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
    public class SectionRepository : ISectionRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;
        public SectionRepository(IDbContextFactory<ClouseauContext> context)
        {
            _contextFactory = context;
        }

        public async Task<List<Section>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            return await _context.Sections.ToListAsync();
        }

        public async Task<List<CustomPermission>> GetCustomPermissionAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            return await _context.CustomPermissions
                .Include(x => x.Section)
                .ToListAsync();

        }

        public async Task AddCustomPermissionAsync(CustomPermission customPermission)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            await _context.CustomPermissions.AddAsync(customPermission);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomPermissionAsync(long permissionId)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var permission = await _context.CustomPermissions.FirstAsync(x => x.Id == permissionId);

            _context.CustomPermissions.Remove(permission);

            await _context.SaveChangesAsync();
        }

        public async Task<List<ProfilePermission>> GetProfilePermissionAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            return await _context.ProfilePermissions
                .Include(x => x.Section)
                .Include(x => x.Profile)
                .ToListAsync();

        }

        public async Task AddProfilePermissionAsync(ProfilePermission profilePermission)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            await _context.ProfilePermissions.AddAsync(profilePermission);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteProfilePermissionAsync(long permissionId)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var permission = await _context.ProfilePermissions.FirstAsync(x => x.Id == permissionId);

            _context.ProfilePermissions.Remove(permission);

            await _context.SaveChangesAsync();
        }

    }
}
