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
    public class FeedBackUserService : IFeedBackUserService
    {
        private readonly IFeedBackUserRepository _repository;

        public FeedBackUserService(IFeedBackUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FeedBackUser>> GetAsync() => await _repository.GetAsync();

        public async Task<List<FeedBackUser>> GetByUserIdAsync(long userId) => await _repository.GetByUserIdAsync(userId);

        public async Task AddAsync(FeedBackUser user) => await _repository.AddAsync(user);

        public async Task EditAsync(FeedBackUser user) => await _repository.EditAsync(user);

        public async Task DeleteAsync(long id) => await _repository.DeleteAsync(id);

        public async Task DefinitiveDeleteAsync(long userId) => await _repository.DefinitiveDeleteAsync(userId);
    }
}
