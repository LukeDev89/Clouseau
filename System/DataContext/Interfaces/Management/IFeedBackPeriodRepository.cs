using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IFeedBackPeriodRepository
    {
        Task<List<FeedBackPeriod>> GetAsync();

        Task AddAsync(FeedBackPeriod period);

        Task EditAsync(FeedBackPeriod period);

    }
}
