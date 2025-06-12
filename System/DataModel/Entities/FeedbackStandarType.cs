using System;
using System.Collections.Generic;

namespace DataContext;

public partial class FeedbackStandarType
{
    public long Id { get; set; }

    public long FeedbackStandarId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<FeedbackPeriod> FeedbackPeriods { get; set; } = new List<FeedbackPeriod>();

    public virtual FeedbackStandar FeedbackStandar { get; set; } = null!;
}
