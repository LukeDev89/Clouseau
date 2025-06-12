using DataContext.Interfaces.ProjectManagement;
using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContext.Interfaces.Management;
using DataService.Interfaces.Management;

namespace DataService.Services.Management
{
    public class SystemConfigService : ISystemConfigService
    {
        private readonly ISystemConfigRepository _repository;

        public SystemConfigService(ISystemConfigRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SystemConfig>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(SystemConfig config) => await _repository.AddAsync(config);

        public async Task EditAsync(SystemConfig config) => await _repository.EditAsync(config);
    }
}
