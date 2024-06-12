namespace Game.Scripts.EditorHelpers
{
    using Game.Scripts.GlobalServices.Repository;
    using UnityEngine;

    public class EditorFpsSetter : MonoBehaviour
    {
#if UNITY_EDITOR
        [Range(-1, 60)] [SerializeField] private int targetFps = -1;
        [SerializeField] private bool needToShowFps = true;
        [SerializeField] private bool saveChanges;

        private void OnValidate()
        {
            if (!saveChanges) return;

            var repository = new RepositoryService();
            repository.GameData.targetFps.Value = targetFps;
            repository.GameData.needToShowFps.Value = needToShowFps;

            saveChanges = false;
        }
#endif
    }
}