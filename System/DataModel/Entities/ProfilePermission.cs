using System;
using System.Collections.Generic;

namespace DataContext;

public partial class ProfilePermission
{
    public long Id { get; set; }

    public long ProfileId { get; set; }

    public long SectionId { get; set; }

    public virtual Profile Profile { get; set; } = null!;

    public virtual Section Section { get; set; } = null!;
}
