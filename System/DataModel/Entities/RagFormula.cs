using System;
using System.Collections.Generic;

namespace DataContext;

public partial class RagFormula
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<RagTemplate> RagTemplates { get; set; } = new List<RagTemplate>();
}
