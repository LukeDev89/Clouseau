using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces.Management
{
    public interface IResourceWarningHistoryService
    {
        Task<List<ResourceWarningHistory>> GetAsync();

        Task AddAsync(ResourceWarningHistory resourceWarning);

        Task EditAsync(ResourceWarningHistory resourceWarning);
    }
}
