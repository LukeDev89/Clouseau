using System;
using System.Collections.Generic;

namespace DataContext;

public partial class FeedbackPeriod
{
    public long Id { get; set; }

    public long FeedbackUserId { get; set; }

    public long FeedbackItemId { get; set; }

    public long? FeedbackStandarTypeId { get; set; }

    public virtual FeedbackItem FeedbackItem { get; set; } = null!;

    public virtual FeedbackStandarType? FeedbackStandarType { get; set; }

    public virtual FeedbackUser FeedbackUser { get; set; } = null!;
}
