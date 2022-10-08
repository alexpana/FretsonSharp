using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    [CustomEditor(typeof(Polygon))]
    [CanEditMultipleObjects]
    public class PolygonEditor : ShapeRendererEditor
    {
        private const int MANY_POINTS = 20;
        [SerializeField] private bool hasManyPoints;
        [SerializeField] private bool showPointList = true;

        private SceneFillEditor fillEditor;
        private ReorderableList pointList;
        private readonly SerializedProperty propFill = null;

        private readonly SerializedProperty propPoints = null;
        private readonly SerializedProperty propTriangulation = null;
        private readonly SerializedProperty propUseFill = null;
        private ScenePointEditor scenePointEditor;

        public override void OnEnable()
        {
            base.OnEnable();

            fillEditor = new SceneFillEditor(this, propFill, propUseFill);
            scenePointEditor = new ScenePointEditor(this);

            pointList = new ReorderableList(serializedObject, propPoints, true, true, true, true)
            {
                drawElementCallback = DrawPointElement,
                drawHeaderCallback = PointListHeader
            };

            if (pointList.count > MANY_POINTS)
            {
                hasManyPoints = true;
                showPointList = false;
            }
        }


        private void OnSceneGUI()
        {
            var p = target as Polygon;
            var fill = p.Fill;
            var changed = fillEditor.DoSceneHandles(p.UseFill, p, ref fill, p.transform);
            changed |= scenePointEditor.DoSceneHandles(true, p, p.points, p.transform);
            if (changed)
            {
                p.Fill = fill;
                p.UpdateMesh(true);
                p.UpdateAllMaterialProperties();
            }
        }

        public override void OnInspectorGUI()
        {
            BeginProperties();
            EditorGUILayout.PropertyField(propTriangulation);

            var changed = fillEditor.DrawProperties(this);

            if (hasManyPoints)
            {
                // to prevent lag when inspecting polylines with many points
                var foldoutLabel = showPointList ? "Hide" : "Show Points";
                showPointList = GUILayout.Toggle(showPointList, foldoutLabel, EditorStyles.foldout);
            }

            if (showPointList)
                pointList.DoLayoutList();
            scenePointEditor.GUIEditButton("Edit Points in Scene");

            EndProperties();
        }

        private void PointListHeader(Rect r)
        {
            EditorGUI.LabelField(r, "Points");
        }

        // Draws the elements on the list
        private void DrawPointElement(Rect r, int i, bool isActive, bool isFocused)
        {
            r.yMin += 1;
            r.yMax -= 2;
            var prop = propPoints.GetArrayElementAtIndex(i);
            EditorGUI.PropertyField(r, prop, GUIContent.none);
        }
    }
}