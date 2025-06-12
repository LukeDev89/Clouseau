using System;
using System.Collections.Generic;

namespace DataContext;

public partial class InventoryPartType
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<InventoryPart> InventoryParts { get; set; } = new List<InventoryPart>();
}
