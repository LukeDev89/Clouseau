using System;
using System.Collections.Generic;

namespace DataContext;

public partial class FeedbackStandar
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<FeedbackItem> FeedbackItems { get; set; } = new List<FeedbackItem>();

    public virtual ICollection<FeedbackStandarType> FeedbackStandarTypes { get; set; } = new List<FeedbackStandarType>();
}
