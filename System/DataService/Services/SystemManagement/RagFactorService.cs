
using DataContext;
using DataContext.Interfaces.Management;
using DataService.Interfaces.Management;

namespace DataService.Services.Management
{
    public class RagFactorService : IRagFactorService
    {
        private readonly IRagFactorRepository _repository;

        public RagFactorService(IRagFactorRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RagFactor>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(RagFactor factor) => await _repository.AddAsync(factor);

        public async Task DeleteAsync(long id) => await _repository.DeleteAsync(id);

        public async Task EditAsync(RagFactor factor) => await _repository.EditAsync(factor);
        
        public async Task DefinitiveDeleteAsync(long id) => await _repository.DefinitiveDeleteAsync(id);

		
	}
}
