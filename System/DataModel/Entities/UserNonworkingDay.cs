using DataContext;
using DataModel.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DataContext;

public partial class UserNonworkingDay
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public DateTime DateFrom { get; set; }
    public DateOnly _DateFrom { get { return DateOnly.FromDateTime(DateFrom); } }

    public DateTime DateTo { get; set; }
    public DateOnly _DateTo { get { return DateOnly.FromDateTime(DateTo); } }

    public long TypeId { get; set; }

    public string? Comment { get; set; }
    public string? CommentCfo { get; set; }

    public string? Path { get; set; }

    public short State { get; set; } = 0;

    public DateTime Created { get; set; } = DateTime.Now;
    public DateOnly _Created { get { return DateOnly.FromDateTime(Created); } }

    public virtual User User { get; set; } = null!;
    public string UserUsername { get { return User.Username; } }
    public string UserFullname { get { return $"{User.Lastname}, {User.Firstname}"; } }

    public virtual LicenseType Type { get; set; } = null!;
    public string TypeName { get => Type != null ? Type.Name : ""; }
}
