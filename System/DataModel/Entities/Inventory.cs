using DataModel.Enums;
using System;
using System.Collections.Generic;

namespace DataContext;

public partial class Inventory
{
    public long Id { get; set; }

    public long ModelId { get; set; }

    public string Serial { get; set; } = null!;

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Domain { get; set; }

    public string? Processor { get; set; }

    public string? Memory { get; set; }

    public string? Storage { get; set; }

    public string? Crystal { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public short State { get; set; }

    public virtual ICollection<InventoryComment> InventoryComments { get; set; } = new List<InventoryComment>();

    public virtual ICollection<InventoryHistory> InventoryHistories { get; set; } = new List<InventoryHistory>();

    public virtual ICollection<InventorySoftware> InventorySoftwares { get; set; } = new List<InventorySoftware>();

    public virtual ICollection<InventoryUser> InventoryUsers { get; set; } = new List<InventoryUser>();

    public virtual InventoryModel Model { get; set; } = null!;

    public string AssignedUser
    {
        get => InventoryUsers == null || InventoryUsers.LastOrDefault() == null ? "No Asignado" :
        $"{InventoryUsers.LastOrDefault()?.User.UserFullName}";
    }

    public string? FullModelName { get => $"{Model.Brand.Name} {Model.Name}"; }
    public string? BrandName { get => $"{Model.Brand.Name}"; }

    public string StateName
    {
        get
        {
            return ((InventoryStatus)State).ToString();
        }
    }
}
