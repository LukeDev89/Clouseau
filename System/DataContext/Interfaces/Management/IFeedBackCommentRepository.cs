using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IFeedBackCommentRepository
    {
        Task<List<FeedbackComment>> GetAsync();

        Task AddAsync(FeedbackComment comment);

        Task EditAsync(FeedbackComment comment);

        Task DeleteAsync(long id);

		Task DefinitiveDeleteAsync(long commentId);
	}
}
