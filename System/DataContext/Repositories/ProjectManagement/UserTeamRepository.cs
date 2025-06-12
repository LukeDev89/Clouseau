using DataContext.Interfaces.ProjectManagement;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repositories.ProjectManagement
{
    public class UserTeamRepository : IUserTeamRepository
    {
        private readonly IDbContextFactory<ClouseauContext> _contextFactory;

        public UserTeamRepository(IDbContextFactory<ClouseauContext> context)
        {
            _contextFactory = context;
        }

        public async Task<List<UserTeam>> GetAsync()
        {
            using var _context = await _contextFactory.CreateDbContextAsync();
            return await _context.UserTeams
                
            //.Include(x => x.UserTeams).ThenInclude(u => u.User)
            //.Include(p => p.Projects)

            .Include(t => t.Team)
            .Include(t => t.User)

            .AsSplitQuery()        
            .ToListAsync();
        }

        //public async Task AddAsync(string name)
        //{
        //    using var _context = await _contextFactory.CreateDbContextAsync();

        //    await _context.Teams.AddAsync(new Team() { Name = name });
        //    await _context.SaveChangesAsync();
        //}

        //public async Task AddUsersTeamAsync(List<UserTeam> users)
        //{
        //    using var _context = await _contextFactory.CreateDbContextAsync();

        //    foreach (var user in users)
        //    {
        //        await _context.UserTeams.AddAsync(new UserTeam()
        //        {
        //            TeamId = user.TeamId,
        //            UserId = user.Id,
        //            Temporary = user.Temporary,
        //            RequireManagement = user.RequireManagement,
        //            Income = DateTime.Now
        //        });
        //    }

        //    await _context.SaveChangesAsync();
        //}

        //public async Task AddUsersAsync(long teamId, long userId, bool temporary, bool requireManagnment)
        //{
        //    using var _context = await _contextFactory.CreateDbContextAsync();

        //    var existInTeam = await _context.UserTeams.AnyAsync(x => x.TeamId == teamId && x.UserId == userId);

        //    if (existInTeam) return;

        //    await _context.UserTeams.AddAsync(new UserTeam()
        //    {
        //        TeamId = teamId,
        //        UserId = userId,
        //        Temporary = temporary,
        //        RequireManagement = requireManagnment,
        //        Income = DateTime.Now
        //    });

        //    await _context.SaveChangesAsync();
        //}

        public async Task EditAsync(UserTeam userTeam)
        {
            using var _context = _contextFactory.CreateDbContext();

            var UserTeamToUpdate = await _context.UserTeams.FirstOrDefaultAsync(x => x.Id == userTeam.Id);

            //ResourceToUpdate.UserId = resourceWarning.UserId;
            //ResourceToUpdate.State = resourceWarning.State;
            //ResourceToUpdate.Comment = resourceWarning.Comment;

         
            UserTeamToUpdate.RollOn = userTeam.RollOn;
            UserTeamToUpdate.RollOff = userTeam.RollOff;
            UserTeamToUpdate.AssignmentSold = userTeam.AssignmentSold;
            UserTeamToUpdate.RealAssignment = userTeam.RealAssignment;
            UserTeamToUpdate.Comments = userTeam.Comments;

            await _context.SaveChangesAsync();
        }
    }
}
