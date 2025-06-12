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
    public class ResourceWarningHistoryService : IResourceWarningHistoryService
    {
        private readonly IResourceWarningHistoryRepository _repository;

        public ResourceWarningHistoryService(IResourceWarningHistoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ResourceWarningHistory>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(ResourceWarningHistory resourceWarning) => await _repository.AddAsync(resourceWarning);

        public async Task EditAsync(ResourceWarningHistory resourceWarning) => await _repository.EditAsync(resourceWarning);
    }
}
