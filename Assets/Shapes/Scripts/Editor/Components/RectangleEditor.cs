using System.Linq;
using UnityEditor;
using UnityEngine;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    [CustomEditor(typeof(Rectangle))]
    [CanEditMultipleObjects]
    public class RectangleEditor : ShapeRendererEditor
    {
        private DashStyleEditor dashEditor;
        private SceneFillEditor fillEditor;
        private readonly SerializedProperty propCornerRadii = null;
        private readonly SerializedProperty propCornerRadiusMode = null;
        private readonly SerializedProperty propDashed = null;
        private readonly SerializedProperty propDashStyle = null;
        private readonly SerializedProperty propFill = null;
        private readonly SerializedProperty propHeight = null;
        private readonly SerializedProperty propMatchDashSpacingToSize = null;
        private readonly SerializedProperty propPivot = null;
        private readonly SerializedProperty propThickness = null;
        private readonly SerializedProperty propThicknessSpace = null;

        private readonly SerializedProperty propType = null;
        private readonly SerializedProperty propUseFill = null;
        private readonly SerializedProperty propWidth = null;
        private SceneRectEditor rectEditor;

        public override void OnEnable()
        {
            base.OnEnable();
            dashEditor = DashStyleEditor.GetDashEditor(propDashStyle, propMatchDashSpacingToSize, propDashed);
            rectEditor = new SceneRectEditor(this);
            fillEditor = new SceneFillEditor(this, propFill, propUseFill);
        }

        private void OnSceneGUI()
        {
            var rect = target as Rectangle;
            var changed = rectEditor.DoSceneHandles(rect);
            var fill = rect.Fill;
            changed |= fillEditor.DoSceneHandles(rect.UseFill, rect, ref fill, rect.transform);
            if (changed)
            {
                rect.Fill = fill;
                rect.UpdateAllMaterialProperties();
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            BeginProperties();
            var multiEditing = serializedObject.isEditingMultipleObjects;

            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.PrefixLabel("Type");
                ShapesUI.DrawTypeSwitchButtons(propType, UIAssets.RectTypeButtonContents);
            }

            EditorGUILayout.PropertyField(propPivot);
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.PrefixLabel("Size");
                using (ShapesUI.TempLabelWidth(14))
                {
                    EditorGUILayout.PropertyField(propWidth, new GUIContent("X"), GUILayout.MinWidth(20));
                    EditorGUILayout.PropertyField(propHeight, new GUIContent("Y"), GUILayout.MinWidth(20));
                }
            }

            var isBorder = ((Rectangle)target).IsBorder;
            using (new EditorGUI.DisabledScope(!multiEditing && isBorder == false))
            {
                ShapesUI.FloatInSpaceField(propThickness, propThicknessSpace);
            }

            var hasRadius = ((Rectangle)target).IsRounded;

            using (new EditorGUI.DisabledScope(hasRadius == false))
            {
                EditorGUILayout.PropertyField(propCornerRadiusMode, new GUIContent("Radius Mode"));
                CornerRadiusProperties();
            }

            rectEditor.GUIEditButton();

            var hasDashablesInSelection = targets.Any(x => (x as Rectangle).IsBorder);
            using (new ShapesUI.GroupScope())
            using (new EditorGUI.DisabledScope(hasDashablesInSelection == false))
            {
                dashEditor.DrawProperties();
            }

            fillEditor.DrawProperties(this);

            EndProperties();
        }

        private void CornerRadiusProperties()
        {
            var radiusMode = (Rectangle.RectangleCornerRadiusMode)propCornerRadiusMode.enumValueIndex;

            if (radiusMode == Rectangle.RectangleCornerRadiusMode.Uniform)
            {
                using (var change = new EditorGUI.ChangeCheckScope())
                {
                    EditorGUI.showMixedValue = propCornerRadii.hasMultipleDifferentValues;
                    var newRadius = Mathf.Max(0f, EditorGUILayout.FloatField("Radius", propCornerRadii.vector4Value.x));
                    EditorGUI.showMixedValue = false;
                    if (change.changed && newRadius != propCornerRadii.vector4Value.x)
                        propCornerRadii.vector4Value = new Vector4(newRadius, newRadius, newRadius, newRadius);
                }
            }
            else
            {
                // per-corner
                var components = propCornerRadii.GetVisibleChildren().ToArray();
                (int component, string label )[] corners = { (1, "↖"), (2, "↗"), (0, "↙"), (3, "↘") };

                void CornerField(string label, int component)
                {
                    EditorGUILayout.PropertyField(components[component], new GUIContent(label), GUILayout.Width(64));
                }

                void RowFields(string label, int a, int b)
                {
                    using (ShapesUI.Horizontal)
                    {
                        GUILayout.Label(label, GUILayout.Width(EditorGUIUtility.labelWidth));
                        using (ShapesUI.TempLabelWidth(18))
                        {
                            CornerField(corners[a].label, corners[a].component);
                            CornerField(corners[b].label, corners[b].component);
                        }
                    }
                }

                RowFields("Radii", 0, 1);
                RowFields(" ", 2, 3);
            }
        }
    }
}