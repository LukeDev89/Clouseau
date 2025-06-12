using DataContext;
using Microsoft.Azure.Pipelines.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces.Management
{
    public interface IFeedBackStandarTypeService
    {
        Task<List<FeedbackStandarType>> GetAsync();

        Task AddAsync(FeedbackStandarType standarType);

        Task EditAsync(FeedbackStandarType standarType);

        Task DeleteAsync(long id);

        Task DefinitiveDeleteAsync(long standarTypeId);

        Task OnSelectedStatusChanged(FeedbackStandarType feedbackStatus);

    }
}
