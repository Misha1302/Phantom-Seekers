namespace Game.Scripts.GlobalServices.FpsCounter.Visualizer
{
    using Game.Scripts.Attributes;
    using Game.Scripts.DependenciesManagement.Injector;
    using Game.Scripts.GlobalServices.Creator;
    using Game.Scripts.GlobalServices.Repository;
    using Game.Scripts.GlobalServices.Scenes.Scenes;
    using UnityEngine;

    public class FpsCanvasInitializer : InjectableBase
    {
        [Inject] private RepositoryService _repositoryService;
        [Inject] private CreatorService _creatorService;


        [SceneInitializer(typeof(CoreScene))]
        public void Initialize()
        {
            if (_repositoryService.GameData.needToShowFps.Value)
                CreateFpsCanvas();
        }

        private void CreateFpsCanvas()
        {
            _creatorService.Instantiate(Resources.Load("UI/FpsCanvas"));
        }
    }
}