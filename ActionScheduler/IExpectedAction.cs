namespace OrnaLibs.ActionScheduler
{
    public interface IExpectedAction
    {
        internal Action Action { get; }
        protected DateTime LastRun { get; set; }

        internal void TryExecute();
    }
}
