using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IFeedBackImprovementUserRepository
	{
        Task<List<FeedBackImprovementUser>> GetAsync();

        Task AddAsync(FeedBackImprovementUser improvementUser);

        Task EditAsync(FeedBackImprovementUser improvementUser);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long improvementUserId);

		
	}
}
