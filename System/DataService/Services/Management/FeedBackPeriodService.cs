using DataContext;
using DataContext.Interfaces.Management;
using DataService.Interfaces.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Services.Management
{
    public class FeedBackPeriodService : IFeedBackPeriodService
    {
        private readonly IFeedBackPeriodRepository _repository;

        public FeedBackPeriodService(IFeedBackPeriodRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FeedBackPeriod>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(FeedBackPeriod period) => await _repository.AddAsync(period);

        public async Task EditAsync(FeedBackPeriod period) => await _repository.EditAsync(period);

    }
}
