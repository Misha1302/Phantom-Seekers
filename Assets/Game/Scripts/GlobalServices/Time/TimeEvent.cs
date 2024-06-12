namespace Game.Scripts.GlobalServices.Time
{
    using System;
    using Game.Scripts.GlobalServices.GameManager.MonoBeh;

    public class TimeEvent : MonoBeh
    {
        private bool _called;
        private double _targetTime;

        public Action OnTimeCome;

        protected override void FixedTick()
        {
            if (TotalTime < _targetTime || _called) return;

            OnTimeCome?.Invoke();
            _called = true;
            isEnabled = false;
        }

        public TimeEvent Init(double targetTime)
        {
            _targetTime = targetTime;
            return this;
        }
    }
}