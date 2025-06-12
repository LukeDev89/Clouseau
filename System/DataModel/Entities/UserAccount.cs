using DataContext;
using DataModel.Enums;
using System;
using System.Collections.Generic;

namespace DataContext;

public partial class UserAccount
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public short ServiceId { get; set; }
    public AccountType Service { get { return (AccountType)ServiceId; } }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual User User { get; set; } = null!;
    public string UserUsername { get { return User.Username; } }
}
