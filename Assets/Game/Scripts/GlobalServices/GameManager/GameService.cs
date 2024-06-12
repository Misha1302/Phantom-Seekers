namespace Game.Scripts.GlobalServices.GameManager
{
    using System.Collections.Generic;
    using Game.Scripts.Attributes;
    using Game.Scripts.Extensions;
    using Game.Scripts.GlobalServices.Pause;
    using Game.Scripts.Singletons;
    using UnityEngine;

    public class GameService : MonoBehaviour, IService
    {
        private readonly List<MonoBeh.MonoBeh> _monoBehs = new();

        private bool _isEnabled = true;

        [Inject] private NextMomentExecutorService _nextMomentExecutorService;
        [Inject] private PauseService _pauseService;


        private void Awake()
        {
            GameSingletons.DependencyInjector.Inject(this);
            _pauseService.OnPausedChanged += OnPausedChanged;
        }

        private void Update()
        {
            _nextMomentExecutorService.CustomTick();
            if (!_isEnabled) return;
            _monoBehs.ForAll(x => x.PubTick());
        }

        private void FixedUpdate()
        {
            _nextMomentExecutorService.CustomTick();
            if (!_isEnabled) return;
            _monoBehs.ForAll(x => x.PubFixedTick());
        }

        private void LateUpdate()
        {
            if (!_isEnabled) return;
            _monoBehs.ForAll(x => x.PubLateTick());
        }


        private void OnPausedChanged(bool isPaused) => _isEnabled = !isPaused;


        public void AddMonoBeh(MonoBeh.MonoBeh monoBeh)
        {
            _monoBehs.Add(monoBeh);
            _nextMomentExecutorService.ExecuteInNextMoment(monoBeh.PubOnStart);
        }

        public void RemoveMonoBeh(MonoBeh.MonoBeh monoBeh)
        {
            _monoBehs.Remove(monoBeh);
        }
    }
}