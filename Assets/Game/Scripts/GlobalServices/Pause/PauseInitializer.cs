namespace Game.Scripts.GlobalServices.Pause
{
    using Game.Scripts.Attributes;
    using Game.Scripts.DependenciesManagement.Injector;
    using Game.Scripts.GlobalServices.Creator;
    using Game.Scripts.GlobalServices.Destroyer;
    using Game.Scripts.GlobalServices.Input.Services;
    using Game.Scripts.GlobalServices.Pause.Canvas;
    using Game.Scripts.GlobalServices.Scenes.Scenes;
    using UnityEngine;
    using Object = UnityEngine.Object;

    public class PauseInitializer : InjectableBase
    {
        [Inject] private PauseService _pauseService;
        [Inject] private IInputService _inputService;
        [Inject] private CreatorService _creatorService;
        [Inject] private DestroyerService _destroyerService;

        [SceneInitializer(typeof(CoreScene))]
        public void Initialize()
        {
            _inputService.OnPause += _pauseService.PauseOrUnPause;

            _pauseService.OnPausedChanged += isPaused =>
            {
                if (isPaused)
                {
                    var settingsCanvas = Resources.Load<SettingsCanvasTag>("UI/SettingsCanvas");
                    _creatorService.Instantiate(settingsCanvas);
                }
                else
                {
                    var canvas = Object.FindAnyObjectByType<SettingsCanvasTag>(FindObjectsInactive.Include);
                    _destroyerService.Destroy(canvas.transform);
                }
            };
        }
    }
}