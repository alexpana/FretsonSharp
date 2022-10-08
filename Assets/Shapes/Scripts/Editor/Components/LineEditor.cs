using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    [CustomEditor(typeof(Line))]
    [CanEditMultipleObjects]
    public class LineEditor : ShapeRendererEditor
    {
        private DashStyleEditor dashEditor;
        private readonly SerializedProperty propColorEnd = null;
        private readonly SerializedProperty propColorMode = null;
        private readonly SerializedProperty propDashed = null;
        private readonly SerializedProperty propDashStyle = null;
        private readonly SerializedProperty propEnd = null;
        private readonly SerializedProperty propEndCaps = null;

        private readonly SerializedProperty propGeometry = null;
        private readonly SerializedProperty propMatchDashSpacingToSize = null;
        private readonly SerializedProperty propStart = null;
        private readonly SerializedProperty propThickness = null;
        private readonly SerializedProperty propThicknessSpace = null;
        private ScenePointEditor scenePointEditor;

        public override void OnEnable()
        {
            base.OnEnable();
            dashEditor =
                DashStyleEditor.GetLineDashEditor(propDashStyle, propMatchDashSpacingToSize, propGeometry, propDashed);
            scenePointEditor = new ScenePointEditor(this) { hasAddRemoveMode = false };
        }

        private void OnSceneGUI()
        {
            var l = target as Line;
            var pts = new List<Vector3> { l.Start, l.End };
            var changed = scenePointEditor.DoSceneHandles(false, l, pts, l.transform);
            if (changed)
            {
                l.Start = pts[0];
                l.End = pts[1];
            }
        }

        public override void OnInspectorGUI()
        {
            var so = serializedObject;

            // show detail edit
            var showDetailEdit = targets.OfType<Line>().Any(x => x.Geometry == LineGeometry.Volumetric3D);
            BeginProperties(false, showDetailEdit);

            var updateGeometry = false;
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(propGeometry, new GUIContent("Geometry"));
            if (EditorGUI.EndChangeCheck())
                updateGeometry = true;
            EditorGUILayout.PropertyField(propStart);
            EditorGUILayout.PropertyField(propEnd);
            ShapesUI.FloatInSpaceField(propThickness, propThicknessSpace);
            scenePointEditor.GUIEditButton("Edit Points in Scene");

            // style (color, caps, dashes)
            using (new ShapesUI.GroupScope())
            {
                EditorGUILayout.PropertyField(propColorMode);
                if ((Line.LineColorMode)propColorMode.enumValueIndex == Line.LineColorMode.Single)
                    PropertyFieldColor();
                else
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.PrefixLabel("Colors");
                        PropertyFieldColor(GUIContent.none);
                        EditorGUILayout.PropertyField(propColorEnd, GUIContent.none);
                    }

                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.PrefixLabel("End Caps");
                    if (ShapesUI.DrawTypeSwitchButtons(propEndCaps, UIAssets.LineCapButtonContents))
                        updateGeometry = true;
                }
            }


            // Dashes
            using (new ShapesUI.GroupScope())
            {
                dashEditor.DrawProperties();
            }

            EndProperties();

            if (updateGeometry)
                foreach (var line in targets.Cast<Line>())
                    line.UpdateMesh();
        }
    }
}