using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces.Management
{
    public interface IFeedBackUserService
    {
        Task<List<FeedbackUser>> GetAsync();

        Task<List<FeedbackUser>> GetByUserIdAsync(long userId);

        Task AddAsync(FeedbackUser user);

        Task EditAsync(FeedbackUser user);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long userId); 


    }
}
