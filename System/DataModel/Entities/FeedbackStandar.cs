using System;
using System.Collections.Generic;

namespace DataContext;

public partial class FeedBackStandar
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<FeedBackItem> FeedbackItems { get; set; } = new List<FeedBackItem>();

    public virtual ICollection<FeedBackStandarType> FeedbackStandarTypes { get; set; } = new List<FeedBackStandarType>();
}
