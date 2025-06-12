using DataContext;
using DataContext.Extension;
using DataContext.Interfaces.Management;
using DataContext.Interfaces.ProjectManagement;
using DataModel.Dto;
using DataModel.Enums;
using DataService.State;
using Microsoft.VisualStudio.Services.Common;
using System.Linq;

namespace DataService.Auth
{
    public class AuthService
    {
        private readonly IUserRepository _repository;
        private readonly ITaskProgressRepository _taskProgressRepository;
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly LoginState _loginState;

        public AuthService(IUserRepository repository, ITaskProgressRepository taskProgressRepository, IProjectTaskRepository projectTaskRepository, LoginState loginstate)
        {
            _repository = repository;
            _loginState = loginstate;
            _taskProgressRepository = taskProgressRepository;
            _projectTaskRepository = projectTaskRepository;
        }

        public bool IsLoggedIn { get; private set; }
        public User User { get; private set; } = new User();
        public List<HoursPerDayModel> UserCalendar { get; private set; } = new List<HoursPerDayModel>();
        public string IncompleteHoursMessage { get; private set; } = "";
        public string LicenseChangeMessage { get; private set; } = "";

        public async Task<LoginType> Login(string username, string password)
        {
            if (username == null || password == null) return LoginType.RequestError;

            var user = await _repository.GetByUsernameAsync(username);

            if (user == null) return LoginType.UsuarioIncorrecto;

            password = PasswordHelper.HashPassword(password);

            if (password == user.Password)
            {
                IsLoggedIn = true;
                User = user;

                await UpdateHours(user.Id);

                _loginState.InitLogin();

                return LoginType.Correcto;
            }

            return LoginType.PasswordIncorrecto;
        }

        public async Task UpdateHours(long userId)
        {
            UserCalendar = await _projectTaskRepository.UserCalendar(userId, true);
            IncompleteHoursMessage = CheckHours(UserCalendar);
        }

        public string CheckHours(List<HoursPerDayModel> hoursPerDayList)
        {
            var firstNonZeroIndex = hoursPerDayList.OrderBy(x => x.Date).ToList().FindIndex(h => h.Hours > 0);

            if (firstNonZeroIndex == -1)
            {
                return "Verifique si tiene horas sin cargar";
            }

            var range = hoursPerDayList.OrderBy(x => x.Date).ToList().GetRange(firstNonZeroIndex, hoursPerDayList.Count - firstNonZeroIndex);

            var hasZeroAfter = range.Where(x => x.Hours < 8).ToList().Count > 0;

            if (hasZeroAfter)
            {
                return "Tiene horas sin cargar";
            }
            else
            {
                return "";
            }
        }

        public void Logout()
        {
            IsLoggedIn = false;
            User = new User();
            IncompleteHoursMessage = "";
            _loginState.InitLogin();
        }

    }
}
