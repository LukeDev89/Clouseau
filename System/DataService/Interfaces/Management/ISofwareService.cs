using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces.Management
{
    public interface ISoftwareService
    {
        Task<List<InventorySoftware>> GetAsync();

        Task AddAsync(InventorySoftware inventory);

        Task EditAsync(InventorySoftware inventory);

        Task DefinitiveDeleteAsync(long modelId);

    }
}
