using System;
using System.Collections.Generic;

namespace DataContext;

public partial class CustomPermission
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long SectionId { get; set; }

    public virtual Section Section { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
