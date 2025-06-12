using System;
using System.Collections.Generic;

namespace DataContext;

public partial class RagTemplateFactor
{
    public long Id { get; set; }

    public long TemplateId { get; set; }

    public long FactorId { get; set; }

    public virtual RagFactor Factor { get; set; } = null!;

    public virtual RagTemplate Template { get; set; } = null!;
}
