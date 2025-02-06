using OrnaLibs.ActionScheduler.ActionTypes;

namespace OrnaLibs.ActionScheduler.Builders
{
    public sealed class DailyBuilder: ActionBuilder
    {
        private TimeOnly? _time = null;

        protected internal override bool CheckDone() => base.CheckDone() && _time is not null;

        public DailyBuilder Time(TimeOnly time)
        {
            _time = time;
            return this;
        }

        public DailyAction Build()
        {
            if (!CheckDone()) throw new Exception();
            return new DailyAction(_action!, _time!.Value);
        }
    }

    public partial class ActionBuilder
    {
        public DailyBuilder Daily => (DailyBuilder)this;
    }
}
