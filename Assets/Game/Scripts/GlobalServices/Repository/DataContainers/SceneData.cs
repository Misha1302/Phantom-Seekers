namespace Game.Scripts.GlobalServices.Repository.DataContainers
{
    using System;
    using Game.Scripts.DependenciesManagement.Container;
    using Game.Scripts.GlobalServices.Repository.DataContainers.Primitives;
    using UnityEngine;

    [Serializable]
    public class SceneData
    {
        public EventSerializableDictionary<SerializableGuid, Any> data = new();
        [SerializeReference] public SceneName name;

        public SceneData(SceneName name)
        {
            this.name = name;
        }
    }
}