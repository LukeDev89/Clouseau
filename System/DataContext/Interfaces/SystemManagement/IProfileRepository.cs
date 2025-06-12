

namespace DataContext.Interfaces.Management
{
    public interface IProfileRepository
    {
        Task<List<Profile>> GetAsync();

        Task AddAsync(Profile profile);

        Task EditAsync(Profile profile);

        Task DeleteAsync(long id);
    }
}
