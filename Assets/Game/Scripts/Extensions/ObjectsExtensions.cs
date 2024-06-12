namespace Game.Scripts.Extensions
{
    using System;

    public static class ObjectsExtensions
    {
        public static T ThisOrIf<T>(this T value, Predicate<T> condition, T ifValue) =>
            condition(value) ? ifValue : value;
    }
}