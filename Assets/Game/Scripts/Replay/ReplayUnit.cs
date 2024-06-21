namespace Game.Scripts.Replay
{
    using System.Collections;
    using System.Collections.Generic;

    public class ReplayUnit : IEnumerable<Frame>
    {
        private readonly List<Frame> _data;
        public IReadOnlyList<Frame> Frames => _data;

        public ReplayUnit(List<Frame> savableData)
        {
            _data = savableData;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<Frame> GetEnumerator() => new ReplayEnumerator(_data);
    }
}