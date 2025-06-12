using System;
using System.Collections.Generic;
using System.Globalization;

namespace DataContext;

public partial class Client
{
    public long Id { get; set; }

    public string Name { get { return _name.Trim(); } set { _name = value; } }
    private string _name = "";

    public DateTime Income { get; set; }

    public DateOnly _Income { get { return DateOnly.FromDateTime(Income); } }

    public DateTime? Outcome { get; set; }

    public DateOnly? _Outcome { get { return Outcome.HasValue ? DateOnly.FromDateTime(Outcome.Value) : (DateOnly?)null; } }

    public bool Active { get; set; }

    public virtual ICollection<ClientContract> ClientContracts { get; set; } = new List<ClientContract>();
    public virtual ICollection<ClientResponsible> ClientResponsibles { get; set; } = new List<ClientResponsible>();

    public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}