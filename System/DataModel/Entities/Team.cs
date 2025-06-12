using System;
using System.Collections.Generic;
using System.Globalization;

namespace DataContext;

public partial class Team
{
    public long Id { get; set; }

    public string Name { get { return _name.Trim(); } set { _name = value; } }

    private string _name = "";

    public DateTime Created { get; set; }

    public DateOnly _Created { get { return DateOnly.FromDateTime(Created); } }

    public bool Active { get; set; }

    public DateTime? Deleted { get; set; }

    public DateOnly? _Deleted { get { return Deleted.HasValue ? DateOnly.FromDateTime(Deleted.Value) : (DateOnly?)null; } }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<UserTeam> UserTeams { get; set; } = new List<UserTeam>();
}
