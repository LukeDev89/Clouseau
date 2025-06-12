using DataContext;
using DataContext.Interfaces.Management;
using DataModel.Response;
using DataService.Interfaces.Management;

namespace DataService.Services.Management
{
    public class UserNonworkingDaysService : IUserNonworkingDaysService
    {
        private readonly IUserNonworkingDaysRepository _repository;

        public UserNonworkingDaysService(IUserNonworkingDaysRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UserNonworkingDay>> GetAsync() => await _repository.GetAsync();

        public async Task AddAsync(long userId, long type, DateTime from, DateTime to, string? comment, string? fileExtension, string? fileBytes) => await _repository.AddAsync(userId, type, from, to, comment,fileExtension, fileBytes);

        public async Task DeleteAsync(long Id) => await _repository.DeleteAsync(Id);

        public async Task UpdateState(long Id, short state, string commentCfo) => await _repository.UpdateState(Id, state, commentCfo);

        public async Task<FileResponse> DownloadFile(long Id) => await _repository.DownloadFile(Id);

        public async Task UploadFile(long Id, string fileExtension, string fileBytes) => await _repository.UploadFile(Id, fileExtension, fileBytes);
    }
}
