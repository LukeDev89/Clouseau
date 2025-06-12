using System;
using System.Collections.Generic;

namespace DataContext;

public partial class FeedBackStandarType
{
    public long Id { get; set; }

    public long FeedbackStandarId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<FeedBackPeriod> FeedbackPeriods { get; set; } = new List<FeedBackPeriod>();

    public virtual FeedBackStandar FeedbackStandar { get; set; } = null!;
}
