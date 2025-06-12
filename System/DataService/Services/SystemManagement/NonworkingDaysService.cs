using DataContext;

using DataContext.Interfaces.Management;
using DataService.Interfaces.Management;

namespace DataService.Services.Management
{
    public class NonworkingDaysService : INonworkingDaysService
    {
        private readonly INonworkingDaysRepository _repository;

        public NonworkingDaysService(INonworkingDaysRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<NonworkingDay>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(NonworkingDay nonworkingDay) => await _repository.AddAsync(nonworkingDay);

        public async Task DeleteAsync(long Id) => await _repository.DeleteAsync(Id);
    }
}
