namespace Game.GameLogic.Scripts
{
    using System.Collections.Generic;
    using Fusion;
    using Game.Scripts.Extensions;
    using UnityEngine;

    public class PlayerSpawnerService : MonoBehaviour
    {
        [SerializeField] private NetworkPrefabRef playerPrefab;
        [SerializeField] private GameObject cameraPrefab;

        private readonly Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new();

        private int _playerIndex;

        public void Spawn(NetworkRunner runner, PlayerRef player)
        {
            var spawnPosition = new Vector3(_playerIndex * 3, 1, 0);
            var networkPlayerObject = runner.Spawn(playerPrefab, spawnPosition, Quaternion.identity, player);
            _spawnedCharacters.Add(player, networkPlayerObject);

            if (_playerIndex == 0)
                SpawnCamera(networkPlayerObject.transform);

            _playerIndex++;
        }

        private void SpawnCamera(Transform parent)
        {
            FindObjectsByType<Camera>(FindObjectsInactive.Include, FindObjectsSortMode.None).ForAll(Destroy);

            var mainCamera = Instantiate(cameraPrefab).transform;

            mainCamera.localPosition = new Vector3(0, 1, 0);
            mainCamera.SetParent(parent);
        }

        public void Despawn(NetworkRunner runner, PlayerRef player)
        {
            if (!_spawnedCharacters.TryGetValue(player, out var networkObject)) return;

            runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
        }
    }
}