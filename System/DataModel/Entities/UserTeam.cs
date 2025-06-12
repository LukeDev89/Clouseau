using System;
using System.Collections.Generic;

namespace DataContext;

public partial class UserTeam
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long TeamId { get; set; }

    public DateTime Income { get; set; }

    public DateOnly _Income { get { return DateOnly.FromDateTime(Income); } }

    public DateTime? Outcome { get; set; }

    public DateOnly? _Outcome { get { return Outcome.HasValue ? DateOnly.FromDateTime(Outcome.Value) : (DateOnly?)null; } }

    public bool RequireManagement { get; set; }

    public DateTime? RollOn { get; set; }

    public DateTime? RollOff { get; set; }

    public int AssignmentSold { get; set; }

    public int RealAssignment { get; set; }

    public string? Comments { get; set; }

    public bool Temporary { get; set; }

    public virtual Team Team { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
