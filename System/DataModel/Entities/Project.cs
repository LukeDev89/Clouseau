using System;
using System.Collections.Generic;
using System.Globalization;

namespace DataContext;

public partial class Project
{
    public long Id { get; set; }

    public long ClientId { get; set; }

    public string Name { get { return _name.Trim(); } set { _name = value; } }
    private string _name = "";

    public long? ClientResponsibleId { get; set; }

    public DateTime Created { get; set; }
    public DateOnly _Created { get { return DateOnly.FromDateTime(Created); } }

    public bool Active { get; set; }

    public DateTime? Deleted { get; set; }
    public DateOnly? _Deleted { get { return Deleted.HasValue ? DateOnly.FromDateTime(Deleted.Value) : (DateOnly?)null; } }

    public long? TeamId { get; set; }

    public virtual Client Client { get; set; } = null!;
    public string ClientName { get { return Client?.Name ?? ""; } }

    public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();

    public virtual Team? Team { get; set; }
    public string TeamName { get { return Team?.Name ?? ""; } }

    public virtual ICollection<RagStatus> RagStatuses { get; set; } = new List<RagStatus>();

    public virtual ClientResponsible? ClientResponsible { get; set; }
    public string ClientResponsibleName { get { return ClientResponsible?.Name ?? ""; } }
}
