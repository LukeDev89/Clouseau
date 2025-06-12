using System;
using System.Collections.Generic;

namespace DataContext;

public partial class InventorySoftware
{
    public long Id { get; set; }

    public long InventoryId { get; set; }

    public long InventoryLicenseId { get; set; }

    public DateTime Date { get; set; }

    public virtual Inventory Inventory { get; set; } = null!;

    public virtual InventoryLicense InventoryLicense { get; set; } = null!;
}
