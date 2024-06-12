namespace Game.Scripts.UI
{
    using Game.Scripts.Attributes;
    using Game.Scripts.Bootstrap.Initialization;
    using Game.Scripts.DependenciesManagement.Injector;
    using Game.Scripts.GlobalServices.Creator;
    using Game.Scripts.GlobalServices.Scenes;
    using Game.Scripts.GlobalServices.Scenes.Scenes;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class UiInitializer : InjectableBase
    {
        [Inject] private CreatorService _creatorService;

        [SceneInitializer(typeof(AnyScene), initializationType: InitializationType.Once)]
        public void Initialize()
        {
            _creatorService.Instantiate(Resources.Load<EventSystem>("UI/EventSystem")).gameObject
                .AddComponent<InterSceneTag>();
        }
    }
}