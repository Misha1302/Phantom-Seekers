namespace Game.Scripts.GlobalServices.Time
{
    using Game.Scripts.Attributes;
    using Game.Scripts.DependenciesManagement.Injector;
    using Game.Scripts.GlobalServices.Pause;
    using Game.Scripts.GlobalServices.Repository;
    using UnityEngine;

    public class TimeService : InjectableBase, ISavable
    {
        private long _elapsedTicks;
        [Inject] private PauseService _pauseService;
        [Inject] private RepositoryService _repositoryService;
        private long _startTotalTicks;

        public long TotalTicks => _startTotalTicks + _elapsedTicks;
        public double TotalTime => CalcTime(TotalTicks);
        public float ElapsedTime => (float)CalcTime(_elapsedTicks);
        public float DeltaTime => Time.deltaTime;
        public float FixedDeltaTime => Time.fixedDeltaTime;

        public void Save()
        {
            SaveTime();
        }

        public double CalcTime(long ticks) => ticks * FixedDeltaTime;


        public void OnStart()
        {
            SetTicks();
            _repositoryService.GameData.totalTicks.OnChanged += SetTicks;
        }

        private void SetTicks()
        {
            _startTotalTicks = _repositoryService.GameData.totalTicks.Value;
        }

        private void SaveTime()
        {
            _repositoryService.GameData.totalTicks.Value = TotalTicks;
        }

        public void FixedTick()
        {
            if (!_pauseService.IsPaused)
                _elapsedTicks++;
        }
    }
}