namespace OrnaLibs.ActionScheduler.ActionTypes
{
    public class DailyAction : IExpectedAction
    {
        Action IExpectedAction.Action => _action;
        DateTime IExpectedAction.LastRun { get => _last; set => _last = value; }
        private DateTime _dt => DateTime.Today.Add(_time.ToTimeSpan());

        private readonly Action _action;
        private readonly TimeOnly _time;
        private DateTime _last;

        internal DailyAction(Action action, TimeOnly time)
        {
            _action = action;
            _last = DateTime.MinValue;
            _time = time; 
        }

        void IExpectedAction.TryExecute()
        {
            if (_last >= _dt || DateTime.Now < _dt) return;
            _last = DateTime.Now;
            _action.Invoke();
        }
    }
}
