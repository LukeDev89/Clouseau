using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces.GadgetExpress
{
    public interface IGadgetExpressService
    {
        Task<List<DataContext.GadgetExpress>> GetAll();
        Task Add(DataContext.GadgetExpress model);
        Task Update(DataContext.GadgetExpress model);
        Task Delete(long id);
        Task Send(DataContext.GadgetExpress model);
    }
}
