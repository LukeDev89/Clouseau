using System;
using System.Collections.Generic;

namespace DataContext;

public partial class LicenseType
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string HelperMessage { get; set; } = null!;

    public bool FileRequired { get; set; }

    public int ConsecutiveDays { get; set; }

    public int MaxDaysPerYear { get; set; }

    public virtual ICollection<UserNonworkingDay> UserNonworkingDays { get; set; } = new List<UserNonworkingDay>();
}
