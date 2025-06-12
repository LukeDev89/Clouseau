using DataContext.Interfaces.ProjectManagement;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repositories.ProjectManagement
{
    public class TeamRepository : ITeamRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;

        public TeamRepository(IDbContextFactory<ClouseauContext> context)
        {
            _contextFactory = context;
        }

        public async Task<List<Team>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            return await _context.Teams.Include(x => x.UserTeams).ThenInclude(u => u.User).Include(p => p.Projects).ToListAsync();
        }

        public async Task AddAsync(string name)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            await _context.Teams.AddAsync(new Team() { Name = name });
            await _context.SaveChangesAsync();
        }

        public async Task AddUsersTeamAsync(List<UserTeam> users) // ojo 
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            foreach (var user in users)
            {
                await _context.UserTeams.AddAsync(new UserTeam()
                {
                    TeamId = user.TeamId,
                    UserId = user.Id,
                    Temporary = user.Temporary,
                    RequireManagement = user.RequireManagement,
                    Income = DateTime.Now,
                    //RollOn = user.RollOn,
                    //RollOff = user.RollOff,
                    //AssignmentSold = user.AssignmentSold,
                    //RealAssignment = user.RealAssignment,
                    //Comments = user.Comments,

                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task AddUsersAsync(long teamId, long userId, bool temporary, bool requireManagnment)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var existInTeam = await _context.UserTeams.AnyAsync(x => x.TeamId == teamId && x.UserId == userId);

            if (existInTeam) return;

            await _context.UserTeams.AddAsync(new UserTeam()
            {
                TeamId = teamId,
                UserId = userId,
                Temporary = temporary,
                RequireManagement = requireManagnment,
                Income = DateTime.Now
            });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUsersAsync(long teamId, long userId)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var userTeam = await _context.UserTeams.FirstAsync(x => x.TeamId == teamId && x.UserId == userId);

            _context.UserTeams.Remove(userTeam);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(long teamId, string teamName, bool active)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var team = await _context.Teams.FirstAsync(x => x.Id == teamId);

            team.Name = teamName;
            team.Active = active;
            team.Deleted = null;

            _context.Teams.Update(team);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long teamId)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var team = await _context.Teams.FirstAsync(x => x.Id == teamId);

            team.Active = false;
            team.Deleted = DateTime.Now;

            _context.Teams.Update(team);

            await _context.SaveChangesAsync();
        }

        public async Task DefinitiveDeleteAsync(long teamId)
        {
            using var _context = await _contextFactory.CreateDbContextAsync();

            var team = await _context.Teams.FirstAsync(x => x.Id == teamId);

            _context.Teams.Remove(team);

            await _context.SaveChangesAsync();
        }
    }
}
