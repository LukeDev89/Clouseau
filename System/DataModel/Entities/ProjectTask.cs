
namespace DataContext;

public partial class ProjectTask
{
    public long Id { get; set; }

    public string Name { get { return _name.Trim(); } set { _name = value; } }
    private string _name = "";

    public string Description { get { return _description.Trim(); } set { _description = value; } }
    private string _description = "";

    public string? KanbanId { get; set; }

    public DateTime CreationDate { get; set; }
    public DateOnly _CreationDate { get { return DateOnly.FromDateTime(CreationDate); } }

    public DateTime? InitDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? DeliverDate { get; set; }

    public long? ProjectId { get; set; }

    public long? ClientId { get; set; }

    public decimal EstimatedHours { get; set; }

    public bool Active { get; set; }

    public bool Finished { get; set; }

    public long? TaskTypeId { get; set; }

    public virtual Project? Project { get; set; } = null!;
    public string ProjectName { get { return Project is not null ? Project.Name : ""; } }

    public virtual Client? Client { get; set; } = null!;
    public string ClientName 
    { 
        get 
        {
            if (Client is not null)
            {
                return Client.Name;
            }

            return Project is not null ? Project.Client.Name : ""; 
        } 
    }

    public virtual ICollection<TaskProgress> TaskProgresses { get; set; } = new List<TaskProgress>();
    public decimal Completed { get { return TaskProgresses.Count > 0 ? TaskProgresses.Sum(x => x.Hours) : 0; } }

    public decimal Progress { get { return EstimatedHours > 0 ? (Completed / EstimatedHours) : 0; } }

    public virtual TaskType? TaskType { get; set; }
    public string TaskTypeName { get { return TaskType?.Name ?? ""; } }

    public virtual ICollection<ProjectTaskEstimation> ProjectTaskEstimations { get; set; } = new List<ProjectTaskEstimation>();

    public virtual ICollection<ProjectTaskManagement> ProjectTaskManagements { get; set; } = new List<ProjectTaskManagement>();

    public virtual ICollection<ExtraHours> ExtraHours { get; set; } = new List<ExtraHours>();
}
