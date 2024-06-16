namespace Game.Scripts.DependenciesManagement.Container
{
    using System;

    public class DependencyContainer : RawDependencyContainer
    {
        public void AddSingle<TSingleton>(Func<TSingleton> lazy) =>
            AddSingleScoped<IAnyScope, TSingleton>(lazy);

        public void AddSingle<TSingleton>(TSingleton value) =>
            AddSingleScoped<IAnyScope, TSingleton>(value);

        public void AddSingleScoped<TScope, TSingleton>(TSingleton value) =>
            AddSingleScoped<TScope, TSingleton>(() => value);
    }
}