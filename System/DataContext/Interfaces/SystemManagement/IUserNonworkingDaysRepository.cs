
using DataModel.Response;

namespace DataContext.Interfaces.Management
{
    public interface IUserNonworkingDaysRepository
    {
        Task<List<UserNonworkingDay>> GetAsync();

        Task AddAsync(long userId, long type, DateTime from, DateTime to, string? comment, string? fileExtension, string? fileBytes);

        Task DeleteAsync(long Id);

        Task UpdateState(long Id, short state, string commentCfo);

        Task<FileResponse> DownloadFile(long Id);

        Task UploadFile(long Id, string fileExtension, string fileBytes);
    }
}
