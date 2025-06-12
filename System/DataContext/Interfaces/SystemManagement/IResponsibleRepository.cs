

namespace DataContext.Interfaces.Management
{
    public interface IResponsibleRepository
    {
        Task<List<ClientResponsible>> GetAsync();

        Task AddAsync(ClientResponsible clientResponsible);

        Task EditAsync(ClientResponsible clientResponsible);

        Task DefinitiveDeleteAsync(long responsibleid);
    }
}
