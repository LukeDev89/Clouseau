using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IFeedBackStandarRepository
    {
        Task<List<FeedbackStandar>> GetAsync();

        Task AddAsync(FeedbackStandar standar);

        Task EditAsync(FeedbackStandar standar);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long standarId); 
    }
}
