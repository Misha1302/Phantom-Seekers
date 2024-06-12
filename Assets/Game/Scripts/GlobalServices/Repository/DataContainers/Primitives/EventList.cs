namespace Game.Scripts.GlobalServices.Repository.DataContainers.Primitives
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class EventList<T> : IEnumerable<T>
    {
        [SerializeField] private List<T> list = new();

        public Action<EventList<T>> OnChanged = null!;

        public IEnumerator<T> GetEnumerator() => list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void AddRange(IEnumerable<T> range)
        {
            list.AddRange(range);
            OnChanged?.Invoke(this);
        }

        public void Clear()
        {
            list.Clear();
            OnChanged?.Invoke(this);
        }

        public void Add(T value)
        {
            list.Add(value);
        }
    }
}