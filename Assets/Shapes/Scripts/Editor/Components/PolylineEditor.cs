using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    [CustomEditor(typeof(Polyline))]
    [CanEditMultipleObjects]
    public class PolylineEditor : ShapeRendererEditor
    {
        private const int MANY_POINTS = 20;
        [SerializeField] private bool hasManyPoints;
        [SerializeField] private bool showPointList = true;

        private ReorderableList pointList;
        private readonly SerializedProperty propClosed = null;
        private readonly SerializedProperty propGeometry = null;
        private readonly SerializedProperty propJoins = null;

        private readonly SerializedProperty propPoints = null;
        private readonly SerializedProperty propThickness = null;
        private readonly SerializedProperty propThicknessSpace = null;

        private ScenePointEditor scenePointEditor;
        private bool showZ;

        public override void OnEnable()
        {
            base.OnEnable();

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

            scenePointEditor = new ScenePointEditor(this) { hasEditThicknessMode = true, hasEditColorMode = true };
        }

        private void OnSceneGUI()
        {
            var p = target as Polyline;
            scenePointEditor.useFlatThicknessHandles = p.Geometry == PolylineGeometry.Flat2D;
            scenePointEditor.hasEditThicknessMode = p.ThicknessSpace == ThicknessSpace.Meters;
            var changed = scenePointEditor.DoSceneHandles(p.Closed, p, p.points, p.transform, p.Thickness, p.Color);
            if (changed)
                p.UpdateMesh(true);
        }

        public override void OnInspectorGUI()
        {
            BeginProperties();
            if (Event.current.type == EventType.Layout)
                showZ = targets.Any(x => ((Polyline)x).Geometry != PolylineGeometry.Flat2D);
            EditorGUILayout.PropertyField(propGeometry);
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(propJoins);
            ShapesUI.FloatInSpaceField(propThickness, propThicknessSpace);

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
            const int checkboxSize = 14;
            const int closedSize = 50;

            var rLabel = r;
            rLabel.width = r.width - checkboxSize - closedSize;
            var rCheckbox = r;
            rCheckbox.x = r.xMax - checkboxSize;
            rCheckbox.width = checkboxSize;
            var rClosed = r;
            rClosed.x = rLabel.xMax;
            rClosed.width = closedSize;
            EditorGUI.LabelField(rLabel, "Points");
            EditorGUI.LabelField(rClosed, "Closed");
            EditorGUI.PropertyField(rCheckbox, propClosed, GUIContent.none);
        }

        // Draws the elements on the list
        private void DrawPointElement(Rect r, int i, bool isActive, bool isFocused)
        {
            r.yMin += 1;
            r.yMax -= 2;
            var prop = propPoints.GetArrayElementAtIndex(i);
            var pPoint = prop.FindPropertyRelative(nameof(PolylinePoint.point));
            var pThickness = prop.FindPropertyRelative(nameof(PolylinePoint.thickness));
            var pColor = prop.FindPropertyRelative(nameof(PolylinePoint.color));

            using (var chChk = new EditorGUI.ChangeCheckScope())
            {
                ShapesUI.PosThicknessColorField(r, pPoint, pThickness, pColor, true, showZ);
                if (chChk.changed)
                    pThickness.floatValue = Mathf.Max(0.001f, pThickness.floatValue); // Make sure it's never 0 or under
            }
        }
    }
}