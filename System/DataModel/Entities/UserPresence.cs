using System;
using System.Collections.Generic;

namespace DataContext;

public partial class UserPresence
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public short WeekDay { get; set; }

    public TimeSpan? Ingress { get; set; }

    public TimeSpan? Egress { get; set; }

    public virtual User User { get; set; } = null!;
}
