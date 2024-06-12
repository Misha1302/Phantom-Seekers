namespace Game.Scripts.EditorHelpers
{
    using Game.Scripts.Attributes;
    using Game.Scripts.GlobalServices.GameManager.MonoBeh;
    using Game.Scripts.GlobalServices.Pause;
    using UnityEngine;

    public class EditorPauseSetter : MonoBeh
    {
#if UNITY_EDITOR
        [SerializeField] private bool changeToPause;
        [SerializeField] private bool changeToUnPause;

        [Inject] private PauseService _pauseService;

        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                if (changeToPause) _pauseService.IsPaused = true;
                if (changeToUnPause) _pauseService.IsPaused = false;
            }

            changeToPause = changeToUnPause = false;
        }
#endif
    }
}