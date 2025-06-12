
using DataContext;
using DataContext.Interfaces.Management;
using DataService.Interfaces.Management;

namespace DataService.Services.Management
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _repository;

        public ProfileService(IProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Profile>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(Profile profile) => await _repository.AddAsync(profile);

        public async Task DeleteAsync(long id) => await _repository.DeleteAsync(id);

        public async Task EditAsync(Profile profile) => await _repository.EditAsync(profile);
    }
}
