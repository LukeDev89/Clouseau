using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface ISoftwareRepository
    {
        Task<List<InventorySoftware>> GetAsync();

        Task AddAsync(InventorySoftware inventory);

        Task EditAsync(InventorySoftware inventory);

        Task DefinitiveDeleteAsync(long brandId);

       
    }
}
