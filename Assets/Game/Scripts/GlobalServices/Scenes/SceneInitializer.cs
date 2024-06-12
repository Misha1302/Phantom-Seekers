namespace Game.Scripts.GlobalServices.Scenes
{
    using Game.Scripts.Attributes;
    using Game.Scripts.Bootstrap.Initialization;
    using Game.Scripts.DependenciesManagement.Injector;
    using Game.Scripts.GlobalServices.Scenes.Scenes;

    public class SceneInitializer : InjectableBase
    {
        [Inject] private SceneService _sceneService;

        [SceneInitializer(typeof(AnyScene), -100, InitializationType.Once)]
        public void Initialize()
        {
            _sceneService.Init();
        }
    }
}