using System;
using System.Collections.Generic;

namespace DataContext;

public partial class UserClientContract
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long ClientContractId { get; set; }

    public int Hours { get; set; }

    public bool? Service { get; set; }

    public virtual ClientContract ClientContract { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
