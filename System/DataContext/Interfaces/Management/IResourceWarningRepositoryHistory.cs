using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IResourceWarningHistoryRepository
    {
        Task<List<ResourceWarningHistory>> GetAsync();

        Task AddAsync(ResourceWarningHistory resourceWarning);

        Task EditAsync(ResourceWarningHistory resourceWarning);
    }
}
