using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Interfaces.Management
{
    public interface IFeedBackStandarTypeRepository
    {
        Task<List<FeedbackStandarType>> GetAsync();

        Task AddAsync(FeedbackStandarType standarType);

        Task EditAsync(FeedbackStandarType standarType);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long standarTypeId);

        Task OnSelectedStatusChanged(FeedbackStandarType feedbackStatus);
    }
}
