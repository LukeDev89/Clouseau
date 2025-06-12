using System;
using System.Collections.Generic;

namespace DataContext;

public partial class InventoryLicenseType
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<InventoryLicense> InventoryLicenses { get; set; } = new List<InventoryLicense>();
}
