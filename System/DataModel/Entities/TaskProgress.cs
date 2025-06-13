using System;
using System.Collections.Generic;

namespace DataContext;

public partial class TaskProgress
{
    public long Id { get; set; }

    public long TaskId { get; set; }

    public long UserId { get; set; }

    public long TaskTypeId { get; set; }

    public string Comment { get { return _comment.Trim(); } set { _comment = value; } }
    private string _comment = "";

    public DateTime Date { get; set; }

    public decimal Hours { get; set; }

    public long? UserAuditId { get; set; }

    public virtual TaskType TaskType { get; set; } = null!;
    public string TaskTypeName { get { return TaskType.Name; } }

    public virtual ProjectTask Task { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual User? UserAudit { get; set; }
}
