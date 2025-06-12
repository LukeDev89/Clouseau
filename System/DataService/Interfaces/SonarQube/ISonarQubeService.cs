using DataModel.Response.SonarQube;

namespace DataService.Interfaces.SonarQube
{
    public interface ISonarQubeService
    {
        Task<List<SonarQubeResponse>> GetSonarQubeMetrics();
    }
}
