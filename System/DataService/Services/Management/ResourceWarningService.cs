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
    public class ResourceWarningService : IResourceWarningService
    {
        private readonly IResourceWarningRepository _repository;

        public ResourceWarningService(IResourceWarningRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ResourceWarning>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(ResourceWarning resourceWarning) => await _repository.AddAsync(resourceWarning);

        public async Task EditAsync(ResourceWarning resourceWarning) => await _repository.EditAsync(resourceWarning);

		public async Task DefinitiveDeleteAsync(long resourceWarningId) => await _repository.DefinitiveDeleteAsync(resourceWarningId);
	}
}
