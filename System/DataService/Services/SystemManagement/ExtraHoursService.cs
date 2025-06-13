using DataContext;
using DataContext.Interfaces.SystemManagement;
using DataModel.Enums;
using DataService.Interfaces.SystemManagement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataService.Services.SystemManagement
{
    public class ExtraHoursService : IExtraHoursService
    {
        private readonly IExtraHoursRepository _repository;

        public ExtraHoursService(IExtraHoursRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ExtraHours>> GetAsync() => await _repository.GetAsync();

        public async Task<List<ExtraHours>> GetByUserIdAsync(long userId) => await _repository.GetByUserIdAsync(userId);

        public async Task<List<ExtraHours>> GetByDateRangeAsync(DateTime from, DateTime to) => await _repository.GetByDateRangeAsync(from, to);

        public async Task<List<ExtraHours>> GetByStatusAsync(ExtraHoursStatus status) => await _repository.GetByStatusAsync(status);

        public async Task<List<ExtraHours>> GetPendingForSupervisorAsync(long supervisorId) => await _repository.GetPendingForSupervisorAsync(supervisorId);

        public async Task<ExtraHours?> GetByIdAsync(long id) => await _repository.GetByIdAsync(id);

        public async Task AddAsync(ExtraHours extraHours) => await _repository.AddAsync(extraHours);

        public async Task EditAsync(ExtraHours extraHours) => await _repository.EditAsync(extraHours);

        public async Task ApproveAsync(long id, long reviewedBy, string? reviewComment = null) => await _repository.ApproveAsync(id, reviewedBy, reviewComment);

        public async Task RejectAsync(long id, long reviewedBy, string? reviewComment = null) => await _repository.RejectAsync(id, reviewedBy, reviewComment);

        public async Task DeleteAsync(long id) => await _repository.DeleteAsync(id);
    }
}