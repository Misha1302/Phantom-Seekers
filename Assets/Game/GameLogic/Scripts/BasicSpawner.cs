namespace Game.GameLogic.Scripts
{
    using System;
    using System.Collections.Generic;
    using Fusion;
    using Fusion.Sockets;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class BasicSpawner : MonoBehaviour, INetworkRunnerCallbacks
    {
        [SerializeField] private NetworkPrefabRef playerPrefab;
        private readonly Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new();
        private NetworkRunner _runner;

        private void OnGUI()
        {
            if (_runner != null) return;

            if (GUI.Button(new Rect(0, 0, 500, 100), "Host"))
                StartGame(GameMode.Host);
            if (GUI.Button(new Rect(0, 100, 500, 100), "Join"))
                StartGame(GameMode.Client);
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (!runner.IsServer) return;

            // Create a unique position for the player
            var spawnPosition = new Vector3(player.RawEncoded % runner.Config.Simulation.PlayerCount * 3, 1, 0);
            var networkPlayerObject = runner.Spawn(playerPrefab, spawnPosition, Quaternion.identity, player);
            // Keep track of the player avatars for easy access
            _spawnedCharacters.Add(player, networkPlayerObject);
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            if (!_spawnedCharacters.TryGetValue(player, out var networkObject)) return;

            runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            var data = new NetworkInputData
            {
                Direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")),
                Jump = Input.GetKey(KeyCode.Space)
            };
            input.Set(data);
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


        private async void StartGame(GameMode mode)
        {
            // Create the Fusion runner and let it know that we will be providing user input
            _runner = gameObject.AddComponent<NetworkRunner>();
            _runner.ProvideInput = true;

            // Create the NetworkSceneInfo from the current scene
            var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
            var sceneInfo = new NetworkSceneInfo();
            if (scene.IsValid) sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);

            // Start or join (depends on gamemode) a session with a specific name
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