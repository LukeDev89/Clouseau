using System;
using System.Collections.Generic;

namespace DataContext;

public partial class ResourceWarningHistory
{
    public long Id { get; set; }

    public long ResourceWarningId { get; set; }

    public long UserId { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime Date { get; set; }

    public virtual ResourceWarning ResourceWarning { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
