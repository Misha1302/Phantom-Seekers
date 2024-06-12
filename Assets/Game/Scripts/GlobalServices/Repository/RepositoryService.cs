namespace Game.Scripts.GlobalServices.Repository
{
    using System;
    using Game.Scripts.Attributes;
    using Game.Scripts.DependenciesManagement.Injector;
    using Game.Scripts.Extensions;
    using Game.Scripts.GlobalServices.ApplicationEvents;
    using Game.Scripts.GlobalServices.GameManager.MonoBeh;
    using UnityEngine;
    using Object = UnityEngine.Object;

    public class RepositoryService : InjectableBase, IService
    {
        private const string Key = "GameDataKey";
        private bool _isSaving;

        private readonly Lazy<GameData> _gameData;

        [Inject] private ApplicationEventsService _applicationEventsService;

        public RepositoryService()
        {
            _gameData = new Lazy<GameData>(LoadGameData);
        }

        public GameData GameData => _gameData.Value;

        private GameData LoadGameData()
        {
            var gameData = LoadRawGameData();
            SubscribeOnDataSave(gameData);
            return gameData;
        }

        private static GameData LoadRawGameData()
        {
            GameData gameData;
            if (SaveSystem.Get(Key, out var value) && !string.IsNullOrWhiteSpace(value))
            {
                gameData = JsonUtility.FromJson<GameData>(value);
                GameDataFabric.SubscribeOnChanged(gameData);
            }
            else
            {
                gameData = GameDataFabric.MakeExampleGameData();
            }

            return gameData;
        }

        public void Reset()
        {
            GameData.targetFps.Value = GameDataFabric.DefaultTargetFps;
            GameData.inputSpeed.Value = GameDataFabric.DefaultInputSpeed;
            GameData.needToShowFps.Value = GameDataFabric.DefaultNeedToShowFps;
            GameData.totalTicks.Value = GameDataFabric.DefaultTotalTicks;
            GameData.playerPos.Value = GameDataFabric.DefaultPlayerPos;
        }

        private void SubscribeOnDataSave(GameData gameData)
        {
            if (Application.isPlaying)
            {
                _applicationEventsService.OnAppUnFocused.Add(SaveGameData);
                _applicationEventsService.OnAppQuitting.Add(SaveGameData);
            }
            else
            {
                gameData.OnChanged += _ => SaveGameData();
            }
        }

        private void SaveGameData()
        {
            if (_isSaving) return;

            _isSaving = true;
            SaveMonoBehs();
            SaveSystem.Set(Key, JsonUtility.ToJson(GameData, true));
            _isSaving = false;
        }

        private static void SaveMonoBehs()
        {
            if (!Application.isPlaying)
                return;

            var objects = Object.FindObjectsByType<MonoBeh>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            objects.ForAll(x => (x as ISavable)?.Save());
        }
    }
}