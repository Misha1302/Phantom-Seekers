namespace Game.Scripts.Bootstrap.Initialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Game.Scripts.Attributes;
    using Game.Scripts.Extensions;
    using Game.Scripts.GlobalServices.Repository.DataContainers;
    using Game.Scripts.GlobalServices.Scenes.Scenes;
    using Initializers = System.Linq.IOrderedEnumerable<
        (System.Reflection.MethodInfo method, Attributes.SceneInitializerAttribute attribute)
    >;

    public static class InitializersManager
    {
        private static readonly HashSet<MethodInfo> _initialized = new();

        public static void InitEveryInitializer(SceneName currentSceneName)
        {
            var initializers = GetAllInitializers();
            InvokeInitializers(initializers, currentSceneName);
        }

        private static void InvokeInitializers(Initializers initializers, SceneName currentSceneName)
        {
            initializers.ForAll(x =>
            {
                var attr = x.attribute;

                if (attr.SceneNameType != typeof(AnyScene) && attr.SceneNameType != currentSceneName.GetType())
                    return;
                if (attr.InitializationType == InitializationType.Once && _initialized.Contains(x.method))
                    return;

                _initialized.Add(x.method);

                var instance = Activator.CreateInstance(x.method.DeclaringType!);
                x.method.Invoke(instance, null);
            });
        }

        private static Initializers GetAllInitializers()
        {
            var initializers = Assembly.GetExecutingAssembly()
                .GetTypes()
                .SelectMany(type =>
                    type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                        .Where(m => m.GetCustomAttributes<SceneInitializerAttribute>().Any())
                )
                .Select(x => (method: x, attribute: x.GetCustomAttribute<SceneInitializerAttribute>()))
                .OrderBy(x => x.attribute.Priority);
            return initializers;
        }
    }
}