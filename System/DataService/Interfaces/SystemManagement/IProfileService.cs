

using DataContext;

namespace DataService.Interfaces.Management
{
    public interface IProfileService
    {
        Task<List<Profile>> GetAsync();

        Task AddAsync(Profile profile);

        Task EditAsync(Profile profile);

        Task DeleteAsync(long id);
    }
}
