namespace Game.Scripts.Helpers
{
    public static class Random
    {
        public static bool Bool(float chance) =>
            UnityEngine.Random.Range(0f, 1f) * 100f < chance;
    }
}