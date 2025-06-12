
using DataContext;
using DataContext.Interfaces.ProjectManagement;
using DataService.Interfaces.ProjectManagement;

namespace DataService.Services.ProjectManagement
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _repository;

        public TeamService(ITeamRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Team>> GetAsync() => await _repository.GetAsync();

        public async Task AddUsersTeamAsync(List<UserTeam> users) => await _repository.AddUsersTeamAsync(users);

        public async Task AddUsersAsync(long teamId, long userId, bool temporary, bool requireManagnment) => await _repository.AddUsersAsync(teamId, userId, temporary, requireManagnment);

        public async Task DeleteUsersAsync(long teamId, long userId) => await _repository.DeleteUsersAsync(teamId, userId);

        public async Task AddAsync(string name) => await _repository.AddAsync(name);

        public async Task DeleteAsync(long teamId) => await _repository.DeleteAsync(teamId);

        public async Task EditAsync(long teamId, string teamName, bool active) => await _repository.EditAsync(teamId, teamName, active);
    
        public async Task DefinitiveDeleteAsync(long teamId) => await _repository.DefinitiveDeleteAsync(teamId);
    }
}
