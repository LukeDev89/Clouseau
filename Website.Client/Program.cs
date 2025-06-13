using Blazored.LocalStorage;
using DataContext;
using DataContext.Interfaces;
using DataContext.Interfaces.Management;
using DataContext.Interfaces.ProjectManagement;
using DataContext.Interfaces.SystemManagement;
using DataContext.Repositories;
using DataContext.Repositories.Management;
using DataContext.Repositories.ProjectManagement;
using DataContext.Repositories.SystemManagement;
using DataService.Auth;
using DataService.Interfaces.GadgetExpress;
using DataService.Interfaces.Management;
using DataService.Interfaces.ProjectManagement;
using DataService.Interfaces.SonarQube;
using DataService.Interfaces.SystemManagement;
using DataService.Interfaces.VersionControl;
using DataService.Services.GadgetExpress;
using DataService.Services.Management;
using DataService.Services.ProjectManagement;
using DataService.Services.SonarQube;
using DataService.Services.SystemManagement;
using DataService.Services.VersionControl;
using DataService.State;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddBlazorBootstrap();

builder.Services.AddOptions();
builder.Services.AddMvc();

builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

builder.Services.AddDbContextFactory<ClouseauContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("ClouseauContext")));

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddLocalization();

// Servicios y Repositorios

builder.Services.AddScoped<LoginState>();
builder.Services.AddScoped<ChangedState>();

builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<IVersionControlService, VersionControlService>();
builder.Services.AddScoped<ISonarQubeService, SonarQubeService>();

builder.Services.AddTransient<IGadgetExpressService, GadgetExpressService>();
builder.Services.AddTransient<IGadgetExpressRepository, GadgetExpressRepository>();

builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITeamService, TeamService>();
builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IProfileService, ProfileService>();
builder.Services.AddTransient<ITaskTypeService, TaskTypeService>();
builder.Services.AddTransient<ITaskProgressService, TaskProgressService>();
builder.Services.AddTransient<IProjectTaskService, ProjectTaskService>();
builder.Services.AddTransient<IUsersAccountService, UsersAccountService>();
builder.Services.AddTransient<IUserNonworkingDaysService, UserNonworkingDaysService>();
builder.Services.AddTransient<INonworkingDaysService, NonworkingDaysService>();
builder.Services.AddTransient<ISystemConfigService, SystemConfigService>();
builder.Services.AddTransient<IRagStatusService, RagStatusService>();
builder.Services.AddTransient<IRagTemplatesService, RagTemplatesService>();
builder.Services.AddTransient<IRagFactorService, RagFactorService>();
builder.Services.AddTransient<IInventoryService, InventoryService>();
builder.Services.AddTransient<IAddBrandService, AddBrandService>();
builder.Services.AddTransient<IAddModelService, AddModelService>();
builder.Services.AddTransient<IInventoryPartsService, InventoryPartsService>();
builder.Services.AddTransient<IInventoryPartTypeService, InventoryPartTypeService>();
builder.Services.AddTransient<IInventoryLicenseService, InventoryLicenseService>();
builder.Services.AddTransient<IAddLicenseTypeService, AddLicenseTypeService>();
builder.Services.AddTransient<IFeedBackImprovementService, FeedBackImprovementService>();
builder.Services.AddTransient<IFeedBackItemService, FeedBackItemService>();
builder.Services.AddTransient<IFeedBackPeriodService, FeedBackPeriodService>();
builder.Services.AddTransient<IFeedBackStandarService, FeedBackStandarService>();
builder.Services.AddTransient<IFeedBackStandarTypeService, FeedBackStandarTypeService>();
builder.Services.AddTransient<IFeedBackUserService, FeedBackUserService>();
builder.Services.AddTransient<IFeedBackCommentService, FeedBackCommentService>();
builder.Services.AddTransient<IFeedBackImprovementUserService, FeedBackImprovementUserService>();
builder.Services.AddTransient<ILicenseTypeService, LicenseTypeService>();
builder.Services.AddTransient<IResourceWarningService, ResourceWarningService>();
builder.Services.AddTransient<IResourceWarningHistoryService, ResourceWarningHistoryService>();
builder.Services.AddTransient<IContractService, ContractService>();
builder.Services.AddTransient<IResponsibleService, ResponsibleService>();
builder.Services.AddTransient<IProjectTaskEstimationService, ProjectTaskEstimationService>();
builder.Services.AddTransient<IUserTeamService, UserTeamService>();
builder.Services.AddTransient<ISectionService, SectionService>();
builder.Services.AddTransient<IExtraHoursService, ExtraHoursService>();

builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITeamRepository, TeamRepository>();
builder.Services.AddTransient<IClientRepository, ClientRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IProfileRepository, ProfileRepository>();
builder.Services.AddTransient<ITaskTypeRepository, TaskTypeRepository>();
builder.Services.AddTransient<ITaskProgressRepository, TaskProgressRepository>();
builder.Services.AddTransient<IProjectTaskRepository, ProjectTaskRepository>();
builder.Services.AddTransient<IUsersAccountRepository, UsersAccountRepository>();
builder.Services.AddTransient<IUserNonworkingDaysRepository, UserNonworkingDaysRepository>();
builder.Services.AddTransient<INonworkingDaysRepository, NonworkingDaysRepository>();
builder.Services.AddTransient<ILicenseTypeRepository, LicenseTypeRepository>();
builder.Services.AddTransient<IFeedBackItemRepository, FeedBackItemRepository>();
builder.Services.AddTransient<IFeedBackStandarRepository, FeedBackStandarRepository>();
builder.Services.AddTransient<IFeedBackUserRepository, FeedBackUserRepository>();
builder.Services.AddTransient<IFeedBackStandarTypeRepository, FeedBackStandarTypeRepository>();
builder.Services.AddTransient<IFeedBackImprovementRepository, FeedBackImprovementRepository>();
builder.Services.AddTransient<IFeedBackCommentRepository, FeedBackCommentRepository>();
builder.Services.AddTransient<IFeedBackPeriodRepository, FeedBackPeriodRepository>();
builder.Services.AddTransient<IFeedBackImprovementUserRepository, FeedBackImprovementUserRepository>();
builder.Services.AddTransient<ISystemConfigRepository, SystemConfigRepository>();
builder.Services.AddTransient<IRagStatusRepository, RagStatusRepository>();
builder.Services.AddTransient<IRagTemplatesRepository, RagTemplatesRepository>();
builder.Services.AddTransient<IRagFactorRepository, RagFactorRepository>();
builder.Services.AddTransient<IInventoryRepository, InventoryRepository>();
builder.Services.AddTransient<IAddBrandRepository, AddBrandRepository>();
builder.Services.AddTransient<IAddModelRepository, AddModelRepository>();
builder.Services.AddTransient<IInventoryPartsRepository, InventoryPartsRepository>();
builder.Services.AddTransient<IInventoryPartTypeRepository, InventoryPartTypeRepository>();
builder.Services.AddTransient<IInventoryLicenseRepository, InventoryLicenseRepository>();
builder.Services.AddTransient<IAddLicenseTypeRepository, AddLicenseTypeRepository>();
builder.Services.AddTransient<IResourceWarningRepository, ResourceWarningRepository>();
builder.Services.AddTransient<IResourceWarningHistoryRepository, ResourceWarningHistoryRepository>();
builder.Services.AddTransient<IContractRepository, ContractRepository>();
builder.Services.AddTransient<IResponsibleRepository, ResponsibleRepository>();
builder.Services.AddTransient<IProjectTaskEstimationRepository, ProjectTaskEstimationRepository>();
builder.Services.AddTransient<IUserTeamRepository, UserTeamRepository>();
builder.Services.AddTransient<ISectionRepository, SectionRepository>();
builder.Services.AddTransient<IExtraHoursRepository, ExtraHoursRepository>();


// Configuraciones

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseRequestLocalization("es-AR");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();