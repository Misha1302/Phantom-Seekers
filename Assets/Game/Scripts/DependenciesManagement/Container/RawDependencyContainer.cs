namespace Game.Scripts.DependenciesManagement.Container
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    public class RawDependencyContainer
    {
        private readonly Dictionary<ScopeType, Dictionary<Type, Lazy<Any>>> _scopes = new();

        public void AddSingleScoped<TScope, TSingleton>(Func<TSingleton> func)
        {
            var typesInScope = GetScope(new ScopeType(typeof(TScope)));
            typesInScope[typeof(TSingleton)] = new Lazy<Any>(() => new Any(func()));
        }

        [Pure] public bool TryGetSingleOfScopes(ScopeType[] scopeTypes, Type singleType, out object single)
        {
            single = null;

            if (TryGetSingleOfScope(typeof(IAnyScope), singleType, out single))
                return true;

            foreach (var scopeType in scopeTypes)
                if (TryGetSingleOfScope(scopeType, singleType, out single))
                    return true;

            return false;
        }

        private bool TryGetSingleOfScope(ScopeType scopeType, Type type, out object single)
        {
            single = null;

            var scope = GetScope(scopeType);
            if (!scope.TryGetValue(type, out var lazy))
                return false;

            single = lazy.Value.Get();
            return single != null;
        }

        [Pure] private Dictionary<Type, Lazy<Any>> GetScope(ScopeType scopeType)
        {
            _scopes.TryAdd(scopeType, null);
            return _scopes[scopeType] ??= new Dictionary<Type, Lazy<Any>>();
        }

        public void RemoveScope<T>()
        {
            GetScope(typeof(T)).Clear();
        }
    }
}