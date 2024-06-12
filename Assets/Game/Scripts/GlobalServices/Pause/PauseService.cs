namespace Game.Scripts.GlobalServices.Pause
{
    using System;

    public class PauseService : IService
    {
        private bool _isPaused;
        public Action<bool> OnPausedChanged;

        public bool IsPaused
        {
            get => _isPaused;
            set
            {
                _isPaused = value;
                OnPausedChanged?.Invoke(_isPaused);
            }
        }

        public void PauseOrUnPause()
        {
            IsPaused = !IsPaused;
        }
    }
}