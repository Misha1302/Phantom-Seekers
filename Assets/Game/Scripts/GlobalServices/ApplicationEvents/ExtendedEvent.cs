namespace Game.Scripts.GlobalServices.ApplicationEvents
{
    using System;

    public class ExtendedEvent
    {
        private Action _actions;

        public void Add(Action action)
        {
            _actions -= action;
            _actions += action;
        }

        public void Sub(Action action)
        {
            _actions -= action;
        }

        public void Invoke()
        {
            _actions?.Invoke();
        }
    }
}