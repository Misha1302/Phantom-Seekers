namespace Game.Scripts.DependenciesManagement.Container
{
    using System;
    using UnityEngine;

    [Serializable]
    public class Any
    {
        [SerializeReference] private object obj;

        public Any(object obj)
        {
            this.obj = obj;
        }

        public T Get<T>() => (T)obj;

        public bool Is<T>() => obj is T;

        public object Get() => obj;
    }
}