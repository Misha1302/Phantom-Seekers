namespace Game.Scripts.GlobalServices.Scenes
{
    using System.Linq;
    using Game.Scripts.Attributes;
    using Game.Scripts.Bootstrap.Initialization;
    using Game.Scripts.DependenciesManagement.Injector;
    using Game.Scripts.Extensions;
    using Game.Scripts.GlobalServices.Destroyer;
    using Game.Scripts.GlobalServices.Repository.DataContainers;
    using Game.Scripts.GlobalServices.Scenes.Scenes;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using Object = UnityEngine.Object;

    public class SceneService : InjectableBase, IService
    {
        [Inject] private DestroyerService _destroyerService;

        private Transform[] Objects => Object.FindObjectsOfType<Transform>(true);
        public SceneName CurrentScene { get; private set; }

        public void Init()
        {
            DestroyAll(false);
            ChangeScene(ScenesFactory.CoreScene());
        }

        private void DestroyAll(bool saveData)
        {
            Objects.Select(x => x.root).Distinct().ForAll(x =>
            {
                if (!x.HasComponent<IInterScene>() && !x.IsDestroying())
                    _destroyerService.Destroy(x, saveData);
            });
        }

        public void ChangeScene(SceneName name)
        {
            CurrentScene = name;
            DestroyAll(true);
            SceneManager.LoadScene(CurrentScene, LoadSceneMode.Additive);
            InitializersManager.InitEveryInitializer(CurrentScene);
        }
    }
}