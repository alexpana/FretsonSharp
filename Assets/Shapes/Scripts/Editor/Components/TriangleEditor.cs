using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    [CustomEditor(typeof(Triangle))]
    [CanEditMultipleObjects]
    public class TriangleEditor : ShapeRendererEditor
    {
        private readonly List<Color> colors = new() { default, default, default };

        private DashStyleEditor dashEditor;

        private readonly SerializedProperty propA = null;
        private readonly SerializedProperty propB = null;
        private readonly SerializedProperty propBorder = null;
        private readonly SerializedProperty propC = null;
        private readonly SerializedProperty propColorB = null;
        private readonly SerializedProperty propColorC = null;
        private readonly SerializedProperty propColorMode = null;
        private readonly SerializedProperty propDashed = null;
        private readonly SerializedProperty propDashStyle = null;
        private readonly SerializedProperty propMatchDashSpacingToSize = null;
        private readonly SerializedProperty propRoundness = null;
        private readonly SerializedProperty propThickness = null;
        private readonly SerializedProperty propThicknessSpace = null;
        private ScenePointEditor scenePointEditor;

        public override void OnEnable()
        {
            base.OnEnable();
            dashEditor = DashStyleEditor.GetDashEditor(propDashStyle, propMatchDashSpacingToSize, propDashed);
            scenePointEditor = new ScenePointEditor(this) { hasAddRemoveMode = false, hasEditColorMode = true };
            scenePointEditor.onValuesChanged += OnChangeColor;
        }

        private void OnSceneGUI()
        {
            var tri = target as Triangle;
            var pts = new List<Vector3> { tri.A, tri.B, tri.C };
            if (tri.ColorMode == Triangle.TriangleColorMode.Single)
                (colors[0], colors[1], colors[2]) = (tri.ColorA, tri.ColorA, tri.ColorA);
            else
                (colors[0], colors[1], colors[2]) = (tri.ColorA, tri.ColorB, tri.ColorC);
            var changed = scenePointEditor.DoSceneHandles(false, tri, pts, colors, tri.transform);
            if (changed)
                (tri.A, tri.B, tri.C) = (pts[0], pts[1], pts[2]);
        }

        private void OnChangeColor(ShapeRenderer shape, int changeIndex)
        {
            var tri = shape as Triangle;
            var newColor = colors[changeIndex];
            if (tri.ColorMode == Triangle.TriangleColorMode.Single)
            {
                colors[0] = newColor;
                colors[1] = newColor;
                colors[2] = newColor;
            }

            (tri.ColorA, tri.ColorB, tri.ColorC) = (colors[0], colors[1], colors[2]);
        }

        public override void OnInspectorGUI()
        {
            BeginProperties(false);

            EditorGUILayout.PropertyField(propColorMode);
            EditorGUILayout.PropertyField(propRoundness);
            EditorGUILayout.PropertyField(propBorder);
            var hasBordersInSelection = targets.Any(x => (x as Triangle).Border);
            using (new EditorGUI.DisabledScope(hasBordersInSelection == false))
            {
                ShapesUI.FloatInSpaceField(propThickness, propThicknessSpace);
            }

            if (propColorMode.enumValueIndex == (int)Triangle.TriangleColorMode.Single)
            {
                ShapesUI.PosColorField("A", propA, propColor);
                ShapesUI.PosColorField("B", propB, propColor, false);
                ShapesUI.PosColorField("C", propC, propColor, false);
            }
            else
            {
                ShapesUI.PosColorField("A", propA, propColor);
                ShapesUI.PosColorField("B", propB, propColorB);
                ShapesUI.PosColorField("C", propC, propColorC);
            }

            scenePointEditor.GUIEditButton("Edit Points in Scene");

            using (new ShapesUI.GroupScope())
            using (new EditorGUI.DisabledScope(hasBordersInSelection == false))
            {
                dashEditor.DrawProperties();
            }

            EndProperties();
        }
    }
}