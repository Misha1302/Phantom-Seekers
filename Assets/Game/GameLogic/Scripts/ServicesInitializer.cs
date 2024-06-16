namespace Game.GameLogic.Scripts
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Game.Scripts.Extensions;
    using Game.Scripts.Singletons;
    using UnityEngine;
    using Object = UnityEngine.Object;

    public static class ServicesInitializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            GameSingletons.DependencyInjector.DependencyContainer.AddSingle(new InputService());
            GameSingletons.DependencyInjector.DependencyContainer.AddSingle(new SceneService());
            GameSingletons.DependencyInjector.DependencyContainer.AddSingle(new CursorService());
            GameSingletons.DependencyInjector.DependencyContainer.AddSingle(
                Object.FindAnyObjectByType<PlayerSpawnerService>
            );

            InitAllInitializers();
        }

        private static void InitAllInitializers()
        {
            Assembly.GetExecutingAssembly().GetTypes()
                .SelectMany(t =>
                    t.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                        .Where(m => m.GetCustomAttributes<InitAttribute>().Any())
                ).ForAll(m => m.Invoke(Activator.CreateInstance(m.DeclaringType!), null));
        }
    }
}