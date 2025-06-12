
using DataContext;
using DataContext.Interfaces.Management;
using DataService.Interfaces.Management;

namespace DataService.Services.Management
{
    public class ResponsibleService : IResponsibleService
    {
        private readonly IResponsibleRepository _repository;

        public ResponsibleService(IResponsibleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ClientResponsible>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(ClientResponsible clientResponsible) => await _repository.AddAsync(clientResponsible);

        public async Task EditAsync(ClientResponsible clientResponsible) => await _repository.EditAsync(clientResponsible);

        public async Task DefinitiveDeleteAsync(long responsibleid) => await _repository.DefinitiveDeleteAsync(responsibleid);
    }
}
