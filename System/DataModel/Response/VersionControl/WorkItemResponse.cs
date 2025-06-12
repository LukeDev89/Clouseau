using DataContext;

namespace DataModel.Response.VersionControl
{
    public class WorkItemResponse
    {
        public List<WorkItemProjectModel> Projects { get; set; } = new List<WorkItemProjectModel>();
    }

    public class WorkItemProjectModel
    {
        public int Id { get; set; }

        public string Url { get; set; } = "";
        
        public string Title { get; set; } = "";
        
        public string Type { get; set; } = "";

        public string AssignedTo { get; set; } = "";

        public double? OriginalEstimate { get; set; }

        public double? CompletedWork { get; set; }

        public string State { get; set; } = "";

        public DateTime? StartDate { get; set; }

        public DateTime? FinishDate { get; set; }
    }
}