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
        Task<List<FeedbackImprovementUser>> GetAsync();

        Task AddAsync(FeedbackImprovementUser improvementUser);

        Task EditAsync(FeedbackImprovementUser improvementUser);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long improvementUserId); 
    }
}
