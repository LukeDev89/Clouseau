using System;
using System.Collections.Generic;

namespace DataContext;

public partial class FeedbackImprovement
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<FeedbackImprovementUser> FeedbackImprovementUsers { get; set; } = new List<FeedbackImprovementUser>();
}
