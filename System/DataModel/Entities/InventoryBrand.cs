using System;
using System.Collections.Generic;

namespace DataContext;

public partial class InventoryBrand
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<InventoryModel> InventoryModels { get; set; } = new List<InventoryModel>();
}
