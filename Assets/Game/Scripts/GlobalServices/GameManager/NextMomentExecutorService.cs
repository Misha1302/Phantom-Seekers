namespace Game.Scripts.GlobalServices.GameManager
{
    using System;
    using Game.Scripts.Helpers;

    public class NextMomentExecutorService : IService
    {
        private readonly Action[] _actions = new Action[CollectionsLength.MaxActionsCount];
        private int _len;

        public void ExecuteInNextMoment(Action action)
        {
            _actions[_len++] = action;
        }

        public void CustomTick()
        {
            for (var i = 0; i < _len; i++)
                _actions[i]();
            _len = 0;
        }
    }
}