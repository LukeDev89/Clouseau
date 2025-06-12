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
    public class FeedBackStandarTypeService : IFeedBackStandarTypeService
    {
        private readonly IFeedBackStandarTypeRepository _repository;

        public FeedBackStandarTypeService(IFeedBackStandarTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FeedBackStandarType>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(FeedBackStandarType standarType) => await _repository.AddAsync(standarType);

        public async Task EditAsync(FeedBackStandarType standarType) => await _repository.EditAsync(standarType);

        public async Task DeleteAsync(long id) => await _repository.DeleteAsync(id);

        public async Task DefinitiveDeleteAsync(long standarTypeId) => await _repository.DefinitiveDeleteAsync(standarTypeId);

        public async Task OnSelectedStatusChanged (FeedBackStandarType feedbackStatus) => await _repository.OnSelectedStatusChanged(feedbackStatus);

        

    }
}
