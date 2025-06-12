using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IFeedBackImprovementUserRepository
	{
        Task<List<FeedbackImprovementUser>> GetAsync();

        Task AddAsync(FeedbackImprovementUser improvementUser);

        Task EditAsync(FeedbackImprovementUser improvementUser);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long improvementUserId);

		
	}
}
