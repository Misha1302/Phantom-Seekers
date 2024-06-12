namespace Game.Scripts.Attributes
{
    using System;
    using UnityEngine;

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ExtendedRangeAttribute : PropertyAttribute
    {
        public readonly int Min;
        public readonly int Max;
        public readonly int Step;

        public ExtendedRangeAttribute(int min, int max, int step)
        {
            Min = min;
            Max = max;
            Step = step;
        }
    }
}