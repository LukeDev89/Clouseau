

namespace DataContext.Interfaces.Management
{
    public interface IRagFactorRepository
    {
        Task<List<RagFactor>> GetAsync();

        Task AddAsync(RagFactor factor);

        Task EditAsync(RagFactor factor);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long id);

		

	}
}
