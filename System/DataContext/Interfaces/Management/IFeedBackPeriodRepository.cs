using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IFeedBackPeriodRepository
    {
        Task<List<FeedbackPeriod>> GetAsync();

        Task AddAsync(FeedbackPeriod period);

        Task EditAsync(FeedbackPeriod period);

    }
}
