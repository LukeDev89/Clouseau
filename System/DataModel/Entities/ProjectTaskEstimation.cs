using System;
using System.Collections.Generic;

namespace DataContext;

public partial class ProjectTaskEstimation
{
    public long Id { get; set; }

    public long ProjectTaskId { get; set; }

    public long RoleId { get; set; }

    public int Hours { get; set; }

    public virtual ProjectTask ProjectTask { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
    public string RoleName { get => Role != null ? Role.Name : ""; }
}
