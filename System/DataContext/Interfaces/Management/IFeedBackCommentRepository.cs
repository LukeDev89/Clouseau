using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IFeedBackCommentRepository
    {
        Task<List<FeedBackComment>> GetAsync();

        Task AddAsync(FeedBackComment comment);

        Task EditAsync(FeedBackComment comment);

        Task DeleteAsync(long id);

		Task DefinitiveDeleteAsync(long commentId);
	}
}
