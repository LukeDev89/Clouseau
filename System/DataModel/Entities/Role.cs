using System;
using System.Collections.Generic;

namespace DataContext;

public partial class Role
{
    public long Id { get; set; }

    public string Name { get { return _name.Trim(); } set { _name = value; } }
    private string _name = "";

    public string? Description { get { return _description?.Trim(); } set { _description = value; } }
    private string? _description = "";

    public bool Active { get; set; }

    public short Hierarchy { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual ICollection<ProjectTaskEstimation> ProjectTaskEstimations { get; set; } = new List<ProjectTaskEstimation>();
}
