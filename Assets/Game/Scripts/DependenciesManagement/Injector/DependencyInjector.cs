namespace Game.Scripts.DependenciesManagement.Injector
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Game.Scripts.Attributes;
    using Game.Scripts.DependenciesManagement.Container;
    using Game.Scripts.Extensions;
    using Game.Scripts.Helpers;

    public class DependencyInjector
    {
        public readonly DependencyContainer DependencyContainer;

        public DependencyInjector(DependencyContainer dependencyContainer)
        {
            DependencyContainer = dependencyContainer;
        }

        public void Inject(object injectable)
        {
            InjectViaFields(injectable);
            InjectViaMethod(injectable);
        }

        private void InjectViaFields(object component)
        {
            var fieldInfos = GetTypesRecursivelyToParent(component).SelectMany(t =>
                    t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                        .Where(x => x.GetCustomAttributes(typeof(InjectAttribute), false).Any())
                )
                .ToArray();

            if (!fieldInfos.Any())
                return;

            var scopeTypes = GetScopeTypes(component);
            fieldInfos.ForAll(x =>
                x.SetValue(component, GetFromContainer(scopeTypes, x.FieldType))
            );
        }

        private List<Type> GetTypesRecursivelyToParent(object component)
        {
            var types = new List<Type> { component.GetType() };
            GetTypesRecursivelyToParentInternal(component.GetType());
            return types;

            void GetTypesRecursivelyToParentInternal(Type type)
            {
                var parent = type.BaseType;
                if (parent == null) return;

                types.Add(parent);
                GetTypesRecursivelyToParentInternal(type.BaseType);
            }
        }

        private void InjectViaMethod(object component)
        {
            var initMethod = component.GetType().GetMethods()
                .FirstOrDefault(x => x.GetCustomAttributes(typeof(InjectAttribute), false).Any());

            if (initMethod == null)
                return;

            var scopeTypes = GetScopeTypes(component);
            var parameters = initMethod.GetParameters()
                .Select(x => GetFromContainer(scopeTypes, x.ParameterType))
                .ToList();

            initMethod.Invoke(component, parameters.ToArray());
        }

        private ScopeType[] GetScopeTypes(object component)
        {
            return component.GetType().GetInterfaces().Select(t => new ScopeType(t)).ToArray();
        }

        private object GetFromContainer(ScopeType[] scopeTypes, Type type)
        {
            if (!DependencyContainer.TryGetSingleOfScopes(scopeTypes, type, out var single))
                Thrower.InvalidOpEx($"Cannot find {type.GetFriendlyTypeName()} in container");
            return single;
        }
    }
}