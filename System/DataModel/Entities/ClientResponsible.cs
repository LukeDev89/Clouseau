using System;
using System.Collections.Generic;

namespace DataContext;

public partial class ClientResponsible
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Mail { get; set; }

    public long? ClientId { get; set; }
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    public virtual Client Client { get; set; } = null!;
}
