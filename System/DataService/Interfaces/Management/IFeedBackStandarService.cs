using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces.Management
{
    public interface IFeedBackStandarService
    {
        Task<List<FeedbackStandar>> GetAsync();

        Task AddAsync(FeedbackStandar standar);

        Task EditAsync(FeedbackStandar standar);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long standarId); 
    }
}
