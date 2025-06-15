using System;
using System.Collections.Generic;

namespace DataContext;

public partial class ProjectTaskType
{
    public long Id { get; set; }

    public string Name { get { return _name.Trim(); } set { _name = value; } }
    private string _name = "";

    public string? Description { get { return _description?.Trim(); } set { _description = value; } }
    private string? _description = "";

    public virtual ICollection<ProjectTaskProgress> TaskProgresses { get; set; } = new List<ProjectTaskProgress>();
    public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
    public virtual ICollection<ExtraHours> ExtraHours { get; set; } = new List<ExtraHours>();
}
