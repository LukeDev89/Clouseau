using DataContext;
using DataContext.Interfaces.Management;
using DataService.Interfaces.Management;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Services.Management
{
    public class FeedBackImprovementUserService : IFeedBackImprovementUserService
    {
        private readonly IFeedBackImprovementUserRepository _repository;

        public FeedBackImprovementUserService(IFeedBackImprovementUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FeedbackImprovementUser>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(FeedbackImprovementUser improvementUser) => await _repository.AddAsync(improvementUser);

        public async Task EditAsync(FeedbackImprovementUser improvementUser) => await _repository.EditAsync(improvementUser);

        public async Task DeleteAsync(long id) => await _repository.DeleteAsync(id);

        public async Task DefinitiveDeleteAsync(long improvementUserId) => await _repository.DefinitiveDeleteAsync(improvementUserId); 
    }
}
