using System;
using System.Collections.Generic;

namespace DataContext;

public partial class FeedBackUser
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public DateTime Period { get; set; }
    public DateOnly PeriodDateOnly { get => DateOnly.FromDateTime(Period); }

    public bool? Active { get; set; }

    public DateTime Creation { get; set; }

    public DateTime? Modification { get; set; }

    public DateTime? Deleted { get; set; }

    public virtual ICollection<FeedBackComment> FeedbackComments { get; set; } = new List<FeedBackComment>();

    public virtual ICollection<FeedBackImprovementUser> FeedbackImprovementUsers { get; set; } = new List<FeedBackImprovementUser>();

    public virtual ICollection<FeedBackPeriod> FeedbackPeriods { get; set; } = new List<FeedBackPeriod>();

	public virtual User User { get; set; } = null!;
}
