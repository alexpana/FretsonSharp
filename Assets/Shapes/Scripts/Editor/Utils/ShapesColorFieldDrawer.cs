using UnityEditor;
using UnityEngine;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    [CustomPropertyDrawer(typeof(ShapesColorFieldAttribute))]
    internal sealed class ShapesColorFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var hdr = ShapesConfig.Instance.useHdrColorPickers;

            var colorUsage = (ShapesColorFieldAttribute)attribute;
            if (property.propertyType == SerializedPropertyType.Color)
            {
                label = EditorGUI.BeginProperty(position, label, property);
                EditorGUI.BeginChangeCheck();
                var newColor = EditorGUI.ColorField(position, label, property.colorValue, true, colorUsage.showAlpha,
                    hdr);
                if (EditorGUI.EndChangeCheck())
                    property.colorValue = newColor;
                EditorGUI.EndProperty();
            }
            else
            {
                EditorGUI.ColorField(position, label, property.colorValue, true, colorUsage.showAlpha, hdr);
            }
        }
    }
}