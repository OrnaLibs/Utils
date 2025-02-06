namespace OrnaLibs.ActionScheduler.Builders
{
    public partial class ActionBuilder
    {
        protected internal Action? _action = null!;


        public ActionBuilder Action(Action action)
        {
            _action = action;
            return this;
        }

        protected internal virtual bool CheckDone() => _action is not null;
    }
}
