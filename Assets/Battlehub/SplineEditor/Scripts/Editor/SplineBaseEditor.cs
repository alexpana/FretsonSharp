using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Battlehub.SplineEditor
{
    public class SplineBaseEditor : Editor
    {
        public const string UNDO_ADDCURVE = "Battlehub.MeshDeformer.AddCurve";
        public const string UNDO_MOVEPOINT = "Battlehub.MeshDeformer.MovePoint";
        public const string UNDO_CHANGEMODE = "Battlehub.MeshDeformer.ChangePointMode";
        public const string UNDO_TOGGLELOOP = "Battlehub.MeshDeformer.ToggleLoop";

        private const int StepsPerCurve = 5;
        private const float DirectionScale = 0.0f;
        protected const float TwistAngleScale = 0.25f;
        protected const float HandleSize = 0.045f;
        protected const float PickSize = 0.15f;
        protected const float PickSize2 = 0.09f;

        private static readonly Color[] ModeColors =
        {
            Color.yellow,
            Color.blue,
            Color.red
        };

        protected GUIStyle m_addButton;
        protected GUIStyle m_branchButton;
        private readonly Color m_branchColor = new Color32(0xa5, 0x00, 0xff, 0xff);
        private readonly GUIStyle m_greenLabelStyle = new();

        private SplineBase m_splineBase;


        protected SplineBase SelectedSpline { get; private set; }

        protected int SelectedIndex { get; set; } = -1;

        public static SplineBase ConvergingSpline
        {
            get => SplineBase.ConvergingSpline;
            set
            {
                SplineBase.ConvergingSpline = value;
                if (SplineBase.ConvergingSpline)
                {
                    Tools.hidden = true;
                    SceneView.RepaintAll();
                }
                else
                {
                    Tools.hidden = false;
                    SceneView.RepaintAll();
                }
            }
        }

        private void Awake()
        {
            AwakeOverride();
        }

        private void OnEnable()
        {
            OnEnableOverride();

            var spline = GetTarget();
            if (spline) spline.Select();

            m_greenLabelStyle.normal.textColor = Color.green;
            m_addButton = new GUIStyle();
            m_addButton.alignment = TextAnchor.UpperLeft;
            m_addButton.contentOffset = new Vector2(-4, -10);
            m_addButton.normal.textColor = Color.green;


            m_branchButton = new GUIStyle();
            m_branchButton.alignment = TextAnchor.UpperLeft;
            m_branchButton.contentOffset = new Vector2(-4, -10);
            m_branchButton.normal.textColor = m_branchColor;
        }

        private void OnDisable()
        {
            OnDisableOverride();

            var spline = GetTarget();
            if (spline) spline.Unselect();
        }


        private void OnDestroy()
        {
            ConvergingSpline = null;
            OnDestroyOverride();
        }

        private void OnSceneGUI()
        {
            SceneGUIOverride();
        }

        protected virtual void AwakeOverride()
        {
        }

        protected virtual void OnEnableOverride()
        {
        }

        protected virtual void OnDisableOverride()
        {
        }


        protected virtual void OnDestroyOverride()
        {
        }

        public override void OnInspectorGUI()
        {
            var sObj = GetSerializedObject();
            sObj.Update();
            if (m_splineBase == null) m_splineBase = GetTarget();

            if (m_splineBase == null) return;


            if (SelectedIndex >= 0 && SelectedIndex < m_splineBase.ControlPointCount) DrawSelectedPointInspector();

            OnInspectorGUIOverride();


            if (target != null) sObj.ApplyModifiedProperties();
        }

        protected virtual void OnInspectorGUIOverride()
        {
            EditorGUI.BeginChangeCheck();
            var loop = EditorGUILayout.Toggle("Loop", m_splineBase.Loop);
            if (EditorGUI.EndChangeCheck()) ToggleLoop(loop);
        }

        protected virtual void SceneGUIOverride()
        {
            if (m_splineBase == null) m_splineBase = GetTarget();

            if (m_splineBase == null) return;

            var root = m_splineBase.Root;
            ShowPointsRecursive(root);
        }

        private void ShowPointsRecursive(SplineBase spline)
        {
            ShowPoints(spline);
            if (spline.Children != null)
                for (var i = 0; i < spline.Children.Length; ++i)
                {
                    var childSpline = spline.Children[i];
                    ShowPointsRecursive(childSpline);
                }
        }

        private void ShowPoints(SplineBase spline)
        {
            if (!spline.IsSelected) return;
            var handleRotation = Tools.pivotRotation == PivotRotation.Local
                ? spline.transform.rotation
                : Quaternion.identity;

            var firstPointIndex = 0;
            if (spline.IsControlPointLocked(firstPointIndex))
            {
                firstPointIndex++;
                if (spline.IsControlPointLocked(firstPointIndex)) firstPointIndex++;
            }

            var lastPointIndex = spline.ControlPointCount - 1;
            if (spline.IsControlPointLocked(lastPointIndex))
            {
                lastPointIndex--;
                if (spline.IsControlPointLocked(lastPointIndex)) lastPointIndex--;
            }

            if (ConvergingSpline)
            {
                if (spline.Loop)
                {
                    if (firstPointIndex == 0) firstPointIndex++;
                    if (lastPointIndex == spline.ControlPointCount - 1) lastPointIndex--;
                }

                for (var i = firstPointIndex; i <= lastPointIndex; i++)
                    if (i % 3 == 0 && spline != m_splineBase)
                    {
                        var p = spline.GetControlPoint(i);
                        ShowPoint(spline, i, p, handleRotation);
                    }
            }
            else
            {
                for (var i = firstPointIndex; i <= lastPointIndex; i++)
                {
                    var p = spline.GetControlPoint(i);
                    ShowPoint(spline, i, p, handleRotation);
                }
            }

            if (SelectedIndex == -1 && spline == m_splineBase) ShowSplineLength(m_splineBase, m_greenLabelStyle);
        }

        protected virtual int GetStepsPerCurve()
        {
            return StepsPerCurve;
        }

        protected virtual Vector3 GetUpVector()
        {
            return Vector3.up;
        }

        protected virtual Vector3 GetSideVector()
        {
            return Vector3.forward;
        }

        private void ShowPoint(SplineBase spline, int index, Vector3 point, Quaternion handleRotation)
        {
            if (!CanShowPoint(index)) return;

            var hasBranches = spline.HasBranches(index);
            Handles.color = ModeColors[(int)spline.GetControlPointMode(index)];
            if (index % 3 == 0)
            {
                if (hasBranches)
                    Handles.color = m_branchColor;
                else
                    Handles.color = Color.green;
            }

            var size = HandleUtility.GetHandleSize(point);

            Handles.CapFunction dcf = Handles.DotHandleCap;

            if (index == 0)
                if (!hasBranches)
                    size *= 1.5f;


            if (Handles.Button(point, handleRotation, size * HandleSize, size * PickSize, dcf))
            {
                var unselectedSpline = SelectedSpline;
                SelectedSpline = spline;

                var unselectedIndex = SelectedIndex;
                SelectedIndex = index;

                if (OnControlPointClick(unselectedIndex, SelectedIndex))
                {
                    var controlPoint = spline.GetSplineControlPoints().Where(cpt => cpt.Index == index)
                        .FirstOrDefault();
                    if (controlPoint != null) Selection.activeGameObject = controlPoint.gameObject;
                }
                else
                {
                    SelectedIndex = unselectedIndex;
                    SelectedSpline = unselectedSpline;
                }

                Repaint();
            }

            if (SelectedIndex == index && spline == m_splineBase) ShowLengths(spline, index, true);

            ShowPointOverride(spline, index, point, handleRotation, size);
        }

        protected virtual void ShowPointOverride(SplineBase spline, int index, Vector3 point, Quaternion handleRotation,
            float size)
        {
        }

        private void ShowLengths(SplineBase spline, int index, bool allowRecursiveCall)
        {
            var curveIndex = (SelectedIndex + 1) / 3;
            curveIndex = Math.Min(curveIndex, spline.CurveCount - 1);
            ShowLength(spline, curveIndex, m_greenLabelStyle);
        }

        private void ShowSplineLength(SplineBase spline, GUIStyle style)
        {
            var distance = spline.EvalDistance();
            var splineLength = spline.EvalSplineLength(GetStepsPerCurve());
            Handles.Label(spline.GetPoint(0.5f), string.Format("D: {0:0.00} m, S: {1:0.00} m", distance, splineLength),
                style);
        }

        private void ShowLength(SplineBase spline, int curveIndex, GUIStyle style)
        {
            var distance = spline.EvalDistance(curveIndex);
            var curveLength = spline.EvalCurveLength(curveIndex, GetStepsPerCurve());
            var splineLength = spline.EvalSplineLength(GetStepsPerCurve());
            Handles.Label(spline.GetPoint(0.5f, curveIndex),
                string.Format("D: {0:0.00} m, C: {1:0.00} m, S: {2:0.00}", distance, curveLength, splineLength), style);
        }

        protected virtual bool OnControlPointClick(int unselectedIndex, int selectedIndex)
        {
            return true;
        }

        protected virtual bool CanShowPoint(int index)
        {
            return true;
        }

        private void DrawSelectedPointInspector()
        {
            if (DrawSelectedPointInspectorOverride())
            {
                EditorGUI.BeginChangeCheck();
                var mode = (ControlPointMode)
                    EditorGUILayout.EnumPopup("Mode", m_splineBase.GetControlPointMode(SelectedIndex));
                if (EditorGUI.EndChangeCheck()) SetMode(m_splineBase, mode, SelectedIndex);

                EditorGUI.BeginChangeCheck();

                var index = SelectedIndex;
                var isLast = (SelectedIndex + 1) / 3 == m_splineBase.CurveCount;
                var twist = m_splineBase.GetTwist(index);
                EditorGUI.BeginChangeCheck();
                var twistAngle = EditorGUILayout.FloatField("Twist Angle", twist.Data);
                if (EditorGUI.EndChangeCheck()) SetTwistAngle(m_splineBase, index, twistAngle);


                if (m_splineBase.Loop || !isLast || m_splineBase.HasBranches(SelectedIndex))
                {
                    var t1 = twist.T1;
                    var t2 = twist.T2;
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.MinMaxSlider(new GUIContent("Twist Offset"), ref t1, ref t2, 0.0f, 1.0f);
                    if (EditorGUI.EndChangeCheck()) SetTwistOffset(m_splineBase, index, t1, t2);
                }

                var thickness = m_splineBase.GetThickness(index);
                EditorGUI.BeginChangeCheck();
                var thicknessValue = EditorGUILayout.Vector3Field("Thickness", thickness.Data);
                if (EditorGUI.EndChangeCheck()) SetThickness(m_splineBase, index, thicknessValue);

                if (m_splineBase.Loop || !isLast || m_splineBase.HasBranches(SelectedIndex))
                {
                    var t1 = thickness.T1;
                    var t2 = thickness.T2;
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.MinMaxSlider(new GUIContent("Thickness Offset"), ref t1, ref t2, 0.0f, 1.0f);
                    if (EditorGUI.EndChangeCheck()) SetThicknessOffset(m_splineBase, index, t1, t2);
                }
            }
            else
            {
                EditorGUI.BeginChangeCheck();

                var index = SelectedIndex;
                var twist = m_splineBase.GetTwist(index);
                EditorGUI.BeginChangeCheck();
                var twistAngle = EditorGUILayout.FloatField("Twist Angle", twist.Data);
                if (EditorGUI.EndChangeCheck()) SetTwistAngle(m_splineBase, index, twistAngle);

                var thickness = m_splineBase.GetThickness(index);
                EditorGUI.BeginChangeCheck();
                var thicknessValue = EditorGUILayout.Vector3Field("Thickness", thickness.Data);
                if (EditorGUI.EndChangeCheck()) SetThickness(m_splineBase, index, thicknessValue);
            }
        }

        protected virtual bool DrawSelectedPointInspectorOverride()
        {
            return true;
        }

        protected virtual SplineBase GetTarget()
        {
            return (SplineBase)target;
        }

        protected virtual SerializedObject GetSerializedObject()
        {
            return serializedObject;
        }


        protected void ToggleLoop(bool loop)
        {
            do
            {
                if (loop)
                {
                    if (m_splineBase.PrevSpline != null || m_splineBase.NextSpline != null)
                        if (!EditorUtility.DisplayDialog("Creating Loop",
                                "This spline is branch of another spline. This operation will break connection between them. Do you want to proceed?",
                                "Yes", "No"))
                            break;

                    if (m_splineBase.HasBranches(0) || m_splineBase.HasBranches(m_splineBase.ControlPointCount - 1))
                        if (!EditorUtility.DisplayDialog("Creating Loop",
                                "This spline has branches connected to it's ends. This operation will break connection between them. Do you want to proceed?",
                                "Yes", "No"))
                            break;
                }

                RecordHierarchy(m_splineBase.Root, UNDO_TOGGLELOOP);
                EditorUtility.SetDirty(m_splineBase);
                m_splineBase.Loop = loop;
            } while (false);
        }

        public static void Smooth(SplineBase spline)
        {
            RecordHierarchy(spline.Root, "Battlehub.Spline.SetMode");
            spline.Root.Smooth();
            EditorUtility.SetDirty(spline);
        }

        public static void SetMode(SplineBase spline, ControlPointMode mode)
        {
            RecordHierarchy(spline.Root, "Battlehub.Spline.SetMode");
            spline.Root.SetControlPointMode(mode);
            EditorUtility.SetDirty(spline);
        }


        public static void SetMode(SplineBase spline, ControlPointMode mode, int controlPointIndex)
        {
            RecordHierarchy(spline.Root, UNDO_CHANGEMODE);
            EditorUtility.SetDirty(spline);
            spline.SetControlPointMode(controlPointIndex, mode);
        }

        public static void SetTwistAngle(SplineBase spline, int index, float twistAngle)
        {
            RecordHierarchy(spline.Root, "Battlehub.MeshDeformer2 Twist Angle");
            EditorUtility.SetDirty(spline);
            var twist = spline.GetTwist(index);
            twist.Data = twistAngle;
            spline.SetTwist(index, twist);
        }

        public static void SetTwistOffset(SplineBase spline, int index, float t1, float t2)
        {
            var twist = spline.GetTwist(index);
            RecordHierarchy(spline.Root, "Battlehub.MeshDeformer2 Twist Offset");
            EditorUtility.SetDirty(spline);
            twist.T1 = t1;
            twist.T2 = t2;
            spline.SetTwist(index, twist);
        }

        public static void SetThickness(SplineBase spline, int index, Vector3 thicknessValue)
        {
            var thickness = spline.GetThickness(index);
            RecordHierarchy(spline.Root, "Battlehub.MeshDeformer2 Thickness");
            EditorUtility.SetDirty(spline);
            thickness.Data = thicknessValue;
            spline.SetThickness(index, thickness);
        }

        public static void SetThicknessOffset(SplineBase spline, int index, float t1, float t2)
        {
            var thickness = spline.GetThickness(index);
            RecordHierarchy(spline.Root, "Battlehub.MeshDeformer2 Thickness Offset");
            EditorUtility.SetDirty(spline);
            thickness.T1 = t1;
            thickness.T2 = t2;
            spline.SetThickness(index, thickness);
        }

        public static void RecordHierarchy(SplineBase root, string name)
        {
            Undo.RecordObject(root, name);
            if (root.Children != null)
                for (var i = 0; i < root.Children.Length; ++i)
                    RecordHierarchy(root.Children[i], name);
        }

        protected void CapFunction(float size, int id, Vector3 p, GUIStyle style, EventType e)
        {
            if (e == EventType.Repaint)
                Handles.Label(p, "+", style);
            else if (e == EventType.Layout) Layout(id, p, size * PickSize2);
        }

        private void Layout(int id, Vector3 position, float pickSize)
        {
            var screenPosition = Handles.matrix.MultiplyPoint(position);

            var cachedMatrix = Handles.matrix;
            Handles.matrix = Matrix4x4.identity;
            HandleUtility.AddControl(id, HandleUtility.DistanceToCircle(screenPosition, pickSize));
            Handles.matrix = cachedMatrix;
        }
    }
}