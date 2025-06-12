using DataContext;
using System;
using System.Collections.Generic;

namespace DataContext;

public partial class ActionsAudit
{
    public long Id { get; set; }

    public string Entity { get; set; } = null!;

    public string Action { get; set; } = null!;

    public long UserId { get; set; }

    public DateTime Date { get; set; }

    public string? Description { get; set; }

    public virtual User User { get; set; } = null!;
}
