namespace Game.GameLogic.Scripts.Services
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Game.Scripts.Extensions;
    using Game.Scripts.Replay;
    using Game.Scripts.Singletons;
    using UnityEngine;
    using Object = UnityEngine.Object;

    public static class ServicesInitializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            var container = GameSingletons.DependencyInjector.DependencyContainer;
            container.AddSingle(new InputService());
            container.AddSingle(new SceneService());
            container.AddSingle(() => new GameObject().AddComponent<ReplayService>());
            container.AddSingle(new CursorService());
            container.AddSingle(() => Object.FindAnyObjectByType<PlayerSpawnerService>(FindObjectsInactive.Include));

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