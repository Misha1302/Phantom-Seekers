namespace Game.GameLogic.Scripts
{
    using Fusion;
    using UnityEngine;

    public struct NetworkInputData : INetworkInput
    {
        public Vector3 Direction;
        public bool Jump;
    }
}