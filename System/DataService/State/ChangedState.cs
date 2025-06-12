namespace DataService.State
{
    public class ChangedState
    {
        public event Action ChangeAction;

        public void InitChanges()
        {
            ExecuteAction();
        }

        private void ExecuteAction() => ChangeAction?.Invoke();
    }
}
