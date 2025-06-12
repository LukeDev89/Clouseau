
using DataContext;
using DataModel.Response.VersionControl;
using DataService.Interfaces.Management;
using DataService.Interfaces.VersionControl;
using Microsoft.Extensions.Configuration;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Newtonsoft.Json;
using System.Text;

namespace DataService.Services.VersionControl
{
    public class VersionControlService : IVersionControlService
    {
        private readonly List<string> _collectionUri;
        private readonly string _personalAccessToken;

        private readonly IUserService _userService;

        private List<User> _users;

        public VersionControlService(IConfiguration configuration, IUserService userService)
        {
            _personalAccessToken = configuration.GetSection("Tokens:VersionControlToken").Value;
            var collectionUriSection = configuration.GetSection("VersionControlConfig:CollectionUri");
            
            _collectionUri = collectionUriSection.GetChildren().Select(x => x.Value).ToList();

            _userService = userService;
        }

        public async Task<List<VersionControlProjectModel>> GetVersionControlAsync()
        {
            var response = new List<VersionControlProjectModel>();

            foreach (var _uri in _collectionUri)
            {
                try
                {
                    VssConnection connection = new VssConnection(new Uri(_uri), new VssBasicCredential(string.Empty, _personalAccessToken));
                    GitHttpClient gitClient = connection.GetClient<GitHttpClient>();

                    var _response = new List<VersionControlProjectModel>();
                    var projects = await GetProjectsAsync(_uri);

                    foreach (var project in projects)
                    {
                        var azureProject = await ProcessProjectAsync(project, gitClient);
                        _response.Add(azureProject);
                    }

                    response.AddRange(_response);
                }
                catch (Exception ex)
                {
                    return response;
                }
            }

            return response;
        }

        private async Task<VersionControlProjectModel> ProcessProjectAsync(TeamProjectReference project, GitHttpClient gitClient)
        {
            var azureProject = new VersionControlProjectModel
            {
                Guid = project.Id,
                Name = project.Name
            };

            var repositories = await gitClient.GetRepositoriesAsync(project.Name);
            var repos = new List<VersionControlRepositoryModel>();

            foreach (var repo in repositories)
            {
                var azureRepo = await ProcessRepositoryAsync(project, repo, gitClient);
                repos.Add(azureRepo);
            }

            azureProject.Repositories = repos;
            return azureProject;
        }

        private async Task<VersionControlRepositoryModel> ProcessRepositoryAsync(TeamProjectReference project, GitRepository repo, GitHttpClient gitClient)
        {
            var branches = await gitClient.GetBranchesAsync(repo.Id);
            var commitsList = new List<VersionControlCommitModel>();

            foreach (var branch in branches)
            {
                var commits = await GetCommitsAsync(project.Id, repo.Id, branch.Name, gitClient, project.Name, repo.Name);
                commitsList.AddRange(commits);
            }

            return new VersionControlRepositoryModel
            {
                Guid = repo.Id,
                Name = repo.Name,
                Commits = commitsList
            };
        }

        private async Task<IEnumerable<VersionControlCommitModel>> GetCommitsAsync(Guid projectId, Guid repoId, string branchName, GitHttpClient gitClient, string projectName, string repositoryName)
        {
            if (_users == null)
            {
                _users = await _userService.GetAsync();
            }

            var commits = await gitClient.GetCommitsAsync(projectId, repoId, new GitQueryCommitsCriteria
            {
                ItemVersion = new GitVersionDescriptor
                {
                    Version = branchName,
                    VersionType = GitVersionType.Branch
                }
            });

            return commits.DistinctBy(c => c.CommitId).Select(c => new VersionControlCommitModel
            {
                Guid = c.CommitId,
                Project = projectName,
                Repository = repositoryName,
                Branch = branchName,
                Comment = c.Comment,
                Committer = c.Committer.Name,
                UserSWF = _users.Find(u => u.UserAccounts.Any(a => a.Username.Trim() == c.Committer.Name))?.Username,
                ChangeCount = c.ChangeCounts.Sum(t => t.Value),
                Date = c.Committer.Date.AddHours(-3)
            });
        }

        public async Task<List<TeamProjectReference>> GetProjectsAsync(string uri)
        {
            VssConnection connection = new VssConnection(new Uri(uri), new VssBasicCredential(string.Empty, _personalAccessToken));
            ProjectHttpClient projectClient = connection.GetClient<ProjectHttpClient>();
            var projects = await projectClient.GetProjects();
            return (List<TeamProjectReference>)projects;
        }

        public async Task<List<WorkItemProjectModel>> GetWorkItemsAsync()
        {
            var response = new List<WorkItemProjectModel>();

            var base64Token = Convert.ToBase64String(Encoding.ASCII.GetBytes($":{_personalAccessToken}"));
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Token);

            var wiqlQuery = @"{
                ""query"": ""Select
                                [System.Id],
                                [System.Title],
                                [System.WorkItemType],
                                [System.AssignedTo],
                                [Microsoft.VSTS.Scheduling.OriginalEstimate],
                                [Microsoft.VSTS.Scheduling.CompletedWork],
                                [Microsoft.VSTS.Scheduling.StartDate],
                                [Microsoft.VSTS.Scheduling.FinishDate],
                                [System.State]
                            From WorkItems
                            Where
                                [System.WorkItemType] IN ('Task', 'User Story', 'Bug')
                                And [System.State] IN ('Active', 'Closed')
                                And [System.CreatedDate] > @today-90""
            }";

            foreach (var _uri in _collectionUri)
            {
                var _response = new List<WorkItemProjectModel>();
                var projects = await GetProjectsAsync(_uri);

                foreach (var project in projects)
                {
                    try
                    {
                        var clientResponse = await client.PostAsync($"{_uri}/{project.Name}/_apis/wit/wiql?api-version=6.0", new StringContent(wiqlQuery, Encoding.UTF8, "application/json"));
                        var responseBody = await clientResponse.Content.ReadAsStringAsync();

                        var workItems = JsonConvert.DeserializeObject<List<WorkItemProjectModel>>(responseBody);
                    }
                    catch (Exception e) 
                    { 
                    
                    }
                }
            }

            return response;
        }
    }
}