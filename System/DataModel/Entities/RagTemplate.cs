using System;
using System.Collections.Generic;

namespace DataContext;

public partial class RagTemplate
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Period { get; set; }
	public DateOnly _Period { get { return DateOnly.FromDateTime(Period); } }

    public long? RagFormulaId { get; set; }

    public virtual RagFormula? RagFormula { get; set; }

    public virtual ICollection<RagTemplateFactor> RagTemplateFactors { get; set; } = new List<RagTemplateFactor>();
    
    public virtual ICollection<RagStatus> RagStatuses { get; set; } = new List<RagStatus>();
}
