

namespace DataContext.Interfaces.Management
{
    public interface INonworkingDaysRepository
    {
        Task<List<NonworkingDay>> GetAsync();

        Task AddAsync(NonworkingDay nonworkingDay);

        Task DeleteAsync(long Id);
    }
}
