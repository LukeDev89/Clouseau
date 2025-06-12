
using DataContext;
using DataContext.Interfaces.ProjectManagement;
using DataService.Interfaces.ProjectManagement;

namespace DataService.Services.ProjectManagement
{
    public class UserTeamService : IUserTeamService
    {
        private readonly IUserTeamRepository _repository;

        public UserTeamService(IUserTeamRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UserTeam>> GetAsync() => await _repository.GetAsync();

        //public async Task AddUsersTeamAsync(List<UserTeam> users) => await _repository.AddUsersTeamAsync(users);

        //public async Task AddUsersAsync(long teamId, long userId, bool temporary, bool requireManagnment) => await _repository.AddUsersAsync(teamId, userId, temporary, requireManagnment);

        //public async Task AddAsync(string name) => await _repository.AddAsync(name);

        public async Task EditAsync(UserTeam userTeam) => await _repository.EditAsync(userTeam);   

    }
}
