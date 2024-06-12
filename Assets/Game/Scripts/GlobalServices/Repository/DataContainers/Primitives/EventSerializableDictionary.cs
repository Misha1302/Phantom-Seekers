namespace Game.Scripts.GlobalServices.Repository.DataContainers.Primitives
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    [Serializable]
    public class EventSerializableDictionary<TKey, TValue> : IEnumerable<SerializableKeyValuePair<TKey, TValue>>
    {
        public List<SerializableKeyValuePair<TKey, TValue>> pairs = new();

        public Action OnChanged;

        public TValue this[TKey key]
        {
            get => GetPair(key).value;
            set
            {
                OnChanged?.Invoke();
                var pair = GetPair(key);
                if (pair == null)
                    pairs.Add(pair = new SerializableKeyValuePair<TKey, TValue>(key, default));
                pair.value = value;
            }
        }

        public IEnumerator<SerializableKeyValuePair<TKey, TValue>> GetEnumerator() => pairs.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public SerializableKeyValuePair<TKey, TValue> GetPair(TKey key) =>
            pairs.Find(x => x.key.Equals(key));

        public void Clear()
        {
            pairs.Clear();
        }
    }
}