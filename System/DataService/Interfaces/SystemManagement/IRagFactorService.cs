

using DataContext;

namespace DataService.Interfaces.Management
{
    public interface IRagFactorService
    {
        Task<List<RagFactor>> GetAsync();

        Task AddAsync(RagFactor factor);

        Task EditAsync(RagFactor factor);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long id);
	}
}
