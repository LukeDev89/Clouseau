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
    public class FeedBackStandarService : IFeedBackStandarService
    {
        private readonly IFeedBackStandarRepository _repository;

        public FeedBackStandarService(IFeedBackStandarRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FeedbackStandar>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(FeedbackStandar standar) => await _repository.AddAsync(standar);

        public async Task EditAsync(FeedbackStandar standar) => await _repository.EditAsync(standar);

        public async Task DeleteAsync(long id) => await _repository.DeleteAsync(id);

        public async Task DefinitiveDeleteAsync(long standarId) => await _repository.DefinitiveDeleteAsync(standarId); 

    }
}
