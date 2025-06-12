using System;
using System.Collections.Generic;

namespace DataContext;

public partial class RagCalification
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public int Value { get; set; }

    public virtual ICollection<RagFormulasCalification> RagFormulasCalifications { get; set; } = new List<RagFormulasCalification>();

    public virtual ICollection<RagStatus> RagStatuses { get; set; } = new List<RagStatus>();
}
