namespace Game.Scripts.Singletons
{
    using Game.Scripts.Helpers;
    using UnityEngine;

    public abstract class MonoBehaviourSingleton<TSelf> : MonoBehaviour
    {
        public static TSelf Instance;

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = (TSelf)(object)this;
            if (transform.root != transform)
                Thrower.InvalidOpEx(
                    $"GameObject with script {nameof(MonoBehaviourSingleton<TSelf>)} must be the root of hierarchy"
                );

            DontDestroyOnLoad(this);
        }
    }
}