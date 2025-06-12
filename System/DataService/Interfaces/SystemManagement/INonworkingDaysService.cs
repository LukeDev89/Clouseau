using DataContext;


namespace DataService.Interfaces.Management
{
    public interface INonworkingDaysService
    {
        Task<List<NonworkingDay>> GetAsync();

        Task AddAsync(NonworkingDay nonworkingDay);

        Task DeleteAsync(long Id);
    }
}
