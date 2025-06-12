using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IResourceWarningRepository
    {
        Task<List<ResourceWarning>> GetAsync();

        Task AddAsync(ResourceWarning resourceWarning);

        Task EditAsync(ResourceWarning resourceWarning);

		Task DefinitiveDeleteAsync(long resourceWarningId);
	}
}
