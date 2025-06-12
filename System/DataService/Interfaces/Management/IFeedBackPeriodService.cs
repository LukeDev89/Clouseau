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
        Task<List<FeedbackPeriod>> GetAsync();

        Task AddAsync(FeedbackPeriod period);

        Task EditAsync(FeedbackPeriod period);

    }
}
