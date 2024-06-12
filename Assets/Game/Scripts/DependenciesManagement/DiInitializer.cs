namespace Game.Scripts.DependenciesManagement
{
    using Game.Scripts.Attributes;
    using Game.Scripts.Bootstrap.Initialization;
    using Game.Scripts.GlobalServices.ApplicationEvents;
    using Game.Scripts.GlobalServices.Coroutines;
    using Game.Scripts.GlobalServices.Creator;
    using Game.Scripts.GlobalServices.Destroyer;
    using Game.Scripts.GlobalServices.FpsCounter;
    using Game.Scripts.GlobalServices.FpsSetter;
    using Game.Scripts.GlobalServices.GameManager;
    using Game.Scripts.GlobalServices.Input;
    using Game.Scripts.GlobalServices.Pause;
    using Game.Scripts.GlobalServices.Repository;
    using Game.Scripts.GlobalServices.Scenes;
    using Game.Scripts.GlobalServices.Scenes.Scenes;
    using Game.Scripts.GlobalServices.Time;
    using Game.Scripts.Singletons;

    public class DiInitializer
    {
        [SceneInitializer(typeof(AnyScene), -1000, InitializationType.Once)]
        public void Initialize()
        {
            var creator = new CreatorService();
            var container = GameSingletons.DependencyInjector.DependencyContainer;

            container.AddSingle(creator);
            container.AddSingle(new DestroyerService());
            container.AddSingle(creator.Create<ApplicationEventsService>());
            container.AddSingle(new PauseService());
            container.AddSingle(new NextMomentExecutorService());
            container.AddSingle(new RepositoryService());
            container.AddSingle(new TimeService());
            container.AddSingle(creator.Create<GameService>());
            container.AddSingle(creator.Create<TimeManagerService>());

            container.AddSingle(new SceneService());
            container.AddSingle(InputMaker.MakeInputService(creator));
            container.AddSingle(creator.Create<CoroutinesService>());
            container.AddSingle(creator.Create<FpsCounterService>());
            container.AddSingle(new FpsSetterService());
        }
    }
}