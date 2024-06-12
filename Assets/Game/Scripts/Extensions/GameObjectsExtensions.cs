namespace Game.Scripts.Extensions
{
    using System.Linq;
    using Game.Scripts.Helpers;
    using UnityEngine;

    public static class GameObjectsExtensions
    {
        public static float CalcHalfOfVisibleYSize(this GameObject gameObject)
        {
            return gameObject
                .GetComponentsInChildren<SpriteRenderer>()
                .Max(r => r.size.y * r.transform.localScale.y) / 2;
        }

        public static GameObject GameObject(this Object uo)
        {
            return uo switch
            {
                GameObject gameObject => gameObject,
                Component component => component.gameObject,
                _ => Thrower.InvalidOpEx("Cannot get gameObject").Get<GameObject>()
            };
        }
    }
}