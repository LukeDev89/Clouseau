using System;
using System.Collections.Generic;

namespace DataContext;

public partial class ClientContract
{
    public long Id { get; set; }

    public long ClientId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<UserClientContract> UserClientContracts { get; set; } = new List<UserClientContract>();
}
