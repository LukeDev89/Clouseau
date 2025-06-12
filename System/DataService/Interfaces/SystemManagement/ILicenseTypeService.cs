using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces.Management
{
    public interface ILicenseTypeService
    {
        Task<List<LicenseType>> GetAsync();

        Task AddAsync(LicenseType license);

        Task EditAsync(LicenseType license);

        Task DeleteAsync(long id);
    }
}
