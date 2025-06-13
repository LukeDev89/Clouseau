using DataContext;
using DataModel.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataService.Interfaces.SystemManagement
{
    public interface IExtraHoursService
    {
        Task<List<ExtraHours>> GetAsync();
        Task<List<ExtraHours>> GetByUserIdAsync(long userId);
        Task<List<ExtraHours>> GetByDateRangeAsync(DateTime from, DateTime to);
        Task<List<ExtraHours>> GetByStatusAsync(ExtraHoursStatus status);
        Task<List<ExtraHours>> GetPendingForSupervisorAsync(long supervisorId);
        Task<ExtraHours?> GetByIdAsync(long id);
        Task AddAsync(ExtraHours extraHours);
        Task EditAsync(ExtraHours extraHours);
        Task ApproveAsync(long id, long reviewedBy, string? reviewComment = null);
        Task RejectAsync(long id, long reviewedBy, string? reviewComment = null);
        Task DeleteAsync(long id);
    }
}