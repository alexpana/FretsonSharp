using System.Linq;
using UnityEditor;
using UnityEngine;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    [CustomEditor(typeof(RegularPolygon))]
    [CanEditMultipleObjects]
    public class RegularPolygonEditor : ShapeRendererEditor
    {
        private static readonly GUILayoutOption[] angLabelLayout = { GUILayout.Width(50) };

        private DashStyleEditor dashEditor;
        private SceneDiscEditor discEditor; // todo: polygonal version
        private SceneFillEditor fillEditor;

        private readonly int[] indexToPolygonPreset = { 3, 4, 5, 6, 8 };
        private readonly SerializedProperty propAngle = null;
        private readonly SerializedProperty propAngUnitInput = null;
        private readonly SerializedProperty propBorder = null;
        private readonly SerializedProperty propDashed = null;
        private readonly SerializedProperty propDashStyle = null;
        private readonly SerializedProperty propFill = null;

        private readonly SerializedProperty propGeometry = null;
        private readonly SerializedProperty propMatchDashSpacingToSize = null;
        private readonly SerializedProperty propRadius = null;
        private readonly SerializedProperty propRadiusSpace = null;
        private readonly SerializedProperty propRoundness = null;
        private readonly SerializedProperty propSides = null;
        private readonly SerializedProperty propThickness = null;
        private readonly SerializedProperty propThicknessSpace = null;
        private readonly SerializedProperty propUseFill = null;

        private GUIContent[] sideCountTypes;

        private GUIContent[] SideCountTypes =>
            sideCountTypes ?? (sideCountTypes = new[]
            {
                new GUIContent(UIAssets.Instance.GetRegularPolygonIcon(3), "Triangle"),
                new GUIContent(UIAssets.Instance.GetRegularPolygonIcon(4), "Square"),
                new GUIContent(UIAssets.Instance.GetRegularPolygonIcon(5), "Pentagon"),
                new GUIContent(UIAssets.Instance.GetRegularPolygonIcon(6), "Hexagon"),
                new GUIContent(UIAssets.Instance.GetRegularPolygonIcon(8), "Octagon")
            });

        public override void OnEnable()
        {
            base.OnEnable();
            dashEditor = DashStyleEditor.GetDashEditor(propDashStyle, propMatchDashSpacingToSize, propDashed);
            fillEditor = new SceneFillEditor(this, propFill, propUseFill);
            discEditor = new SceneDiscEditor(this);
        }

        private void OnSceneGUI()
        {
            var rp = target as RegularPolygon;
            var fill = rp.Fill;
            var changed = discEditor.DoSceneHandles(rp);
            changed |= fillEditor.DoSceneHandles(rp.UseFill, rp, ref fill, rp.transform);
            if (changed)
            {
                rp.Fill = fill;
                rp.UpdateAllMaterialProperties();
            }
        }


        public override void OnInspectorGUI()
        {
            BeginProperties(true);

            EditorGUILayout.PropertyField(propGeometry);
            ShapesUI.DrawTypeSwitchButtons(propSides, SideCountTypes, indexToPolygonPreset);
            //ShapesUI.EnumButtonRow(); // todo
            EditorGUILayout.PropertyField(propSides);
            EditorGUILayout.PropertyField(propRoundness);

            ShapesUI.FloatInSpaceField(propRadius, propRadiusSpace);

            EditorGUILayout.PropertyField(propBorder);
            var hasBordersInSelection = targets.Any(x => (x as RegularPolygon).Border);
            using (new EditorGUI.DisabledScope(hasBordersInSelection == false))
            {
                ShapesUI.FloatInSpaceField(propThickness, propThicknessSpace);
            }

            ShapesUI.AngleProperty(propAngle, "Angle", propAngUnitInput, angLabelLayout);
            ShapesUI.DrawAngleSwitchButtons(propAngUnitInput);

            var canEditInSceneView = propRadiusSpace.hasMultipleDifferentValues ||
                                     propRadiusSpace.enumValueIndex == (int)ThicknessSpace.Meters;
            using (new EditorGUI.DisabledScope(canEditInSceneView == false))
            {
                discEditor.GUIEditButton();
            }


            using (new ShapesUI.GroupScope())
            using (new EditorGUI.DisabledScope(hasBordersInSelection == false))
            {
                dashEditor.DrawProperties();
            }

            fillEditor.DrawProperties(this);

            EndProperties();
        }
    }
}