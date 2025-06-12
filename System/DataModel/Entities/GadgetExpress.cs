namespace DataContext;

public partial class GadgetExpress
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Message { get; set; } = null!;

    public string? Icon { get; set; }

    public DateTime StartDay { get; set; } = DateTime.Now;
    public DateOnly _StartDay { get => DateOnly.FromDateTime(StartDay); }

    public TimeSpan StartTime { get; set; } = DateTime.Now.TimeOfDay;
    public TimeOnly _StartTime { get => TimeOnly.FromTimeSpan(StartTime); }

    public DateTime? EndDay { get; set; }
    public DateOnly? _EndDay { get => EndDay.HasValue ? DateOnly.FromDateTime(EndDay.Value) : null; }

    public TimeSpan? EndTime { get; set; }
    public TimeOnly? _EndTime { get => EndTime.HasValue ? TimeOnly.FromTimeSpan(EndTime.Value) : null; }

    public int? IntervalHours { get; set; }

    public int Executions { get; set; }

    public bool ActiveMonday { get; set; }

    public bool ActiveTuesday { get; set; }

    public bool ActiveWednesday { get; set; }

    public bool ActiveThursday { get; set; }

    public bool ActiveFriday { get; set; }

    public DateTime? NextExecution { get; set; }
}
