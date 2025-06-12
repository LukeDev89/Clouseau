using System;
using System.Collections.Generic;

namespace DataContext;

public partial class FeedbackComment
{
    public long Id { get; set; }

    public long FeedbackUserId { get; set; }

    public long UserId { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime Date { get; set; }

    public virtual FeedbackUser FeedbackUser { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
