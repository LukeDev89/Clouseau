using DataContext;
using Microsoft.Azure.Pipelines.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces.Management
{
    public interface IRagTemplatesService
    {
        Task<List<RagTemplate>> GetAsync();
		Task<List<RagFactor>> GetTemplateFactorById(long id);
		Task AddAsync(RagTemplate ragTemplate);
		public Task UpdateState(long id, string name, DateTime period);
		Task AddFactorsAsync(long templateId, long factorId);
		Task DeleteFactorAsync(long id, long factorId);
		Task DeleteFactor(long id);
		Task EditAsync(long id, string name, bool active);
		Task DefinitiveDeleteAsync(long templateId);
		Task Execute(long templateId);
	}
}
