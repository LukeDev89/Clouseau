using System;
using System.Collections.Generic;

namespace DataContext;

public partial class InventoryUser
{
    public long Id { get; set; }

    public long InventoryId { get; set; }

    public long UserId { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public virtual Inventory Inventory { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
