using DataContext;

using DataContext.Interfaces.Management;
using Microsoft.EntityFrameworkCore;
namespace DataContext.Interfaces.Management
{
    public interface ILicenseTypeRepository
    {
        Task<List<LicenseType>> GetAsync();

        Task AddAsync(LicenseType license);

        Task EditAsync(LicenseType license);

        Task DeleteAsync(long id);
    }
}
