using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IFeedBackItemRepository
    {
        Task<List<FeedbackItem>> GetAsync();

        Task AddAsync(FeedbackItem item);

        Task EditAsync(FeedbackItem item);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long itemId);
    }
}
