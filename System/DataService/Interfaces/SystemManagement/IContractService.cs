using DataContext;

namespace DataService.Interfaces.Management
{
    public interface IContractService
    {
        Task<List<ClientContract>> GetAsync();

        Task<List<UserClientContract>> GetUserClientContractAsync();

        Task AddAsync(ClientContract contract);

        Task AddUserClientContractAsync(UserClientContract userClientContract);

        Task EditAsync(ClientContract contract);

        Task EditContractAsync(UserClientContract userContract);

        Task DefinitiveDeleteAsync(long contractId);

        Task DeleteUserContractAsync(long userContractId);

        

    }
}
