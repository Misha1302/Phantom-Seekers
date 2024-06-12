namespace Game.Scripts.Extensions.Math.Vectors
{
    using UnityEngine;

    public static class Vectors2Extensions
    {
        public static Vector2 WithX(this Vector2 vec, float x)
        {
            vec.x = x;
            return vec;
        }

        public static Vector2 WithY(this Vector2 vec, float y)
        {
            vec.y = y;
            return vec;
        }

        public static bool IsNan(this Vector2 vec) => float.IsNaN(vec.x) || float.IsNaN(vec.y);
        public static Vector2 MakeNan() => new(float.NaN, float.NaN);
    }
}