namespace Game.Scripts
{
    using Game.Scripts.Bootstrap;
    using UnityEngine;

    public static class EntryPoint
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoadRuntimeMethod()
        {
            Bootstrapper.Bootstrap();
        }
    }
}