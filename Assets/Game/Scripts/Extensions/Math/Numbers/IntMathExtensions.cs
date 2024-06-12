namespace Game.Scripts.Extensions.Math.Numbers
{
    using UnityEngine;

    public static class IntMathExtensions
    {
        public static int Min(this int value, int value2) => Mathf.Min(value, value2);
        public static int RoundStep(this int value, int step) => value / step * step;
    }
}