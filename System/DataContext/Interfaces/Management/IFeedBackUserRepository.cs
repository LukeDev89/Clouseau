using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IFeedBackUserRepository
    {
        Task<List<FeedBackUser>> GetAsync();

        Task<List<FeedBackUser>> GetByUserIdAsync(long userId);

        Task AddAsync(FeedBackUser user);

        Task EditAsync(FeedBackUser user);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long userId); 
    }
}
