using System;
using System.Collections.Generic;

namespace DataContext;

public partial class InventoryPart
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Serial { get; set; }

    public long PartTypeId { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<InventoryHistory> InventoryHistories { get; set; } = new List<InventoryHistory>();
    public virtual InventoryPartType PartType { get; set; } = null!;

    
}
