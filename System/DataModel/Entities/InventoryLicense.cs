using System;
using System.Collections.Generic;

namespace DataContext;

public partial class InventoryLicense
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Serial { get; set; }

    public long LicenseTypeId { get; set; }

    public bool? Active { get; set; }

    public long? UserId { get; set; }

    public DateTime? RequestDate { get; set; }

    public DateTime? RequestFinished { get; set; }

    public virtual ICollection<InventoryHistory> InventoryHistories { get; set; } = new List<InventoryHistory>();

    public virtual ICollection<InventorySoftware> InventorySoftwares { get; set; } = new List<InventorySoftware>();

    public virtual InventoryLicenseType LicenseType { get; set; } = null!;

    public virtual User? User { get; set; }
}
