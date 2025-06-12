using System;
using System.Collections.Generic;

namespace DataContext;

public partial class FeedBackImprovement
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool Active { get; set; }

    public virtual ICollection<FeedBackImprovementUser> FeedbackImprovementUsers { get; set; } = new List<FeedBackImprovementUser>();
}
