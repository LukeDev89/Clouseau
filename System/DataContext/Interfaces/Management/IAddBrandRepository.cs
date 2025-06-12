using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IAddBrandRepository
    {
        Task<List<InventoryBrand>> GetAsync();

        Task AddAsync(InventoryBrand inventory);

        Task EditAsync(InventoryBrand inventory);

        Task DefinitiveDeleteAsync(long brandId);

        Task<List<InventoryBrand>> GetBrands();
    }
}
