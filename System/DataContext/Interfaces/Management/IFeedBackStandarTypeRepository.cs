using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IFeedBackStandarTypeRepository
    {
        Task<List<FeedBackStandarType>> GetAsync();

        Task AddAsync(FeedBackStandarType standarType);

        Task EditAsync(FeedBackStandarType standarType);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long standarTypeId);

        Task OnSelectedStatusChanged(FeedBackStandarType feedbackStatus);
    }
}
