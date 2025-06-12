using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces.Management
{
    public interface IRagStatusService
    {
        Task<List<RagStatus>> GetAsync();

        Task AddAsync(RagStatus ragStatus);

        Task EditAsync(RagStatus ragStatus);
    }
}
