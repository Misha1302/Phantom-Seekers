namespace Game.Scripts.DependenciesManagement.Container
{
    public class DependencyContainer : RawDependencyContainer
    {
        public void AddSingle<TSingleton>(TSingleton value) =>
            AddSingleScoped<IAnyScope, TSingleton>(value);

        public void AddSingleScoped<TScope, TSingleton>(TSingleton value) =>
            AddSingleScoped<TScope, TSingleton>(() => value);
    }
}