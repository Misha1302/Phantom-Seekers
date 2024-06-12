namespace RFL.Scripts.Editor
{
    using Game.Scripts.Attributes;
    using Game.Scripts.Extensions.Math.Numbers;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    ///     <a href="https://forum.unity.com/threads/range-attribute.451848/">
    ///         code taken from here
    ///     </a>
    /// </summary>
    [CustomPropertyDrawer(typeof(ExtendedRangeAttribute))]
    public sealed class ExtendedRangeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = (ExtendedRangeAttribute)attribute;

            if (!IsTypeCorrect(position, property, label))
                return;

            var max = attr.Max - (attr.Max - attr.Min) % attr.Step;
            var value = EditorGUI.IntSlider(position, label, property.intValue, attr.Min, max);

            value = max.Min(value.RoundStep(attr.Step) + attr.Min % attr.Step);
            property.intValue = value;
        }

        private static bool IsTypeCorrect(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Integer)
                return true;

            EditorGUI.LabelField(position, label.text, "Use Extended Range with int.");
            return false;
        }
    }
}