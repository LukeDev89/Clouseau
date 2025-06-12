using DataContext;
using DataContext.Interfaces.Management;
using DataService.Interfaces.Management;
using Microsoft.VisualStudio.Services.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Services.Management
{
    public class FeedBackItemService : IFeedBackItemService
    {
        private readonly IFeedBackItemRepository _repository;

        public FeedBackItemService(IFeedBackItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FeedBackItem>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(FeedBackItem standar) => await _repository.AddAsync(standar);

        public async Task EditAsync(FeedBackItem standar) => await _repository.EditAsync(standar);

        public async Task DeleteAsync(long id) => await _repository.DeleteAsync(id);

        public async Task DefinitiveDeleteAsync(long itemId) => await _repository.DefinitiveDeleteAsync(itemId); 
    }
}
