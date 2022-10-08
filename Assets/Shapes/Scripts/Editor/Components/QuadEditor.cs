using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    [CustomEditor(typeof(Quad))]
    [CanEditMultipleObjects]
    public class QuadEditor : ShapeRendererEditor
    {
        private readonly List<Color> colors = new() { default, default, default, default };

        private readonly SerializedProperty propA = null;
        private readonly SerializedProperty propAutoSetD = null;
        private readonly SerializedProperty propB = null;
        private readonly SerializedProperty propC = null;
        private readonly SerializedProperty propColorB = null;
        private readonly SerializedProperty propColorC = null;
        private readonly SerializedProperty propColorD = null;
        private readonly SerializedProperty propColorMode = null;
        private readonly SerializedProperty propD = null;

        private ScenePointEditor scenePointEditor;

        public override void OnEnable()
        {
            base.OnEnable();
            scenePointEditor = new ScenePointEditor(this) { hasAddRemoveMode = false, hasEditColorMode = true };
            scenePointEditor.onValuesChanged += UpdateColors;
        }

        private void OnSceneGUI()
        {
            var quad = target as Quad;
            var pts = new List<Vector3> { quad.A, quad.B, quad.C, quad.IsUsingAutoD ? quad.DAuto : quad.D };
            colors[0] = quad.ColorA;
            colors[1] = quad.ColorB;
            colors[2] = quad.ColorC;
            colors[3] = quad.ColorD;


            scenePointEditor.positionEnabledArray = new[]
            {
                true, true, true, quad.IsUsingAutoD == false
            };
            UpdateHandleColors(quad);

            var changed = scenePointEditor.DoSceneHandles(false, quad, pts, colors, quad.transform);
            if (changed)
            {
                (quad.A, quad.B, quad.C) = (pts[0], pts[1], pts[2]);
                if (quad.IsUsingAutoD == false) quad.D = pts[3];
                (quad.ColorA, quad.ColorB, quad.ColorC, quad.ColorD) = (colors[0], colors[1], colors[2], colors[3]);
                quad.UpdateAllMaterialProperties();
            }
        }

        private void UpdateColors(ShapeRenderer shape, int changeIndex)
        {
            var quad = shape as Quad;

            var newColor = colors[changeIndex];

            switch ((Quad.QuadColorMode)propColorMode.enumValueIndex)
            {
                case Quad.QuadColorMode.Single:
                    colors[0] = newColor;
                    colors[1] = newColor;
                    colors[2] = newColor;
                    colors[3] = newColor;
                    break;
                case Quad.QuadColorMode.Horizontal:
                    if (changeIndex == 0 || changeIndex == 1)
                    {
                        colors[0] = newColor;
                        colors[1] = newColor;
                    }
                    else
                    {
                        colors[2] = newColor;
                        colors[3] = newColor;
                    }

                    break;
                case Quad.QuadColorMode.Vertical:
                    if (changeIndex == 0 || changeIndex == 3)
                    {
                        colors[0] = newColor;
                        colors[3] = newColor;
                    }
                    else
                    {
                        colors[1] = newColor;
                        colors[2] = newColor;
                    }

                    break;
            }


            (quad.ColorA, quad.ColorB, quad.ColorC, quad.ColorD) = (colors[0], colors[1], colors[2], colors[3]);
        }

        private void UpdateHandleColors(Quad quad)
        {
            switch ((Quad.QuadColorMode)propColorMode.enumValueIndex)
            {
                case Quad.QuadColorMode.Single:
                    colors[0] = quad.ColorA;
                    colors[1] = quad.ColorA;
                    colors[2] = quad.ColorA;
                    colors[3] = quad.ColorA;
                    break;
                case Quad.QuadColorMode.Horizontal:
                    colors[0] = quad.ColorA;
                    colors[1] = quad.ColorA;
                    colors[2] = quad.ColorC;
                    colors[3] = quad.ColorC;
                    break;
                case Quad.QuadColorMode.Vertical:
                    colors[0] = quad.ColorD;
                    colors[1] = quad.ColorB;
                    colors[2] = quad.ColorB;
                    colors[3] = quad.ColorD;
                    break;
                case Quad.QuadColorMode.PerCorner:
                    colors[0] = quad.ColorA;
                    colors[1] = quad.ColorB;
                    colors[2] = quad.ColorC;
                    colors[3] = quad.ColorD;
                    break;
            }
        }


        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            BeginProperties(false);
            EditorGUILayout.PropertyField(propColorMode);

            var dEnabled = propAutoSetD.boolValue == false;
            var dAuto = (target as Quad).DAuto;

            switch ((Quad.QuadColorMode)propColorMode.enumValueIndex)
            {
                case Quad.QuadColorMode.Single:
                    ShapesUI.PosColorField("A", propA, propColor);
                    ShapesUI.PosColorField("B", propB, propColor, false);
                    ShapesUI.PosColorField("C", propC, propColor, false);
                    ShapesUI.PosColorFieldSpecialOffState("D", propD, dAuto, propColor, false, dEnabled);
                    break;
                case Quad.QuadColorMode.Horizontal:
                    ShapesUI.PosColorField("A", propA, propColor);
                    ShapesUI.PosColorField("B", propB, propColor, false);
                    ShapesUI.PosColorField("C", propC, propColorC);
                    ShapesUI.PosColorFieldSpecialOffState("D", propD, dAuto, propColorC, false, dEnabled);
                    break;
                case Quad.QuadColorMode.Vertical:
                    ShapesUI.PosColorField("A", propA, propColorD);
                    ShapesUI.PosColorField("B", propB, propColorB);
                    ShapesUI.PosColorField("C", propC, propColorB, false);
                    ShapesUI.PosColorFieldSpecialOffState("D", propD, dAuto, propColorD, false, dEnabled);
                    break;
                case Quad.QuadColorMode.PerCorner:
                    ShapesUI.PosColorField("A", propA, propColor);
                    ShapesUI.PosColorField("B", propB, propColorB);
                    ShapesUI.PosColorField("C", propC, propColorC);
                    ShapesUI.PosColorFieldSpecialOffState("D", propD, dAuto, propColorD, true, dEnabled);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Label(GUIContent.none, GUILayout.Width(ShapesUI.POS_COLOR_FIELD_LABEL_WIDTH));
                EditorGUILayout.PropertyField(propAutoSetD, GUIContent.none, GUILayout.Width(16));
                GUILayout.Label("Auto-set D");
            }

            scenePointEditor.GUIEditButton("Edit Points in Scene");

            EndProperties();
        }
    }
}