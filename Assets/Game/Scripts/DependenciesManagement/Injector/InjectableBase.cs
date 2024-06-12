namespace Game.Scripts.DependenciesManagement.Injector
{
    using Game.Scripts.Singletons;

    public abstract class InjectableBase
    {
        protected InjectableBase()
        {
            GameSingletons.DependencyInjector.Inject(this);
        }
    }
}