using System;
using System.Collections.Generic;

namespace DataContext;

public partial class RagStatus
{
    public long Id { get; set; }

    public long ProjectId { get; set; }

    public long? UserId { get; set; }

    public long FactorId { get; set; }

    public long CalificationId { get; set; }

    public string? Comment { get; set; }

    public DateTime? Date { get; set; }

    public long? TemplateId { get; set; }

    public virtual RagFactor Factor { get; set; } = null!;
    public string FactorName { get => Factor != null ? Factor.Name : ""; }

    public virtual Project Project { get; set; } = null!;
    public string ProjectName { get => Project != null ? Project.Name : ""; }

    public virtual RagTemplate? Template { get; set; }

    public virtual User? User { get; set; }

    public virtual RagCalification Calification { get; set; } = null!;
}
