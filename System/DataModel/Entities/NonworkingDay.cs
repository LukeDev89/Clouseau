using System;
using System.Collections.Generic;
using System.Globalization;

namespace DataContext;

public partial class NonworkingDay
{
    public long Id { get; set; }

    public DateTime Day { get; set; }

    public DateOnly _Day { get { return DateOnly.FromDateTime(Day); } }

    public string? Description { get; set; }
}
