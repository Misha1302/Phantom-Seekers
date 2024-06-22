namespace Game.Scripts.Replay
{
    using System.Collections.Generic;
    using UnityEngine;

    public readonly struct Frame
    {
        private readonly List<(Transform, SavableData)> _objs;

        public Frame(int length)
        {
            _objs = new List<(Transform, SavableData)>(length);
        }

        public IReadOnlyList<(Transform, SavableData)> Objs => _objs;

        public void Add(Transform transform, SavableData savableData)
        {
            _objs.Add((transform, savableData));
        }
    }
}