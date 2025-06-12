using System;
using System.Collections.Generic;

namespace DataContext;

public partial class FeedBackImprovementUser
{
    public long Id { get; set; }

    public long FeedbackImprovementId { get; set; }

    public long FeedbackUserId { get; set; }

    public string? Description { get; set; }

    public virtual FeedBackImprovement FeedbackImprovement { get; set; } = null!;

    public virtual FeedBackUser FeedbackUser { get; set; } = null!;
}
