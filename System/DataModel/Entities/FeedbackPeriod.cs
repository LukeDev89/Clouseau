using System;
using System.Collections.Generic;

namespace DataContext;

public partial class FeedBackPeriod
{
    public long Id { get; set; }

    public long FeedbackUserId { get; set; }

    public long FeedbackItemId { get; set; }

    public long? FeedbackStandarTypeId { get; set; }

    public virtual FeedBackItem FeedbackItem { get; set; } = null!;

    public virtual FeedBackStandarType? FeedbackStandarType { get; set; }

    public virtual FeedBackUser FeedbackUser { get; set; } = null!;
}
