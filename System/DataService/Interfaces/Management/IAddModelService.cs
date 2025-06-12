using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces.Management
{
    public interface IAddModelService
    {
        Task<List<InventoryModel>> GetAsync();

        Task AddAsync(InventoryModel inventory);

        Task EditAsync(InventoryModel inventory);

        Task DefinitiveDeleteAsync(long modelId);

        Task<List<InventoryBrand>> GetBrands();

        Task<List<InventoryModel>> GetModels();
    }
}
