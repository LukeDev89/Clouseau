using DataContext;
using DataContext.Interfaces.Management;
using DataService.Interfaces.Management;

namespace DataService.Services.Management
{
    public class RagTemplatesService : IRagTemplatesService
    {
        private readonly IRagTemplatesRepository _repository;

        public RagTemplatesService(IRagTemplatesRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RagTemplate>> GetAsync() => await _repository.GetAsync();

		public async Task AddAsync(RagTemplate ragTemplate) => await _repository.AddAsync(ragTemplate);


		public async Task UpdateState(long id, string name, DateTime period)
		{
			await _repository.UpdateState(id, name, period);
		}

		public async Task<List<RagFactor>> GetTemplateFactorById(long id) => await _repository.GetTemplateFactorById(id);

		public async Task DeleteFactorAsync(long id, long factorId) => await _repository.DeleteFactorAsync(id, factorId);

		public async Task DeleteFactor(long id) => await _repository.DeleteFactor(id);

		public async Task AddFactorsAsync(long templateId, long factorId) => await _repository.AddFactorsAsync(templateId, factorId);

        public async Task EditAsync(long id, string name, bool active) => await _repository.EditAsync(id, name, active);
		
		public async Task DefinitiveDeleteAsync(long templateId) => await _repository.DefinitiveDeleteAsync(templateId);

        public async Task Execute(long templateId) => await _repository.Execute(templateId);

    }
}
