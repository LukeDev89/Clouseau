using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IFeedBackImprovementRepository
    {
        Task<List<FeedbackImprovement>> GetAsync();

        Task AddAsync(FeedbackImprovement improvement);

        Task EditAsync(FeedbackImprovement improvement);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long improvementId); 
    }
}
