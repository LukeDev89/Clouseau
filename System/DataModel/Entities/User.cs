using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DataContext;

public partial class User
{
    public long Id { get; set; }

    public string Firstname { get { return _firstName.Trim(); } set { _firstName = value; } }
    private string _firstName = "";

    public string Lastname { get { return _lastName.Trim(); } set { _lastName = value; } }
    private string _lastName = "";

    public string Username { get { return _userName.Trim(); } set { _userName = value; } }
    private string _userName = "";

    public string Password { get { return _password.Trim(); } set { _password = value; } }
    private string _password = "";

    public string Email { get { return _email.Trim(); } set { _email = value; } }
    private string _email = "";

    public string PhoneNumber { get { return _phoneNumber.Trim(); } set { _phoneNumber = value; } }
    private string _phoneNumber = "";

    public string UserFullName { get { return $"{Lastname}, {Firstname}"; } }

    public DateTime? Birth { get; set; }

    public DateOnly? _Birth { get { return Birth.HasValue ? DateOnly.FromDateTime(Birth.Value) : (DateOnly?)null; } }

    public DateTime Income { get; set; }

    public DateOnly _Income { get { return DateOnly.FromDateTime(Income); } }

    public DateTime? Outcome { get; set; }

    public DateOnly? _Outcome { get { return Outcome.HasValue ? DateOnly.FromDateTime(Outcome.Value) : (DateOnly?)null; } }

    public DateTime? RollOn { get; set; }

    public DateTime? RollOff { get; set; }

    public bool IsActive { get; set; }

    public bool IsExternal { get; set; }

    public long? Superior { get; set; }

    public long RoleId { get; set; }

    public long ProfileId { get; set; }

    public string? ExternalDetail { get; set; }

    public string? Province { get; set; }

    public string? Seniority { get; set; }

    public virtual ICollection<ActionsAudit> ActionsAudits { get; set; } = new List<ActionsAudit>();

    public virtual ICollection<User> InverseSuperiorNavigation { get; set; } = new List<User>();

    public virtual Profile Profile { get; set; } = null!;
    public string ProfileName { get { return Profile.Name; } }

    public virtual Role? Role { get; set; }
    public string RoleName { get { return Role?.Name ?? ""; } }

    public virtual User? SuperiorNavigation { get; set; }
    public string SuperiorName { get { return SuperiorNavigation?.UserFullName ?? ""; } }

    public List<long> Permissions 
    { 
        get
        {
            var profilePermission = Profile.ProfilePermissions.Select(x => x.SectionId);
            var customPermission = CustomPermissions.Select(x => x.SectionId);

            var permissions = profilePermission.Union(customPermission).ToList();

            return permissions;
        } 
    }

    public virtual ICollection<TaskProgress> TaskProgresses { get; set; } = new List<TaskProgress>();

    public virtual ICollection<UserTeam> UserTeams { get; set; } = new List<UserTeam>();

    public virtual ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();

    public virtual ICollection<UserNonworkingDay> UserNonworkingDays { get; set; } = new List<UserNonworkingDay>();

    public virtual ICollection<RagStatus> RagStatuses { get; set; } = new List<RagStatus>();

    public virtual ICollection<FeedbackComment> FeedbackComments { get; set; } = new List<FeedbackComment>();

    public virtual ICollection<FeedbackUser> FeedbackUsers { get; set; } = new List<FeedbackUser>();

    public virtual ICollection<InventoryLicense> InventoryLicenses { get; set; } = new List<InventoryLicense>();

    public virtual ICollection<InventoryUser> InventoryUsers { get; set; } = new List<InventoryUser>();

    public virtual ICollection<CustomPermission> CustomPermissions { get; set; } = new List<CustomPermission>();

    public virtual ICollection<ResourceWarning> ResourceWarningAdvertisers { get; set; } = new List<ResourceWarning>();

    public virtual ICollection<ResourceWarningHistory> ResourceWarningHistories { get; set; } = new List<ResourceWarningHistory>();

    public virtual ICollection<ResourceWarning> ResourceWarningUsers { get; set; } = new List<ResourceWarning>();

    public virtual ICollection<TaskProgress> TaskProgressUserAudits { get; set; } = new List<TaskProgress>();

    public virtual ICollection<UserClientContract> UserClientContracts { get; set; } = new List<UserClientContract>();

    public virtual ICollection<UserPresence> UserPresences { get; set; } = new List<UserPresence>();
}
