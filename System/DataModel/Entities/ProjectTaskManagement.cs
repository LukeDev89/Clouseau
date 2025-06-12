using System;
using System.Collections.Generic;

namespace DataContext;

public partial class ProjectTaskManagement
{
    public long Id { get; set; }

    public long ProjectTaskId { get; set; }

    public int RealWeekProgress { get; set; }

    public int HoursIncurred { get; set; }

    public int NextWeekProgressPrediction { get; set; }

    public string? Observation { get; set; }

    public string? ObservationResponsible { get; set; }

    public DateTime Date { get; set; }

    public virtual ProjectTask ProjectTask { get; set; } = null!;
}
