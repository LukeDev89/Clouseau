using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces.Management
{
    public interface ISystemConfigService
    {
        Task<List<SystemConfig>> GetAsync();

        Task AddAsync(SystemConfig config);

        Task EditAsync(SystemConfig config);
    }
}
