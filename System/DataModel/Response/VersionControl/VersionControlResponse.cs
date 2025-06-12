using DataContext;

namespace DataModel.Response.VersionControl
{
    public class VersionControlResponse
    {
        public List<VersionControlProjectModel> Projects { get; set; } = new List<VersionControlProjectModel>();
    }

    public class VersionControlProjectModel
    {
        public Guid Guid { get; set; }
        public string Name { get; set; } = "";
        public List<VersionControlRepositoryModel> Repositories { get; set; } = new List<VersionControlRepositoryModel>();

    }

    public class VersionControlRepositoryModel
    {
        public Guid Guid { get; set; }
        public string Name { get; set; } = "";
        public List<VersionControlCommitModel> Commits { get; set; } = new List<VersionControlCommitModel>();
    }

    public class VersionControlCommitModel
    {
        public string Guid { get; set; } = "";
        public string Project { get; set; } = "";
        public string Repository { get; set; } = "";
        public string Branch { get; set; } = "";
        public string Comment { get; set; } = "";
        public string UserSWF { get; set; } = "";
        public string Committer { get; set; } = "";
        public string CommitterEmail { get; set; } = "";
        public int ChangeCount { get; set; }
        public DateTime Date { get; set; }
    }
}