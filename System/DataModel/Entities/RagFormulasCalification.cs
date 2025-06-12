using System;
using System.Collections.Generic;

namespace DataContext;

public partial class RagFormulasCalification
{
    public long Id { get; set; }

    public long RagCalificationId { get; set; }

    public int NewValue { get; set; }

    public virtual RagCalification RagCalification { get; set; } = null!;
}
