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
    public class FeedBackImprovementService : IFeedBackImprovementService
    {
        private readonly IFeedBackImprovementRepository _repository;

        public FeedBackImprovementService(IFeedBackImprovementRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FeedbackImprovement>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(FeedbackImprovement improvement) => await _repository.AddAsync(improvement);

        public async Task EditAsync(FeedbackImprovement improvement) => await _repository.EditAsync(improvement);

        public async Task DeleteAsync(long id) => await _repository.DeleteAsync(id);

        public async Task DefinitiveDeleteAsync(long improvementId) => await _repository.DefinitiveDeleteAsync(improvementId); 
    }
}
