namespace Game.Scripts.GlobalServices.ApplicationEvents
{
    using Game.Scripts.Extensions;
    using Game.Scripts.GlobalServices.GameManager.MonoBeh;
    using UnityEngine;

    public class ApplicationEventsService : MonoBehaviour, IService
    {
        public readonly ExtendedEvent OnAppQuitting = new();
        public readonly ExtendedEvent OnAppUnFocused = new();

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus) return;
            OnAppUnFocused?.Invoke();
        }

        private void OnApplicationQuit()
        {
            OnAppQuitting?.Invoke();
            DisableAll();
        }

        private void DisableAll()
        {
            FindObjectsByType<MonoBeh>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                .ForAll(x => x.isEnabled = false);
        }
    }
}