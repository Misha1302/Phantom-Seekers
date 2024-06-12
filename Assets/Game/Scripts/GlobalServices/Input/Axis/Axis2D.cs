namespace Game.Scripts.GlobalServices.Input.Axis
{
    using UnityEngine;

    public class Axis2D
    {
        public readonly Axis X;
        public readonly Axis Y;

        public Axis2D(float speed)
        {
            X = new Axis(speed);
            Y = new Axis(speed);
        }

        public static implicit operator Vector2(Axis2D vec) => new(vec.X.Value, vec.Y.Value);
        public static implicit operator Vector3(Axis2D vec) => new(vec.X.Value, vec.Y.Value);
    }
}