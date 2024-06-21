namespace Game.GameLogic.Scripts
{
    using System;
    using Fusion;
    using Game.GameLogic.Scripts.Services;
    using Game.Scripts.Singletons;
    using UnityEngine.SceneManagement;

    public class BasicSpawner : SimulationBehaviourSingleton<BasicSpawner>, IPlayerJoined, IPlayerLeft
    {
        private readonly InjectField<PlayerSpawnerService> _playerSpawnerService = new();
        private readonly InjectField<SceneService> _sceneService = new();


        [NonSerialized] private NetworkRunner _runner;

        public void PlayerJoined(PlayerRef player)
        {
            if (player == _runner.LocalPlayer)
                _playerSpawnerService.Value.Spawn(_runner, player);
        }

        public void PlayerLeft(PlayerRef player)
        {
            _playerSpawnerService.Value.Despawn(_runner, player);
        }


        public async void StartGame(GameMode mode)
        {
            await _sceneService.Value.LoadScene("Core");

            _runner = gameObject.AddComponent<NetworkRunner>();

            var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
            var sceneInfo = new NetworkSceneInfo();
            if (scene.IsValid) sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);

            // Start or join (depends on game-mode) a session with a specific name
            await _runner.StartGame(new StartGameArgs
            {
                GameMode = mode,
                SessionName = "TestRoom",
                Scene = scene,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
            });
        }
    }
}