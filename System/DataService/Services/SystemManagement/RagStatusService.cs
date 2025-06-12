using DataContext;
using DataContext.Interfaces.Management;
using DataService.Interfaces.Management;

namespace DataService.Services.Management
{
    public class RagStatusService : IRagStatusService
    {
        private readonly IRagStatusRepository _repository;

        public RagStatusService(IRagStatusRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RagStatus>> GetAsync() => await _repository.GetAsync();
        
        public async Task AddAsync(RagStatus ragStatus) => await _repository.AddAsync(ragStatus);

        public async Task EditAsync(RagStatus ragStatus) => await _repository.EditAsync(ragStatus);
    }
}
