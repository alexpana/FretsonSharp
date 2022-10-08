using System.Linq;
using UnityEditor;
using UnityEngine;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    [CustomEditor(typeof(Disc))]
    [CanEditMultipleObjects]
    public class DiscEditor : ShapeRendererEditor
    {
        public static GUILayoutOption[] angLabelLayout = { GUILayout.Width(50) };

        private DashStyleEditor dashEditor;
        private SceneDiscEditor discEditor;
        private readonly SerializedProperty propAngRadiansEnd = null;
        private readonly SerializedProperty propAngRadiansStart = null;
        private readonly SerializedProperty propAngUnitInput = null;
        private readonly SerializedProperty propArcEndCaps = null;
        private readonly SerializedProperty propColorInnerEnd = null;
        private readonly SerializedProperty propColorMode = null;
        private readonly SerializedProperty propColorOuterEnd = null;
        private readonly SerializedProperty propColorOuterStart = null;
        private readonly SerializedProperty propDashed = null;
        private readonly SerializedProperty propDashStyle = null;
        private readonly SerializedProperty propGeometry = null;
        private readonly SerializedProperty propMatchDashSpacingToSize = null;
        private readonly SerializedProperty propRadius = null;
        private readonly SerializedProperty propRadiusSpace = null;
        private readonly SerializedProperty propThickness = null;
        private readonly SerializedProperty propThicknessSpace = null;

        private readonly SerializedProperty propType = null;

        public override void OnEnable()
        {
            base.OnEnable();
            dashEditor = DashStyleEditor.GetDashEditor(propDashStyle, propMatchDashSpacingToSize, propDashed);
            discEditor = new SceneDiscEditor(this);
        }

        private void OnSceneGUI()
        {
            var disc = target as Disc;
            var changed = discEditor.DoSceneHandles(disc);
        }

        public override void OnInspectorGUI()
        {
            BeginProperties(false);

            EditorGUILayout.PropertyField(propGeometry);

            // Color properties
            EditorGUILayout.PropertyField(propColorMode);
            switch ((Disc.DiscColorMode)propColorMode.enumValueIndex)
            {
                case Disc.DiscColorMode.Single:
                    PropertyFieldColor();
                    break;
                case Disc.DiscColorMode.Radial:
                    PropertyFieldColor("Inner");
                    EditorGUILayout.PropertyField(propColorOuterStart, new GUIContent("Outer"));
                    break;
                case Disc.DiscColorMode.Angular:
                    PropertyFieldColor("Start");
                    EditorGUILayout.PropertyField(propColorInnerEnd, new GUIContent("End"));
                    break;
                case Disc.DiscColorMode.Bilinear:
                    PropertyFieldColor("Inner Start");
                    EditorGUILayout.PropertyField(propColorOuterStart, new GUIContent("Outer Start"));
                    EditorGUILayout.PropertyField(propColorInnerEnd, new GUIContent("Inner End"));
                    EditorGUILayout.PropertyField(propColorOuterEnd, new GUIContent("Outer End"));
                    break;
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.PrefixLabel("Type");
                ShapesUI.DrawTypeSwitchButtons(propType, UIAssets.DiscTypeButtonContents);
            }

            var selectedType = (DiscType)propType.enumValueIndex;

            if (propType.enumValueIndex == (int)DiscType.Arc)
                ShapesUI.EnumToggleProperty(propArcEndCaps, "Round Caps");
            ShapesUI.FloatInSpaceField(propRadius, propRadiusSpace);
            using (new EditorGUI.DisabledScope(selectedType.HasThickness() == false &&
                                               serializedObject.isEditingMultipleObjects == false))
            {
                ShapesUI.FloatInSpaceField(propThickness, propThicknessSpace);
            }

            DrawAngleProperties(selectedType);

            var canEditInSceneView = propRadiusSpace.hasMultipleDifferentValues ||
                                     propRadiusSpace.enumValueIndex == (int)ThicknessSpace.Meters;
            using (new EditorGUI.DisabledScope(canEditInSceneView == false))
            {
                discEditor.GUIEditButton();
            }

            var hasDashablesInSelection = targets.Any(x => (x as Disc).HasThickness);
            using (new ShapesUI.GroupScope())
            using (new EditorGUI.DisabledScope(hasDashablesInSelection == false))
            {
                dashEditor.DrawProperties();
            }

            EndProperties();
        }

        private void DrawAngleProperties(DiscType selectedType)
        {
            using (new EditorGUI.DisabledScope(selectedType.HasSector() == false &&
                                               serializedObject.isEditingMultipleObjects == false))
            {
                ShapesUI.AngleProperty(propAngRadiansStart, "Angle start", propAngUnitInput, angLabelLayout);
                ShapesUI.AngleProperty(propAngRadiansEnd, "Angle end", propAngUnitInput, angLabelLayout);
                ShapesUI.DrawAngleSwitchButtons(propAngUnitInput);
            }
        }
    }
}