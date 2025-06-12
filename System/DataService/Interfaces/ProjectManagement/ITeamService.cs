

using DataContext;

namespace DataService.Interfaces.ProjectManagement
{
    public interface ITeamService
    {
        Task<List<Team>> GetAsync();

        Task AddAsync(string name);

        Task AddUsersTeamAsync(List<UserTeam> users);

        Task AddUsersAsync(long teamId, long userId, bool temporary, bool requireManagnment);

        Task DeleteUsersAsync(long teamId, long userId);

        Task EditAsync(long teamId, string teamName, bool active);

        Task DeleteAsync(long teamId);

        Task DefinitiveDeleteAsync(long teamId);
    }
}
