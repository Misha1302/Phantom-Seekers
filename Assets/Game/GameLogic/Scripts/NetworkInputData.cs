namespace Game.GameLogic.Scripts
{
    using Fusion;
    using UnityEngine;

    public struct NetworkInputData : INetworkInput
    {
        public Vector3 MovementDirection;
        public Vector2 RotationDirection;

        public override string ToString() =>
            $"MovementDirection: {MovementDirection}; RotationDirection: {RotationDirection}";
    }
}