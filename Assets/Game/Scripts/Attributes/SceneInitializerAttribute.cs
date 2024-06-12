namespace Game.Scripts.Attributes
{
    using System;
    using Game.Scripts.Bootstrap.Initialization;
    using Game.Scripts.GlobalServices.Repository.DataContainers;
    using UnityEngine;

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SceneInitializerAttribute : Attribute
    {
        public readonly int Priority;
        public readonly InitializationType InitializationType;
        public readonly Type SceneNameType;

        public SceneInitializerAttribute(
            Type sceneNameType,
            int priority = 0,
            InitializationType initializationType = InitializationType.Every
        )
        {
            Debug.Assert(sceneNameType.IsSubclassOf(typeof(SceneName)));

            SceneNameType = sceneNameType;
            Priority = priority;
            InitializationType = initializationType;
        }
    }
}