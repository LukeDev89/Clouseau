

namespace DataContext.Interfaces.Management
{
    public interface IContractRepository
    {
        Task<List<ClientContract>> GetAsync();

        Task<List<UserClientContract>> GetUserClientContractAsync();

        Task AddAsync(ClientContract clientContract);

        Task AddUserClientContractAsync(UserClientContract userClientContract);

        Task EditAsync(ClientContract clientContract);

        Task EditContractAsync(UserClientContract userContract);

        Task DefinitiveDeleteAsync(long contractId);

        Task DeleteUserContractAsync(long userContractId);
    }
}
