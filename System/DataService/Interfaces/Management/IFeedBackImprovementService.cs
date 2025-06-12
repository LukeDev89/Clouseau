using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces.Management
{
    public interface IFeedBackImprovementService
    {
        Task<List<FeedbackImprovement>> GetAsync();

        Task AddAsync(FeedbackImprovement improvement);

        Task EditAsync(FeedbackImprovement improvement);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long improvementId); 
    }
}
