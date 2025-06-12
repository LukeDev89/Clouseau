using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces
{
    public interface IGadgetExpressRepository
    {
        Task<List<GadgetExpress>> GetAll();
        Task Add(GadgetExpress model);
        Task Update(GadgetExpress model);
        Task Delete(long id);
        Task Send(GadgetExpress model);
    }
}
