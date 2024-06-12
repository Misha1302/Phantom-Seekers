namespace Game.Scripts.Extensions
{
    using Game.Scripts.GlobalServices.Scenes;
    using JetBrains.Annotations;
    using UnityEngine;

    public static class ComponentsExtensions
    {
        [Pure] public static bool HasComponent<T>(this Component component) =>
            component.TryGetComponent<T>(out _);

        public static T GetOrAddComponent<T>(this Component component) where T : Component
        {
            if (!component.TryGetComponent<T>(out var value))
                value = component.gameObject.AddComponent<T>();
            return value;
        }

        public static void AddComponentIfNotAdded<T>(this Component component) where T : Component =>
            component.GetOrAddComponent<T>();

        [Pure] public static bool IsDestroying(this Component component) =>
            component.HasComponent<DestroyingTag>();
    }
}