using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContext;

public partial class InventoryModel
{
    public long Id { get; set; }

    public long BrandId { get; set; }

    public string Name { get; set; } = null!;

    public virtual InventoryBrand Brand { get; set; } = null!;
    [NotMapped] public string BrandName { get => Brand != null ? Brand.Name : ""; }
    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    //public virtual ICollection<InventoryLaptop> InventoryLaptops { get; set; } = new List<InventoryLaptop>();
}
