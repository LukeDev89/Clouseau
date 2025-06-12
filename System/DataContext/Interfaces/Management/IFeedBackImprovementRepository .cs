using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IFeedBackImprovementRepository
    {
        Task<List<FeedBackImprovement>> GetAsync();

        Task AddAsync(FeedBackImprovement improvement);

        Task EditAsync(FeedBackImprovement improvement);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long improvementId); 
    }
}
