using System;
using System.Collections.Generic;

namespace DataContext;

public partial class Section
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<CustomPermission> CustomPermissions { get; set; } = new List<CustomPermission>();

    public virtual ICollection<ProfilePermission> ProfilePermissions { get; set; } = new List<ProfilePermission>();
}
