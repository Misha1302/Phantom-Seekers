namespace Game.Scripts.Replay
{
    using System.Collections;
    using System.Collections.Generic;

    public class ReplayEnumerator : IEnumerator<Frame>
    {
        private readonly IReadOnlyList<Frame> _data;
        private int _index = -1;

        public ReplayEnumerator(IReadOnlyList<Frame> data)
        {
            _data = data;
        }

        public Frame Current => _data[_index];

        public bool MoveNext() => ++_index < _data.Count;

        public void Reset() => _index = -1;

        object IEnumerator.Current => Current;

        public void Dispose() { }
    }
}