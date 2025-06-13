using DataModel.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataContext.Interfaces.SystemManagement
{
    public interface IExtraHoursRepository
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