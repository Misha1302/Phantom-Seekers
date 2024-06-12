namespace Game.Scripts.Extensions.Math.Numbers
{
    using System;
    using UnityEngine;

    public static class FloatMathExtensions
    {
        private const float DefaultTolerance = 0.0001f;

        public static float Abs(this float value) => Mathf.Abs(value);
        public static float ClampNeg10(this float value) => Mathf.Clamp(value, -1, 0);
        public static float Clamp01(this float value) => Mathf.Clamp(value, 0, 1);
        public static float PingPong(this float value, float repeatValue) => Mathf.PingPong(value, repeatValue);
        public static float Max(this float value, float value2) => Mathf.Max(value, value2);
        public static int Sign(this float value) => Math.Sign(value);

        public static float MoveTowards(this float current, float target, float maxDelta) =>
            Mathf.MoveTowards(current, target, maxDelta);

        public static bool ApproxEq(this float value, float value2, float tolerance = DefaultTolerance) =>
            Math.Abs(value - value2) < tolerance;

        public static bool IsDivisibleBy(this float value, float mod, float tolerance = DefaultTolerance)
        {
            var rem = value % mod.Abs();
            return rem.ApproxEq(mod.Abs(), tolerance) || rem.ApproxEq(0, tolerance);
        }
    }
}