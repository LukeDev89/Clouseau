using System;
using System.Collections.Generic;

namespace DataContext;

public partial class InventoryHistory
{
    public long Id { get; set; }

    public long InventoryId { get; set; }

    public long? InventoryPartId { get; set; }

    public long? InventoryLicenseId { get; set; }

    public string? Observation { get; set; }

    public DateTime? Date { get; set; }

    public virtual Inventory Inventory { get; set; } = null!;

    public virtual InventoryLicense InventoryLicense { get; set; } = null!;

    public virtual InventoryPart InventoryPart { get; set; } = null!;
}