
using DataContext;
using DataContext.Interfaces.Management;
using DataService.Interfaces.Management;

namespace DataService.Services.Management
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _repository;

        public ContractService(IContractRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ClientContract>> GetAsync() => await _repository.GetAsync();

        public async Task<List<UserClientContract>> GetUserClientContractAsync() => await _repository.GetUserClientContractAsync();

        public async Task AddAsync(ClientContract contract) => await _repository.AddAsync(contract);
        public async Task AddUserClientContractAsync(UserClientContract userClientContract) => await _repository.AddUserClientContractAsync(userClientContract);
        
        public async Task EditAsync(ClientContract contract) => await _repository.EditAsync(contract);

        public async Task EditContractAsync(UserClientContract userContract) => await _repository.EditContractAsync(userContract);

        public async Task DefinitiveDeleteAsync(long contractId) => await _repository.DefinitiveDeleteAsync(contractId);

        public async Task DeleteUserContractAsync(long userContractId) => await _repository.DeleteUserContractAsync(userContractId);
    }
}
