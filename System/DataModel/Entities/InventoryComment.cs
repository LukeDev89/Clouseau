using System;
using System.Collections.Generic;

namespace DataContext;

public partial class InventoryComment
{
    public long Id { get; set; }

    public long InventoryId { get; set; }

    public string? Comment { get; set; }

    public DateTime Date { get; set; }

    public virtual Inventory Inventory { get; set; } = null!;
}
