namespace Game.Scripts.Extensions.Math.Vectors
{
    using UnityEngine;

    public static class Vectors3Extensions
    {
        public static Vector3 WithX(this Vector3 vec, float x)
        {
            vec.x = x;
            return vec;
        }

        public static Vector3 WithY(this Vector3 vec, float y)
        {
            vec.y = y;
            return vec;
        }
    }
}