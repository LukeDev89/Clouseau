using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces.Management
{
    public interface IFeedBackItemService
    {
        Task<List<FeedBackItem>> GetAsync();

        Task AddAsync(FeedBackItem item);

        Task EditAsync(FeedBackItem item);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long itemId); 
    }
}
