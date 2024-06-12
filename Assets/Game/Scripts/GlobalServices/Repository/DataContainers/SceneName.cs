namespace Game.Scripts.GlobalServices.Repository.DataContainers
{
    using System;
    using UnityEngine;

    [Serializable]
    public abstract class SceneName
    {
        /// <summary>
        ///     Use Name instead
        /// </summary>
        [SerializeField] private string name;

        protected SceneName(string name)
        {
            this.name = name;
        }

        public string Name => name;

        public static implicit operator string(SceneName sceneName) => sceneName.Name;

        public override bool Equals(object obj) => obj is SceneName sceneName && Name == sceneName.Name;

        public override int GetHashCode() => Name != null ? Name.GetHashCode() : 0;

        public static bool operator ==(SceneName a, SceneName b) => Equals(a, b);

        public static bool operator !=(SceneName a, SceneName b) => !(a == b);
    }
}