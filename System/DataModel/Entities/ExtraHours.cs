using System;
using System.Collections.Generic;

namespace DataContext;

public partial class ExtraHours
{
    public long Id { get; set; }
    
    public long UserId { get; set; }
    
    public long TaskId { get; set; }
    
    public long TaskTypeId { get; set; }
    
    public DateTime Date { get; set; }
    
    public decimal Hours { get; set; }
    
    public string Comment { get { return _comment.Trim(); } set { _comment = value; } }
    private string _comment = "";
    
    public short Status { get; set; } // Using ExtraHoursStatus enum
    
    public DateTime Created { get; set; } = DateTime.Now;
    
    public DateTime? Reviewed { get; set; }
    
    public long? ReviewedBy { get; set; }
    
    public string? ReviewComment { get { return _reviewComment?.Trim(); } set { _reviewComment = value; } }
    private string? _reviewComment;

    public virtual User User { get; set; } = null!;
    
    public virtual ProjectTask Task { get; set; } = null!;
    
    public virtual ProjectTaskType TaskType { get; set; } = null!;
    
    public virtual User? ReviewedByUser { get; set; }
}