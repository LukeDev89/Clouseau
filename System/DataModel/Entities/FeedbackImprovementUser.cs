using System;
using System.Collections.Generic;

namespace DataContext;

public partial class FeedbackImprovementUser
{
    public long Id { get; set; }

    public long FeedbackImprovementId { get; set; }

    public long FeedbackUserId { get; set; }

    public string? Description { get; set; }

    public virtual FeedbackImprovement FeedbackImprovement { get; set; } = null!;

    public virtual FeedbackUser FeedbackUser { get; set; } = null!;
}
