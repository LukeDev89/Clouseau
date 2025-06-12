using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace DataContext;

public partial class FeedBackItem
{
    public long Id { get; set; }

    public long? FeedbackStandarId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<FeedBackPeriod> FeedbackPeriods { get; set; } = new List<FeedBackPeriod>();

    public virtual FeedBackStandar? FeedbackStandar { get; set; }

	[NotMapped] public string StandarName { get => FeedbackStandar != null ? FeedbackStandar.Name : ""; }
}
