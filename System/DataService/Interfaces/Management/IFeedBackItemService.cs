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
        Task<List<FeedbackItem>> GetAsync();

        Task AddAsync(FeedbackItem item);

        Task EditAsync(FeedbackItem item);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long itemId); 
    }
}
