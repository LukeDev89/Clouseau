using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IRagTemplatesRepository
    {
        Task<List<RagTemplate>> GetAsync();
		Task<List<RagFactor>> GetTemplateFactorById(long id);
		Task AddAsync(RagTemplate ragTemplate);
		Task UpdateState(long id, string name, DateTime period);
		Task AddFactorsAsync(long templateId, long factorId);
		Task DeleteFactorAsync(long id, long factorId);
		Task DeleteFactor(long id);
		Task EditAsync(long id, string name, bool active);
		Task DefinitiveDeleteAsync(long templateId);
		Task Execute(long templateId);
    }
}

