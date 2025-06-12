using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IFeedBackItemRepository
    {
        Task<List<FeedBackItem>> GetAsync();

        Task AddAsync(FeedBackItem item);

        Task EditAsync(FeedBackItem item);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long itemId);
    }
}
