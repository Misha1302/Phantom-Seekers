namespace Game.Scripts.Singletons
{
    using Game.Scripts.DependenciesManagement.Container;
    using Game.Scripts.DependenciesManagement.Injector;

    public static class GameSingletons
    {
        public static readonly DependencyInjector DependencyInjector;

        static GameSingletons()
        {
            DependencyInjector = new DependencyInjector(new DependencyContainer());
        }
    }
}