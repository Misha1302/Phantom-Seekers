namespace Game.Scripts.EditorHelpers
{
    using UnityEngine;

    public class EditorTimeSetter : MonoBehaviour
    {
#if UNITY_EDITOR
        [Range(0.002f, 2f)] [SerializeField] private float scale = 1;

        private void OnValidate()
        {
            Time.timeScale = scale;
        }
#endif
    }
}