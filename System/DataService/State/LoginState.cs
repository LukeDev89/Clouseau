namespace DataService.State
{
    public class LoginState
    {
        public event Action LoginAction;

        public void InitLogin()
        {
            ExecuteAction();
        }

        private void ExecuteAction() => LoginAction?.Invoke();
    }
}