namespace Game.Scripts.GlobalServices.Repository.DataContainers.Primitives
{
    using System;
    using UnityEngine;

    [Serializable]
    public class EventField<T> where T : new()
    {
        [SerializeField] private T value = new();

        public Action OnChanged = null!;

        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                OnChanged?.Invoke();
            }
        }
    }
}