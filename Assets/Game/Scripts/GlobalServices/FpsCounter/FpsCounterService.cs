namespace Game.Scripts.GlobalServices.FpsCounter
{
    using System;
    using Game.Scripts.Extensions.Math.Numbers;
    using Game.Scripts.GlobalServices.GameManager.MonoBeh;

    public class FpsCounterService : MonoBeh, IService
    {
        private const float UpdateTime = 1f;
        private int _fpsCount;

        public Action<float> OnFpsChanged = null;
        public float CurrentFps { get; private set; }

        protected override void Tick()
        {
            _fpsCount++;
        }

        protected override void FixedTick()
        {
            SetCurrentFpsIfNeed();
        }

        private void SetCurrentFpsIfNeed()
        {
            if (!TimeSinceStart.IsDivisibleBy(UpdateTime)) return;

            CurrentFps = _fpsCount / UpdateTime;
            OnFpsChanged?.Invoke(CurrentFps);
            _fpsCount = 0;
        }
    }
}