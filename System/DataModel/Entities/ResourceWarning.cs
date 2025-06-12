using System;
using System.Collections.Generic;

namespace DataContext;

public partial class ResourceWarning
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime Date { get; set; }
    public DateOnly _Date { get { return DateOnly.FromDateTime(Date); } }
    public short State { get; set; }

    public long AdvertiserId { get; set; }

    public virtual User Advertiser { get; set; } = null!;

    public virtual ICollection<ResourceWarningHistory> ResourceWarningHistories { get; set; } = new List<ResourceWarningHistory>();

    public virtual User User { get; set; } = null!;
}
