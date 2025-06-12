using System;
using System.Collections.Generic;

namespace DataContext;

public partial class FeedbackUser
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public DateTime Period { get; set; }
    public DateOnly PeriodDateOnly { get => DateOnly.FromDateTime(Period); }

    public bool? Active { get; set; }

    public DateTime Creation { get; set; }

    public DateTime? Modification { get; set; }

    public DateTime? Deleted { get; set; }

    public virtual ICollection<FeedbackComment> FeedbackComments { get; set; } = new List<FeedbackComment>();

    public virtual ICollection<FeedbackImprovementUser> FeedbackImprovementUsers { get; set; } = new List<FeedbackImprovementUser>();

    public virtual ICollection<FeedbackPeriod> FeedbackPeriods { get; set; } = new List<FeedbackPeriod>();

	public virtual User User { get; set; } = null!;
}
