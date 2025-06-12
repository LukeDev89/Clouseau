using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IFeedBackStandarRepository
    {
        Task<List<FeedBackStandar>> GetAsync();

        Task AddAsync(FeedBackStandar standar);

        Task EditAsync(FeedBackStandar standar);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long standarId); 
    }
}
