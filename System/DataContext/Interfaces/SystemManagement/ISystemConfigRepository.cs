namespace DataContext.Interfaces.Management
{
    public interface ISystemConfigRepository
    {
        Task<List<SystemConfig>> GetAsync();

        Task<SystemConfig> GetByKeyAsync(string key);

        Task AddAsync(SystemConfig config);

        Task EditAsync(SystemConfig config);
    }
}
