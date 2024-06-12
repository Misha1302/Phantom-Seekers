namespace Game.Scripts.GlobalServices.Repository
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Game.Scripts.Extensions;
    using Game.Scripts.GlobalServices.Repository.DataContainers;
    using UnityEngine;

    public static class GameDataFabric
    {
        public const int DefaultTargetFps = -1;
        public const float DefaultInputSpeed = 1f / 0.4f;
        public const bool DefaultNeedToShowFps = true;
        public const long DefaultTotalTicks = 0;
        public static readonly Vector3 DefaultPlayerPos = Vector3.zero;

        public static GameData MakeExampleGameData()
        {
            var gd = MakeStandardGameData();
            AddScenes(gd);
            SubscribeOnChanged(gd);
            return gd;
        }

        private static void AddScenes(GameData gd)
        {
            Assembly.GetExecutingAssembly().GetTypes().Where(x => x.BaseType == typeof(SceneName)).ForAll(x =>
                gd.sceneDatas.Add(new SceneData((SceneName)Activator.CreateInstance(x)))
            );
        }

        public static void SubscribeOnChanged(GameData gd)
        {
            gd.targetFps.OnChanged += () => gd.OnChanged?.Invoke(gd);
            gd.inputSpeed.OnChanged += () => gd.OnChanged?.Invoke(gd);
            gd.needToShowFps.OnChanged += () => gd.OnChanged?.Invoke(gd);
            gd.scenesList.OnChanged += () => gd.OnChanged?.Invoke(gd);
            gd.totalTicks.OnChanged += () => gd.OnChanged?.Invoke(gd);
            gd.playerPos.OnChanged += () => gd.OnChanged?.Invoke(gd);
            gd.sceneDatas.OnChanged += _ => gd.OnChanged?.Invoke(gd);

            gd.sceneDatas.ForAll(x => x.data.OnChanged += () => gd.OnChanged?.Invoke(gd));
            gd.scenesList.Value.OnChanged += _ => gd.OnChanged?.Invoke(gd);
        }

        public static GameData MakeStandardGameData()
        {
            var gd = new GameData
            {
                targetFps = { Value = -1 },
                inputSpeed = { Value = 1f / 0.4f },
                needToShowFps = { Value = true }
            };

            return gd;
        }
    }
}