namespace Game.Scripts.GlobalServices.Coroutines
{
    using System;
    using System.Collections.Generic;
    using Game.Scripts.Attributes;
    using Game.Scripts.DependenciesManagement.Injector;
    using Game.Scripts.GlobalServices.Coroutines.Waitings;
    using Game.Scripts.GlobalServices.Time;
    using JetBrains.Annotations;

    public class CoroutineObj : InjectableBase
    {
        [Inject] private TimeService _timeService;

        private readonly IEnumerator<ICoroutineWaiting> _enumerator;
        [CanBeNull] private readonly Action _whenCoroutineEnd;

        private bool _finished;

        public Action OnEnd;

        public CoroutineObj(IEnumerator<ICoroutineWaiting> enumerator, [CanBeNull] Action whenCoroutineEnd)
        {
            _enumerator = enumerator;
            _whenCoroutineEnd = whenCoroutineEnd;

            OnEnd += OnEndHandler;
        }

        private bool Finished
        {
            set
            {
                _finished = value;
                if (_finished) OnEnd?.Invoke();
            }
        }


        private void OnEndHandler()
        {
            _whenCoroutineEnd?.Invoke();
            _enumerator.Dispose();
        }

        public void Tick()
        {
            if (_enumerator.Current is WaitNextFrame) Next();

            if (_enumerator.Current is WaitSeconds s)
                if (_timeService.ElapsedTime >= s.EndTime)
                    Next();
        }

        public void FixedTick()
        {
            if (_enumerator.Current is WaitNextFixed) Next();
        }

        private void Next()
        {
            Finished = _enumerator.MoveNext();
        }

        public void Stop()
        {
            Finished = true;
        }
    }
}