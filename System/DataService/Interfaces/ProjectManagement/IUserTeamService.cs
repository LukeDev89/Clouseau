

using DataContext;

namespace DataService.Interfaces.ProjectManagement
{
    public interface IUserTeamService
    {
        Task<List<UserTeam>> GetAsync();

        //Task AddAsync(string name);

        //Task AddUsersTeamAsync(List<UserTeam> users);

        //Task AddUsersAsync(long teamId, long userId, bool temporary, bool requireManagnment);

        Task EditAsync(UserTeam userTeam);

    }
}
