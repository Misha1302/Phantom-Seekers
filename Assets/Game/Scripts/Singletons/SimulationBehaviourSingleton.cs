namespace Game.Scripts.Singletons
{
    using Fusion;
    using Game.Scripts.Helpers;
    using UnityEngine;

    public abstract class SimulationBehaviourSingleton<TSelf> : SimulationBehaviour
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
                    $"GameObject with script {nameof(SimulationBehaviourSingleton<TSelf>)} must be the root of hierarchy"
                );

            DontDestroyOnLoad(this);
        }
    }
}