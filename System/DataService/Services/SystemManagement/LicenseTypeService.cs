
using DataContext;
using DataContext.Interfaces.Management;
using DataService.Interfaces.Management;

namespace DataService.Services.Management
{
    public class LicenseTypeService : ILicenseTypeService
    {
        private readonly ILicenseTypeRepository _repository;

        public LicenseTypeService(ILicenseTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<LicenseType>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(LicenseType license) => await _repository.AddAsync(license);

        public async Task EditAsync(LicenseType license) => await _repository.EditAsync(license);

        public async Task DeleteAsync(long id) => await _repository.DeleteAsync(id);
    }
}
