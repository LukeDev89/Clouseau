using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces.Management
{
    public interface IFeedBackPeriodService
    {
        Task<List<FeedBackPeriod>> GetAsync();

        Task AddAsync(FeedBackPeriod period);

        Task EditAsync(FeedBackPeriod period);

    }
}
