using System;
using System.Collections.Generic;
using DataModel.Dto;
using Microsoft.EntityFrameworkCore;

namespace DataContext;

public partial class ClouseauContext : DbContext
{
    public ClouseauContext()
    {
    }

    public ClouseauContext(DbContextOptions<ClouseauContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ProgresoTarea> ProgresoTareas { get; set; }
    public virtual DbSet<ProgresoHoras> ProgresoHoras { get; set; }
    public virtual DbSet<HorasTareaRol> HorasTareaRoles { get; set; }

    public virtual DbSet<ActionsAudit> ActionsAudits { get; set; }

    public virtual DbSet<Applicant> Applicants { get; set; }

    public virtual DbSet<ApplicantsPosition> ApplicantsPositions { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientContract> ClientContracts { get; set; }

    public virtual DbSet<ClientResponsible> ClientResponsibles { get; set; }

    public virtual DbSet<CustomPermission> CustomPermissions { get; set; }

    public virtual DbSet<FeedbackComment> FeedbackComments { get; set; }

    public virtual DbSet<FeedbackImprovement> FeedbackImprovements { get; set; }

    public virtual DbSet<FeedbackImprovementUser> FeedbackImprovementUsers { get; set; }

    public virtual DbSet<FeedbackItem> FeedbackItems { get; set; }

    public virtual DbSet<FeedbackPeriod> FeedbackPeriods { get; set; }

    public virtual DbSet<FeedbackStandar> FeedbackStandars { get; set; }

    public virtual DbSet<FeedbackStandarType> FeedbackStandarTypes { get; set; }

    public virtual DbSet<FeedbackUser> FeedbackUsers { get; set; }

    public virtual DbSet<GadgetExpress> GadgetExpresses { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<InventoryBrand> InventoryBrands { get; set; }

    public virtual DbSet<InventoryComment> InventoryComments { get; set; }

    public virtual DbSet<InventoryHistory> InventoryHistories { get; set; }

    public virtual DbSet<InventoryLicense> InventoryLicenses { get; set; }

    public virtual DbSet<InventoryLicenseType> InventoryLicenseTypes { get; set; }

    public virtual DbSet<InventoryModel> InventoryModels { get; set; }

    public virtual DbSet<InventoryPart> InventoryParts { get; set; }

    public virtual DbSet<InventoryPartType> InventoryPartTypes { get; set; }

    public virtual DbSet<InventorySoftware> InventorySoftwares { get; set; }

    public virtual DbSet<InventoryUser> InventoryUsers { get; set; }

    public virtual DbSet<IssuesReporter> IssuesReporters { get; set; }

    public virtual DbSet<LicenseType> LicenseTypes { get; set; }

    public virtual DbSet<NonworkingDay> NonworkingDays { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<ProfilePermission> ProfilePermissions { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectTask> ProjectTasks { get; set; }

    public virtual DbSet<ProjectTaskEstimation> ProjectTaskEstimations { get; set; }

    public virtual DbSet<ProjectTaskManagement> ProjectTaskManagements { get; set; }

    public virtual DbSet<RagCalification> RagCalifications { get; set; }

    public virtual DbSet<RagFactor> RagFactors { get; set; }

    public virtual DbSet<RagFormula> RagFormulas { get; set; }

    public virtual DbSet<RagFormulasCalification> RagFormulasCalifications { get; set; }

    public virtual DbSet<RagStatus> RagStatuses { get; set; }

    public virtual DbSet<RagTemplate> RagTemplates { get; set; }

    public virtual DbSet<RagTemplateFactor> RagTemplateFactors { get; set; }

    public virtual DbSet<ResourceWarning> ResourceWarnings { get; set; }

    public virtual DbSet<ResourceWarningHistory> ResourceWarningHistories { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<SystemConfig> SystemConfigs { get; set; }

    public virtual DbSet<TaskProgress> TaskProgresses { get; set; }

    public virtual DbSet<TaskType> TaskTypes { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<UserClientContract> UserClientContracts { get; set; }

    public virtual DbSet<UserNonworkingDay> UserNonworkingDays { get; set; }

    public virtual DbSet<UserPresence> UserPresences { get; set; }

    public virtual DbSet<UserTeam> UserTeams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ClouseauContext");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<ProgresoTarea>().HasNoKey();
        modelBuilder.Entity<ProgresoHoras>().HasNoKey();
        modelBuilder.Entity<HorasTareaRol>().HasNoKey();

        modelBuilder.Entity<ActionsAudit>(entity =>
        {
            entity.ToTable("ActionsAudit");

            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("action");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("description");
            entity.Property(e => e.Entity)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("entity");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.ActionsAudits)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActionsAudit_Users");
        });

        modelBuilder.Entity<Applicant>(entity =>
        {
            entity.ToTable("applicants");

            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .HasColumnName("email");
            entity.Property(e => e.FilePath).HasColumnName("filePath");
            entity.Property(e => e.Fullname)
                .HasMaxLength(250)
                .HasColumnName("fullname");
            entity.Property(e => e.Linkedin)
                .HasMaxLength(250)
                .HasColumnName("linkedin");
            entity.Property(e => e.Other)
                .HasMaxLength(250)
                .HasColumnName("other");
        });

        modelBuilder.Entity<ApplicantsPosition>(entity =>
        {
            entity.ToTable("applicantsPositions");

            entity.Property(e => e.ApplicantId).HasColumnName("applicantId");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasOne(d => d.Applicant).WithMany(p => p.ApplicantsPositions)
                .HasForeignKey(d => d.ApplicantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_applicantsPositions_applicants");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.Income)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("income");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("name");
            entity.Property(e => e.Outcome)
                .HasColumnType("date")
                .HasColumnName("outcome");
        });

        modelBuilder.Entity<ClientContract>(entity =>
        {
            entity.ToTable("ClientContract");

            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientContracts)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientContract_Clients");
        });

        modelBuilder.Entity<ClientResponsible>(entity =>
        {
            entity.ToTable("ClientResponsible");

            entity.Property(e => e.Mail).HasColumnName("mail");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnName("name");
            entity.Property(e => e.ClientId)
                .HasColumnName("clientId");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientResponsibles)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientResponsible_Clients");
        });

        modelBuilder.Entity<CustomPermission>(entity =>
        {
            entity.ToTable("CustomPermission");

            entity.Property(e => e.SectionId).HasColumnName("sectionId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Section).WithMany(p => p.CustomPermissions)
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomPermission_Sections");

            entity.HasOne(d => d.User).WithMany(p => p.CustomPermissions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomPermission_Users");
        });

        modelBuilder.Entity<FeedbackComment>(entity =>
        {
            entity.ToTable("feedbackComment");

            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.FeedbackUserId).HasColumnName("feedbackUserId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.FeedbackUser).WithMany(p => p.FeedbackComments)
                .HasForeignKey(d => d.FeedbackUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_feedbackComment_feedbackUser");

            entity.HasOne(d => d.User).WithMany(p => p.FeedbackComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_feedbackComment_Users");
        });

        modelBuilder.Entity<FeedbackImprovement>(entity =>
        {
            entity.ToTable("feedbackImprovement");

            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<FeedbackImprovementUser>(entity =>
        {
            entity.ToTable("feedbackImprovementUser");

            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.FeedbackImprovementId).HasColumnName("feedbackImprovementId");
            entity.Property(e => e.FeedbackUserId).HasColumnName("feedbackUserId");

            entity.HasOne(d => d.FeedbackImprovement).WithMany(p => p.FeedbackImprovementUsers)
                .HasForeignKey(d => d.FeedbackImprovementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_feedbackImprovementUser_feedbackImprovement");

            entity.HasOne(d => d.FeedbackUser).WithMany(p => p.FeedbackImprovementUsers)
                .HasForeignKey(d => d.FeedbackUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_feedbackImprovementUser_feedbackUser");
        });

        modelBuilder.Entity<FeedbackItem>(entity =>
        {
            entity.ToTable("feedbackItem");

            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.FeedbackStandarId).HasColumnName("feedbackStandarId");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.FeedbackStandar).WithMany(p => p.FeedbackItems)
                .HasForeignKey(d => d.FeedbackStandarId)
                .HasConstraintName("FK_feedbackItem_feedbackStandar");
        });

        modelBuilder.Entity<FeedbackPeriod>(entity =>
        {
            entity.ToTable("feedbackPeriod");

            entity.Property(e => e.FeedbackItemId).HasColumnName("feedbackItemId");
            entity.Property(e => e.FeedbackStandarTypeId).HasColumnName("feedbackStandarTypeId");
            entity.Property(e => e.FeedbackUserId).HasColumnName("feedbackUserId");

            entity.HasOne(d => d.FeedbackItem).WithMany(p => p.FeedbackPeriods)
                .HasForeignKey(d => d.FeedbackItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_feedbackPeriod_feedbackItem");

            entity.HasOne(d => d.FeedbackStandarType).WithMany(p => p.FeedbackPeriods)
                .HasForeignKey(d => d.FeedbackStandarTypeId)
                .HasConstraintName("FK_feedbackPeriod_feedbackStandarType");

            entity.HasOne(d => d.FeedbackUser).WithMany(p => p.FeedbackPeriods)
                .HasForeignKey(d => d.FeedbackUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_feedbackPeriod_feedbackUser");
        });

        modelBuilder.Entity<FeedbackStandar>(entity =>
        {
            entity.ToTable("feedbackStandar");

            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<FeedbackStandarType>(entity =>
        {
            entity.ToTable("feedbackStandarType");

            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.FeedbackStandarId).HasColumnName("feedbackStandarId");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasOne(d => d.FeedbackStandar).WithMany(p => p.FeedbackStandarTypes)
                .HasForeignKey(d => d.FeedbackStandarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_feedbackStandarType_feedbackStandar");
        });

        modelBuilder.Entity<FeedbackUser>(entity =>
        {
            entity.ToTable("feedbackUser");

            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.Creation)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creation");
            entity.Property(e => e.Deleted)
                .HasColumnType("datetime")
                .HasColumnName("deleted");
            entity.Property(e => e.Modification)
                .HasColumnType("datetime")
                .HasColumnName("modification");
            entity.Property(e => e.Period)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("period");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.FeedbackUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_feedbackUser_Users");
        });

        modelBuilder.Entity<GadgetExpress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_gadgetExpress");

            entity.ToTable("GadgetExpress");

            entity.Property(e => e.ActiveFriday).HasColumnName("activeFriday");
            entity.Property(e => e.ActiveMonday).HasColumnName("activeMonday");
            entity.Property(e => e.ActiveThursday).HasColumnName("activeThursday");
            entity.Property(e => e.ActiveTuesday).HasColumnName("activeTuesday");
            entity.Property(e => e.ActiveWednesday).HasColumnName("activeWednesday");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("description");
            entity.Property(e => e.EndDay)
                .HasColumnType("date")
                .HasColumnName("endDay");
            entity.Property(e => e.EndTime).HasColumnName("endTime");
            entity.Property(e => e.Executions).HasColumnName("executions");
            entity.Property(e => e.Icon)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("icon");
            entity.Property(e => e.IntervalHours).HasColumnName("intervalHours");
            entity.Property(e => e.Message)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("message");
            entity.Property(e => e.NextExecution)
                .HasColumnType("datetime")
                .HasColumnName("nextExecution");
            entity.Property(e => e.StartDay)
                .HasColumnType("date")
                .HasColumnName("startDay");
            entity.Property(e => e.StartTime).HasColumnName("startTime");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("title");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.ToTable("inventory");

            entity.Property(e => e.Crystal)
                .HasMaxLength(50)
                .HasColumnName("crystal");
            entity.Property(e => e.Domain)
                .HasMaxLength(50)
                .HasColumnName("domain");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("endDate");
            entity.Property(e => e.Memory)
                .HasMaxLength(50)
                .HasColumnName("memory");
            entity.Property(e => e.ModelId).HasColumnName("modelId");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Processor)
                .HasMaxLength(50)
                .HasColumnName("processor");
            entity.Property(e => e.Serial)
                .HasMaxLength(50)
                .HasColumnName("serial");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("startDate");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.Storage)
                .HasMaxLength(50)
                .HasColumnName("storage");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Model).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inventory_inventoryModel");
        });

        modelBuilder.Entity<InventoryBrand>(entity =>
        {
            entity.ToTable("inventoryBrand");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<InventoryComment>(entity =>
        {
            entity.ToTable("inventoryComment");

            entity.Property(e => e.Comment)
                .IsUnicode(false)
                .HasColumnName("comment");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.InventoryId).HasColumnName("inventoryId");

            entity.HasOne(d => d.Inventory).WithMany(p => p.InventoryComments)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inventoryComment_inventory");
        });

        modelBuilder.Entity<InventoryHistory>(entity =>
        {
            entity.ToTable("inventoryHistory");

            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.InventoryId).HasColumnName("inventoryId");
            entity.Property(e => e.InventoryLicenseId).HasColumnName("inventoryLicenseId").IsRequired(false);
            entity.Property(e => e.InventoryPartId).HasColumnName("inventoryPartId").IsRequired(false);
            entity.Property(e => e.Observation)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("observation");

            entity.HasOne(d => d.Inventory).WithMany(p => p.InventoryHistories)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inventoryHistory_inventory");

            entity.HasOne(d => d.InventoryLicense).WithMany(p => p.InventoryHistories)
                .HasForeignKey(d => d.InventoryLicenseId)
                .HasConstraintName("FK_inventoryHistory_inventoryLicense");

            entity.HasOne(d => d.InventoryPart).WithMany(p => p.InventoryHistories)
                .HasForeignKey(d => d.InventoryPartId)
                .HasConstraintName("FK_inventoryHistory_inventoryParts");
        });

        modelBuilder.Entity<InventoryLicense>(entity =>
        {
            entity.ToTable("inventoryLicense");

            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("description");
            entity.Property(e => e.LicenseTypeId).HasColumnName("licenseTypeId");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("name");
            entity.Property(e => e.RequestDate)
                .HasColumnType("date")
                .HasColumnName("requestDate");
            entity.Property(e => e.RequestFinished)
                .HasColumnType("date")
                .HasColumnName("requestFinished");
            entity.Property(e => e.Serial)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("serial");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.LicenseType).WithMany(p => p.InventoryLicenses)
                .HasForeignKey(d => d.LicenseTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inventoryLicense_inventoryLicenseType");

            entity.HasOne(d => d.User).WithMany(p => p.InventoryLicenses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_inventoryLicense_Users");
        });

        modelBuilder.Entity<InventoryLicenseType>(entity =>
        {
            entity.ToTable("inventoryLicenseType");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<InventoryModel>(entity =>
        {
            entity.ToTable("inventoryModel");

            entity.Property(e => e.BrandId).HasColumnName("brandId");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("name");

            entity.HasOne(d => d.Brand).WithMany(p => p.InventoryModels)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inventoryModel_inventoryBrand");
        });

        modelBuilder.Entity<InventoryPart>(entity =>
        {
            entity.ToTable("inventoryParts");

            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PartTypeId).HasColumnName("partTypeId");
            entity.Property(e => e.Serial)
                .HasMaxLength(100)
                .HasColumnName("serial");

            entity.HasOne(d => d.PartType).WithMany(p => p.InventoryParts)
                .HasForeignKey(d => d.PartTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inventoryParts_inventoryPartType");
        });

        modelBuilder.Entity<InventoryPartType>(entity =>
        {
            entity.ToTable("inventoryPartType");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<InventorySoftware>(entity =>
        {
            entity.ToTable("inventorySoftware");

            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.InventoryId).HasColumnName("inventoryId");
            entity.Property(e => e.InventoryLicenseId).HasColumnName("inventoryLicenseId");

            entity.HasOne(d => d.Inventory).WithMany(p => p.InventorySoftwares)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inventorySoftware_inventory");

            entity.HasOne(d => d.InventoryLicense).WithMany(p => p.InventorySoftwares)
                .HasForeignKey(d => d.InventoryLicenseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inventorySoftware_inventoryLicense");
        });

        modelBuilder.Entity<InventoryUser>(entity =>
        {
            entity.ToTable("inventoryUser");

            entity.Property(e => e.DateFrom)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("dateFrom");
            entity.Property(e => e.DateTo)
                .HasColumnType("date")
                .HasColumnName("dateTo");
            entity.Property(e => e.InventoryId).HasColumnName("inventoryId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Inventory).WithMany(p => p.InventoryUsers)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inventoryUser_inventory");

            entity.HasOne(d => d.User).WithMany(p => p.InventoryUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inventoryUser_Users");
        });

        modelBuilder.Entity<IssuesReporter>(entity =>
        {
            entity.ToTable("issuesReporter");

            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("endDate");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("startDate");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((1))")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<LicenseType>(entity =>
        {
            entity.ToTable("LicenseType");

            entity.Property(e => e.ConsecutiveDays).HasColumnName("consecutiveDays");
            entity.Property(e => e.FileRequired).HasColumnName("fileRequired");
            entity.Property(e => e.HelperMessage)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("helperMessage");
            entity.Property(e => e.MaxDaysPerYear).HasColumnName("maxDaysPerYear");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<NonworkingDay>(entity =>
        {
            entity.Property(e => e.Day)
                .HasColumnType("date")
                .HasColumnName("day");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("description");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<ProfilePermission>(entity =>
        {
            entity.ToTable("ProfilePermission");

            entity.Property(e => e.ProfileId).HasColumnName("profileId");
            entity.Property(e => e.SectionId).HasColumnName("sectionId");

            entity.HasOne(d => d.Profile).WithMany(p => p.ProfilePermissions)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProfilePermission_Profiles");

            entity.HasOne(d => d.Section).WithMany(p => p.ProfilePermissions)
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProfilePermission_Sections");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.ClientResponsibleId).HasColumnName("clientResponsibleId");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("created");
            entity.Property(e => e.Deleted)
                .HasColumnType("date")
                .HasColumnName("deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("name");
            entity.Property(e => e.TeamId).HasColumnName("teamId");

            entity.HasOne(d => d.Client).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projects_Clients");

            entity.HasOne(d => d.ClientResponsible).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ClientResponsibleId)
                .HasConstraintName("FK_Projects_ClientResponsible");

            entity.HasOne(d => d.Team).WithMany(p => p.Projects)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK_Projects_Teams");
        });

        modelBuilder.Entity<ProjectTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Tasks");

            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeliverDate)
                .HasColumnType("date")
                .HasColumnName("deliverDate");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("endDate");
            entity.Property(e => e.EstimatedHours)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("estimatedHours");
            entity.Property(e => e.Finished).HasColumnName("finished");
            entity.Property(e => e.InitDate)
                .HasColumnType("date")
                .HasColumnName("initDate");
            entity.Property(e => e.KanbanId)
                .HasMaxLength(50)
                .HasColumnName("kanbanId");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("name");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.TaskTypeId).HasColumnName("taskTypeId");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectTasks)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_Tasks_Projects");

            entity.HasOne(d => d.TaskType).WithMany(p => p.ProjectTasks)
                .HasForeignKey(d => d.TaskTypeId)
                .HasConstraintName("FK_ProjectTasks_TaskTypes");
        });

        modelBuilder.Entity<ProjectTaskEstimation>(entity =>
        {
            entity.ToTable("ProjectTaskEstimation");

            entity.Property(e => e.Hours).HasColumnName("hours");
            entity.Property(e => e.ProjectTaskId).HasColumnName("projectTaskId");
            entity.Property(e => e.RoleId).HasColumnName("roleId");

            entity.HasOne(d => d.ProjectTask).WithMany(p => p.ProjectTaskEstimations)
                .HasForeignKey(d => d.ProjectTaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectTaskEstimation_ProjectTasks");

            entity.HasOne(d => d.Role).WithMany(p => p.ProjectTaskEstimations)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectTaskEstimation_Roles");
        });

        modelBuilder.Entity<ProjectTaskManagement>(entity =>
        {
            entity.ToTable("ProjectTaskManagement");

            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.HoursIncurred).HasColumnName("hoursIncurred");
            entity.Property(e => e.NextWeekProgressPrediction).HasColumnName("nextWeekProgressPrediction");
            entity.Property(e => e.Observation).HasColumnName("observation");
            entity.Property(e => e.ObservationResponsible).HasColumnName("observationResponsible");
            entity.Property(e => e.ProjectTaskId).HasColumnName("projectTaskId");
            entity.Property(e => e.RealWeekProgress).HasColumnName("realWeekProgress");

            entity.HasOne(d => d.ProjectTask).WithMany(p => p.ProjectTaskManagements)
                .HasForeignKey(d => d.ProjectTaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectTaskManagement_ProjectTasks");
        });

        modelBuilder.Entity<RagCalification>(entity =>
        {
            entity.ToTable("ragCalification");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Value).HasColumnName("value");
        });

        modelBuilder.Entity<RagFactor>(entity =>
        {
            entity.ToTable("ragFactor");

            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("name");
            entity.Property(e => e.Objetive).IsUnicode(false);
        });

        modelBuilder.Entity<RagFormula>(entity =>
        {
            entity.ToTable("ragFormulas");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<RagFormulasCalification>(entity =>
        {
            entity.ToTable("ragFormulasCalification");

            entity.Property(e => e.NewValue).HasColumnName("newValue");
            entity.Property(e => e.RagCalificationId).HasColumnName("ragCalificationId");

            entity.HasOne(d => d.RagCalification).WithMany(p => p.RagFormulasCalifications)
                .HasForeignKey(d => d.RagCalificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ragFormulasCalification_ragCalification");
        });

        modelBuilder.Entity<RagStatus>(entity =>
        {
            entity.ToTable("ragStatus");

            entity.Property(e => e.CalificationId).HasColumnName("calificationId");
            entity.Property(e => e.Comment)
                .IsUnicode(false)
                .HasColumnName("comment");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.FactorId).HasColumnName("factorId");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.TemplateId).HasColumnName("templateId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Calification).WithMany(p => p.RagStatuses)
                .HasForeignKey(d => d.CalificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ragStatus_ragCalification");

            entity.HasOne(d => d.Factor).WithMany(p => p.RagStatuses)
                .HasForeignKey(d => d.FactorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ragStatus_ragFactor");

            entity.HasOne(d => d.Project).WithMany(p => p.RagStatuses)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ragStatus_Projects");

            entity.HasOne(d => d.Template).WithMany(p => p.RagStatuses)
                .HasForeignKey(d => d.TemplateId)
                .HasConstraintName("FK_ragStatus_ragTemplate");

            entity.HasOne(d => d.User).WithMany(p => p.RagStatuses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_ragStatus_Users");
        });

        modelBuilder.Entity<RagTemplate>(entity =>
        {
            entity.ToTable("ragTemplate");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("name");
            entity.Property(e => e.Period)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("period");
            entity.Property(e => e.RagFormulaId).HasColumnName("ragFormulaId");

            entity.HasOne(d => d.RagFormula).WithMany(p => p.RagTemplates)
                .HasForeignKey(d => d.RagFormulaId)
                .HasConstraintName("FK_ragTemplate_ragFormulas");
        });

        modelBuilder.Entity<RagTemplateFactor>(entity =>
        {
            entity.ToTable("ragTemplateFactor");

            entity.Property(e => e.FactorId).HasColumnName("factorId");
            entity.Property(e => e.TemplateId).HasColumnName("templateId");

            entity.HasOne(d => d.Factor).WithMany(p => p.RagTemplateFactors)
                .HasForeignKey(d => d.FactorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ragTemplateFactor_ragFactor");

            entity.HasOne(d => d.Template).WithMany(p => p.RagTemplateFactors)
                .HasForeignKey(d => d.TemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ragTemplateFactor_ragTemplate");
        });

        modelBuilder.Entity<ResourceWarning>(entity =>
        {
            entity.ToTable("resourceWarning");

            entity.Property(e => e.AdvertiserId).HasColumnName("advertiserId");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Advertiser).WithMany(p => p.ResourceWarningAdvertisers)
                .HasForeignKey(d => d.AdvertiserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_resourceWarning_Users1");

            entity.HasOne(d => d.User).WithMany(p => p.ResourceWarningUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_resourceWarning_Users");
        });

        modelBuilder.Entity<ResourceWarningHistory>(entity =>
        {
            entity.ToTable("resourceWarningHistory");

            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.ResourceWarningId).HasColumnName("resourceWarningId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.ResourceWarning).WithMany(p => p.ResourceWarningHistories)
                .HasForeignKey(d => d.ResourceWarningId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_resourceWarningHistory_resourceWarning");

            entity.HasOne(d => d.User).WithMany(p => p.ResourceWarningHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_resourceWarningHistory_Users");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("description");
            entity.Property(e => e.Hierarchy)
                .HasDefaultValueSql("((1))")
                .HasColumnName("hierarchy");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<SystemConfig>(entity =>
        {
            entity.ToTable("systemConfig");

            entity.Property(e => e.DataType).HasColumnName("dataType");
            entity.Property(e => e.DataValue)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("dataValue");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("name");
            entity.Property(e => e.PreviousValue)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("previousValue");
        });

        modelBuilder.Entity<TaskProgress>(entity =>
        {
            entity.ToTable("TaskProgress");

            entity.Property(e => e.Comment)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("comment");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Hours)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("hours");
            entity.Property(e => e.ExtraHours)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("extraHours");
            entity.Property(e => e.TaskId).HasColumnName("taskId");
            entity.Property(e => e.TaskTypeId).HasColumnName("taskTypeId");
            entity.Property(e => e.UserAuditId).HasColumnName("userAuditId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskProgresses)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaskProgress_Tasks");

            entity.HasOne(d => d.TaskType).WithMany(p => p.TaskProgresses)
                .HasForeignKey(d => d.TaskTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaskProgress_TaskTypes");

            entity.HasOne(d => d.User).WithMany(p => p.TaskProgresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaskProgress_Users");
        });

        modelBuilder.Entity<TaskType>(entity =>
        {
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("created");
            entity.Property(e => e.Deleted)
                .HasColumnType("date")
                .HasColumnName("deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Birth)
                .HasColumnType("date")
                .HasColumnName("birth");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("email");
            entity.Property(e => e.ExternalDetail)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("externalDetail");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("firstname");
            entity.Property(e => e.Income)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("income");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.IsExternal).HasColumnName("isExternal");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("lastname");
            entity.Property(e => e.Outcome)
                .HasColumnType("date")
                .HasColumnName("outcome");
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .IsFixedLength()
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("phoneNumber");
            entity.Property(e => e.ProfileId).HasColumnName("profileId");
            entity.Property(e => e.Province)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("province");
            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.RollOff)
                .HasColumnType("date")
                .HasColumnName("rollOff");
            entity.Property(e => e.RollOn)
                .HasColumnType("date")
                .HasColumnName("rollOn");
            entity.Property(e => e.Seniority)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("seniority");
            entity.Property(e => e.Superior).HasColumnName("superior");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("username");

            entity.HasOne(d => d.Profile).WithMany(p => p.Users)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Profiles");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");

            entity.HasOne(d => d.SuperiorNavigation).WithMany(p => p.InverseSuperiorNavigation)
                .HasForeignKey(d => d.Superior)
                .HasConstraintName("FK_Users_Users");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_usersAccounts");

            entity.HasIndex(e => e.Username, "IX_usersAccounts").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsFixedLength()
                .HasColumnName("email");
            entity.Property(e => e.ServiceId).HasColumnName("serviceId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("username");

            entity.HasOne(d => d.User).WithMany(p => p.UserAccounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_usersAccounts_Users");
        });

        modelBuilder.Entity<UserClientContract>(entity =>
        {
            entity.ToTable("userClientContract");

            entity.Property(e => e.ClientContractId).HasColumnName("clientContractId");
            entity.Property(e => e.Hours)
                .HasDefaultValueSql("((160))")
                .HasColumnName("hours");
            entity.Property(e => e.Service)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("service");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.ClientContract).WithMany(p => p.UserClientContracts)
                .HasForeignKey(d => d.ClientContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_userClientContract_ClientContract");
        });

        modelBuilder.Entity<UserNonworkingDay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserNonworkingDays_1");

            entity.Property(e => e.Comment)
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("comment");
            entity.Property(e => e.CommentCfo)
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("commentCfo");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.DateFrom)
                .HasColumnType("date")
                .HasColumnName("dateFrom");
            entity.Property(e => e.DateTo)
                .HasColumnType("date")
                .HasColumnName("dateTo");
            entity.Property(e => e.Path)
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("path");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.TypeId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("typeId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Type).WithMany(p => p.UserNonworkingDays)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserNonworkingDays_LicenseType");

            entity.HasOne(d => d.User).WithMany(p => p.UserNonworkingDays)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserNonworkingDays_Users1");
        });

        modelBuilder.Entity<UserPresence>(entity =>
        {
            entity.ToTable("UserPresence");

            entity.Property(e => e.Egress).HasColumnName("egress");
            entity.Property(e => e.Ingress).HasColumnName("ingress");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.WeekDay).HasColumnName("weekDay");

            entity.HasOne(d => d.User).WithMany(p => p.UserPresences)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPresence_Users");
        });

        modelBuilder.Entity<UserTeam>(entity =>
        {
            entity.Property(e => e.AssignmentSold)
                .HasDefaultValueSql("((160))")
                .HasColumnName("assignmentSold");
            entity.Property(e => e.Comments).HasColumnName("comments");
            entity.Property(e => e.Income)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("income");
            entity.Property(e => e.Outcome)
                .HasColumnType("date")
                .HasColumnName("outcome");
            entity.Property(e => e.RealAssignment)
                .HasDefaultValueSql("((160))")
                .HasColumnName("realAssignment");
            entity.Property(e => e.RequireManagement).HasColumnName("requireManagement");
            entity.Property(e => e.RollOff)
                .HasColumnType("date")
                .HasColumnName("rollOff");
            entity.Property(e => e.RollOn)
                .HasColumnType("date")
                .HasColumnName("rollOn");
            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.Temporary).HasColumnName("temporary");
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public async Task<List<ProgresoTarea>> GetProgresoTareasPorFechaAsync(DateTime fechaInicio, long clientId)
    {
        return await ProgresoTareas.FromSqlInterpolated($"SELECT * FROM dbo.fn_ProgresoTareasPorFecha({fechaInicio}, {clientId})").ToListAsync();
    }

    public async Task<List<ProgresoHoras>> GetProgresoHorasPorFechaAsync(DateTime fechaInicio, long clientId)
    {
        return await ProgresoHoras.FromSqlInterpolated($"SELECT * FROM dbo.fn_ProgresoHorasPorFecha({fechaInicio}, {clientId})").ToListAsync();
    }

    public async Task<List<HorasTareaRol>> GetHorasTareasRolAsync(DateTime fechaInicio, long clientId)
    {
        return await HorasTareaRoles.FromSqlInterpolated($"SELECT * FROM dbo.fn_HorasPorTareaYRol({fechaInicio}, {clientId})").ToListAsync();
    }
}