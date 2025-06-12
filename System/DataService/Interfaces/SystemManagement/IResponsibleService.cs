using DataContext;

namespace DataService.Interfaces.Management
{
    public interface IResponsibleService
    {
        Task<List<ClientResponsible>> GetAsync();

        Task AddAsync(ClientResponsible clientResponsible);

        Task EditAsync(ClientResponsible clientResponsible);

        Task DefinitiveDeleteAsync(long responsibleid);
    }
}
