using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces.Management
{
    public interface IFeedBackImprovementUserService
    {
        Task<List<FeedBackImprovementUser>> GetAsync();

        Task AddAsync(FeedBackImprovementUser improvementUser);

        Task EditAsync(FeedBackImprovementUser improvementUser);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long improvementUserId); 
    }
}
