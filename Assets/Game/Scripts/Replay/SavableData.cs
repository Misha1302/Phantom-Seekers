namespace Game.Scripts.Replay
{
    using UnityEngine;

    public struct SavableData
    {
        public Vector3 Pos;
        public Vector3 Rot;
        public Vector3 Scale;

        public SavableData(Vector3 pos, Vector3 rot, Vector3 scale)
        {
            Pos = pos;
            Rot = rot;
            Scale = scale;
        }
    }
}