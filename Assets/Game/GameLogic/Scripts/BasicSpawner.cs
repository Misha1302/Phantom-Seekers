namespace Game.GameLogic.Scripts
{
    using System;
    using System.Collections.Generic;
    using Fusion;
    using Fusion.Sockets;
    using Game.Scripts.Singletons;
    using UnityEngine.SceneManagement;

    public class BasicSpawner : MonoBehaviourSingleton<BasicSpawner>, INetworkRunnerCallbacks
    {
        private readonly InjectField<InputService> _inputService = new();
        private readonly InjectField<PlayerSpawnerService> _playerSpawnerService = new();


        private NetworkRunner _runner;
        private readonly InjectField<SceneService> _sceneService = new();

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (!runner.IsServer) return;
            _playerSpawnerService.Value.Spawn(runner, player);
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            _playerSpawnerService.Value.Despawn(runner, player);
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            input.Set(_inputService.Value.GetData());
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
        void INetworkRunnerCallbacks.OnConnectedToServer(NetworkRunner runner) { }
        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }

        public void OnConnectRequest(NetworkRunner runner,
            NetworkRunnerCallbackArgs.ConnectRequest request,
            byte[] token) { }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
        public void OnSceneLoadDone(NetworkRunner runner) { }
        public void OnSceneLoadStart(NetworkRunner runner) { }
        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

        public void OnReliableDataReceived(NetworkRunner runner,
            PlayerRef player,
            ReliableKey key,
            ArraySegment<byte> data) { }

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }


        public async void StartGame(GameMode mode)
        {
            await _sceneService.Value.LoadScene("Core");

            // Create the Fusion runner and let it know that we will be providing user input
            _runner = gameObject.AddComponent<NetworkRunner>();
            _runner.ProvideInput = true;

            // Create the NetworkSceneInfo from the current scene
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