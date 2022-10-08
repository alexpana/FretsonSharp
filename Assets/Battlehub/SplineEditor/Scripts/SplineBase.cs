#if USE_BINARY_FORMATTER
using System.Runtime.Serialization;
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using Battlehub.RTEditor;
using Battlehub.RTHandles;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Rendering;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Battlehub.SplineEditor
{
    public enum ControlPointMode
    {
        Free,
        Aligned,
        Mirrored
    }

    public enum ExtendAction
    {
        Append,
        Prepend,
        Insert
    }

    [Serializable]
    public struct Vector3Serialziable
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3Serialziable(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static implicit operator Vector3(Vector3Serialziable v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }

        public static implicit operator Vector3Serialziable(Vector3 v)
        {
            return new Vector3Serialziable(v.x, v.y, v.z);
        }
    }

#if USE_BINARY_FORMATTER
    [Serializable]
    public class Vector3SerialziableArray : List<Vector3Serialziable>
    {
        public static implicit operator Vector3[] (Vector3SerialziableArray v)
        {
            Vector3[] result = new Vector3[v.Count];
            for (int i = 0; i < v.Count; ++i)
            {
                result[i] = v[i];
            }
            return result;
        }

        public static implicit operator Vector3SerialziableArray(Vector3[] v)
        {
            Vector3SerialziableArray result = new Vector3SerialziableArray();
            for (int i = 0; i < v.Length; ++i)
            {
                result.Add(v[i]);
            }
            return result;
        }
    }
    [Serializable]
    public struct SplineSnapshot
    {
        [SerializeField]
        private Vector3SerialziableArray m_points;

        [SerializeField]
        private ControlPointSetting[] m_controlPointSettings;

        [SerializeField]
        private ControlPointMode[] m_modes;

        [SerializeField]
        private bool m_loop;

        public int CurveCount
        {
            get { return (m_points.Count - 1) / 3; }
        }

        public Vector3SerialziableArray Points
        {
            get { return m_points; }
        }

        public ControlPointSetting[] ControlPointSettings
        {
            get { return m_controlPointSettings; }
        }

        public ControlPointMode[] Modes
        {
            get { return m_modes; }
        }

        public bool Loop
        {
            get { return m_loop; }
        }

        public SplineSnapshot(Vector3[] points, ControlPointSetting[] settings, ControlPointMode[] modes, bool loop)
        {
            int modeLength = (points.Length - 1) / 3;
            int settingLength = (points.Length - 1) / 2;
            int pointsLength = modeLength * 3 + 1;
            modeLength += 1;
            if (modeLength < 1)
            {
                throw new ArgumentException("too few points. at least 4 required");
            }

            m_points = points;
            if (pointsLength != m_points.Count)
            {
                Array.Resize(ref points, pointsLength);
            }

            m_controlPointSettings = settings;
            if (settingLength != m_controlPointSettings.Length)
            {
                Array.Resize(ref settings, settingLength);
            }

            m_modes = modes;
            if (modeLength != m_modes.Length)
            {
                Array.Resize(ref m_modes, modeLength);
            }

            m_loop = loop;
        }
    }

    public class Vector3SerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Vector3 v3 = (Vector3)obj;
            info.AddValue("x", v3.x);
            info.AddValue("y", v3.y);
            info.AddValue("z", v3.z);
        }
        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Vector3 v3 = (Vector3)obj;
            v3.x = (float)info.GetValue("x", typeof(float));
            v3.y = (float)info.GetValue("y", typeof(float));
            v3.z = (float)info.GetValue("z", typeof(float));
            obj = v3;
            return obj;
        }
    }
#else
    [Serializable]
    public struct SplineSnapshot
    {
        [SerializeField] private Vector3[] m_points;

        [SerializeField] private ControlPointSetting[] m_controlPointSettings;

        [SerializeField] private ControlPointMode[] m_modes;

        [SerializeField] private bool m_loop;

        public SplineSnapshot(Vector3[] points, ControlPointSetting[] settings, ControlPointMode[] modes, bool loop)
        {
            var modeLength = (points.Length - 1) / 3;
            var settingLength = (points.Length - 1) / 2;
            var pointsLength = modeLength * 3 + 1;
            modeLength += 1;
            if (modeLength < 1) throw new ArgumentException("too few points. at least 4 required");

            m_points = points;
            if (pointsLength != m_points.Length) Array.Resize(ref points, pointsLength);

            m_controlPointSettings = settings;
            if (settingLength != m_controlPointSettings.Length) Array.Resize(ref settings, settingLength);

            m_modes = modes;
            if (modeLength != m_modes.Length) Array.Resize(ref m_modes, modeLength);

            m_loop = loop;
        }

        public int CurveCount => (m_points.Length - 1) / 3;

        public Vector3[] Points => m_points;

        public ControlPointSetting[] ControlPointSettings => m_controlPointSettings;

        public ControlPointMode[] Modes => m_modes;

        public bool Loop => m_loop;
    }
#endif


    public delegate void ControlPointChanged(int pointIndex);

    [Serializable]
    public struct Twist
    {
        public static readonly Twist Null = new();

        public float Data;
        public float T1;
        public float T2;

        public Twist(float data, float t1, float t2)
        {
            Data = data;
            T1 = t1;
            T2 = t2;
        }
    }

    [Serializable]
    public struct Thickness
    {
        public static readonly Thickness Null = new();

        //upgarde from 2.03 to 2.04 or higher
        //public Vector3 Data;
        public Vector3Serialziable Data;
        public float T1;
        public float T2;

        public Thickness(Vector3 data, float t1, float t2)
        {
            Data = data;
            T1 = t1;
            T2 = t2;
        }
    }

    [Serializable]
    public struct SplineBranch
    {
        public int SplineIndex;
        public bool Inbound;

        public SplineBranch(int splineIndex, bool inbound)
        {
            SplineIndex = splineIndex;
            Inbound = inbound;
        }
    }

    [Serializable]
    public struct ControlPointSetting
    {
        public Twist Twist;
        public Thickness Thickness;
        public SplineBranch[] Branches;

        public ControlPointSetting(Twist twist, Thickness thickness, SplineBranch[] connections)
        {
            Twist = twist;
            Thickness = thickness;
            Branches = connections;
        }

        public ControlPointSetting(Twist twist, Thickness thickness)
        {
            Twist = twist;
            Thickness = thickness;
            Branches = null;
        }
    }

    [ExecuteInEditMode]
    public class SplineBase : MonoBehaviour, IGL
    {
        //SPLINE RENDERING
        private static readonly Color SplineColor = Color.green;
        private static readonly float Smoothness = 5.0f;
        private static SplineBase m_convergingSpline;

        //FIELDS
        [SerializeField] [HideInInspector] private ControlPointMode[] m_modes;

        [SerializeField] [HideInInspector] private Vector3[] m_points;

        //upgarde from 2.03 to 2.04 or higher
        //[UnityEngine.Serialization.FormerlySerializedAs("m_controlPointSettings")]
        [SerializeField] [HideInInspector] private ControlPointSetting[] m_settings;

        [SerializeField] [HideInInspector] private bool m_loop;

        [SerializeField] [HideInInspector] private SplineBase m_prevSpline;

        [SerializeField] [HideInInspector] private int m_prevControlPointIndex;

        [SerializeField] [HideInInspector] private SplineBase m_nextSpline;

        [SerializeField] [HideInInspector] private int m_nextControlPointIndex;

        [SerializeField] [HideInInspector] private SplineBase[] m_branches;

        [SerializeField] [HideInInspector] private SplineBase m_parent;

        [SerializeField] [HideInInspector] private SplineBase[] m_children;

        private bool m_isApplicationQuit = false;

        //[SerializeField, HideInInspector] do not serialize this field
        //[SerializeField, HideInInspector] //undo/redo support

        public static SplineBase ActiveSpline { get; set; }

        public static int ActiveControlPointIndex { get; set; } = -1;

        public static SplineBase ConvergingSpline
        {
            get => m_convergingSpline;
            set
            {
                if (m_convergingSpline != value)
                {
                    m_convergingSpline = value;
                    if (ConvergingSplineChanged != null) ConvergingSplineChanged(null, EventArgs.Empty);
                }
            }
        }


        public static Material SplineMaterial { get; private set; }

        public static bool SplineMaterialZTest
        {
            get => PlayerPrefs.GetInt("Battehub.SplineEditor.SplineMaterialZTest", 0) == 1;
            set
            {
                if (SplineMaterial != null) SetSplieMaterialZTest(value);

                PlayerPrefs.SetInt("Battehub.SplineEditor.SplineMaterialZTest", value ? 1 : 0);
            }
        }

        //PROPERTIES
        public int NextControlPointIndex => m_nextControlPointIndex;

        public SplineBase NextSpline => m_nextSpline;

        public int PrevControlPointIndex => m_prevControlPointIndex;

        public SplineBase PrevSpline => m_prevSpline;

        public bool IsSelected { get; private set; }

        public virtual bool Loop
        {
            get => m_loop;
            set
            {
                m_loop = value;
                if (m_loop)
                {
                    Disconnect(0);
                    Disconnect(ControlPointCount - 1);
                    if (PrevSpline != null) PrevSpline.Disconnect(this, false);

                    if (NextSpline != null) NextSpline.Disconnect(this, true);


                    var setting = m_settings[m_settings.Length - 1];
                    m_settings[0] = new ControlPointSetting(setting.Twist, setting.Thickness, m_settings[0].Branches);

                    m_modes[m_modes.Length - 1] = m_modes[0];
                    RaiseControlPointModeChanged(m_modes.Length - 1);
                    _SetControlPointLocalUnchecked(m_points.Length - 1, m_points[0]);
                }
            }
        }

        public int CurveCount => (m_points.Length - 1) / 3;

        public int ControlPointCount => m_points.Length;

        public SplineBase Root
        {
            get
            {
                var parent = this;
                while (parent.Parent != null) parent = parent.Parent;
                return parent;
            }
        }

        public SplineBase Parent => m_parent;

        public SplineBase[] Children => m_children;


        //LIFECYCLE METHODS
        private void Awake()
        {
            if (SplineMaterial == null) InitSplineMaterial();

            if (m_branches == null) m_branches = new SplineBase[0];

            UpdateChildrenAndParent();

            SplineRuntimeEditor.Created += OnRuntimeEditorCreated;
            SplineRuntimeEditor.Destroyed += OnRuntimeEditorDestroyed;

            if (SplineRuntimeEditor.Instance != null)
                if (!GetComponent<ExposeToEditor>())
                    gameObject.AddComponent<ExposeToEditor>();

            SyncArrays();

#if UNITY_EDITOR
            SyncVersions();
            Undo.undoRedoPerformed += OnUndoRedoPerformed;
            if (m_points == null) Reset();

            m_initialized = true;
#endif

            AwakeOverride();
        }

        private void Reset()
        {
            var connectedSplines = m_branches;
            var nextSpline = m_nextSpline;
            var prevSpline = m_prevSpline;
            if (connectedSplines != null)
                for (var i = 0; i < connectedSplines.Length; ++i)
                {
                    var connectedSpline = connectedSplines[i];
                    if (connectedSpline != null) connectedSpline.Disconnect(this);
                }

            if (nextSpline != null) nextSpline.Disconnect(this);
            if (prevSpline != null) prevSpline.Disconnect(this);
            m_branches = new SplineBase[0];
            m_nextSpline = null;
            m_nextControlPointIndex = -1;
            m_prevSpline = null;
            m_prevControlPointIndex = -1;

            m_points = new[]
            {
                new(0f, 0f, 0f),
                new Vector3(1.0f / 3.0f * GetMag(), 0f, 0f),
                new Vector3(2.0f / 3.0f * GetMag(), 0f, 0f),
                new Vector3(1.0f * GetMag(), 0f, 0f)
            };

            m_settings = new[]
            {
                new(new Twist(0.0f, 0.0f, 1.0f), new Thickness(Vector3.one, 0.0f, 1.0f), new SplineBranch[0]),
                new ControlPointSetting(new Twist(0.0f, 0.0f, 1.0f), new Thickness(Vector3.one, 0.0f, 1.0f),
                    new SplineBranch[0])
            };

            m_modes = new[]
            {
                ControlPointMode.Free,
                ControlPointMode.Free
            };

            ResetOverride();
            SyncCtrlPoints();
        }

        private void Start()
        {
            StartOverride();

            if (PrevSpline != null)
                if (PrevSpline.m_branches == null || !PrevSpline.m_branches.Contains(this))
                    PrevSpline.Connect(this, PrevControlPointIndex, false);

            if (NextSpline != null)
                if (NextSpline.m_branches == null || !NextSpline.m_branches.Contains(this))
                    NextSpline.Connect(this, NextControlPointIndex, true);
        }

        private void Update()
        {
            UpdateOverride();
#if UNITY_EDITOR
            if (!m_initialized)
            {
                m_initialized = true;
                if (GLRenderer.Instance != null)
                {
                    if (IsSelected) GLRenderer.Instance.Add(this);
                    SubscribeUndoRedo();
                    UpdateSceneCameras();
                }
                else
                {
                    SubscribeUndoRedo();
                }
            }
#endif
        }

        private void OnEnable()
        {
            if (IsSelected)
                if (GLRenderer.Instance != null)
                    GLRenderer.Instance.Add(this);

            OnEnableOverride();
        }

        private void OnDisable()
        {
            if (GLRenderer.Instance != null) GLRenderer.Instance.Remove(this);

            OnDisableOverride();
        }

        private void OnDestroy()
        {
            SplineRuntimeEditor.Created -= OnRuntimeEditorCreated;
            SplineRuntimeEditor.Destroyed -= OnRuntimeEditorDestroyed;

            var enteringPlayMode = false;
#if UNITY_EDITOR
            enteringPlayMode = EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying;
            Undo.undoRedoPerformed -= OnUndoRedoPerformed;
#endif

            if (!m_isApplicationQuit && !enteringPlayMode)
            {
                UnselectRecursive(Root);

                if (m_prevSpline != null) m_prevSpline.Disconnect(this);

                if (m_nextSpline != null) m_nextSpline.Disconnect(this);
            }

            OnDestroyOverride();
        }

        private void OnApplicationQuit()
        {
            m_isApplicationQuit = true;
        }

        private void OnTransformChildrenChanged()
        {
            UpdateChildrenAndParent();
        }

        private void OnTransformParentChanged()
        {
            UpdateChildrenAndParent();
        }

        void IGL.Draw()
        {
            if (m_points.Length < 2) return;

            if (SplineMaterial == null) InitSplineMaterial();

            SplineMaterial.SetPass(0);
            GL.PushMatrix();
            GL.MultMatrix(transform.localToWorldMatrix);
            GL.Begin(GL.LINES);
            var p0 = m_points[0];
            for (var i = 1; i < m_points.Length; i += 3)
            {
                var p1 = m_points[i];
                var p2 = m_points[i + 1];
                var p3 = m_points[i + 2];

                if (!ConvergingSpline)
                {
                    GL.Color(SplineRuntimeEditor.ControlPointLineColor);
                    GL.Vertex(p0);
                    GL.Vertex(p1);

                    GL.Color(SplineRuntimeEditor.ControlPointLineColor);
                    GL.Vertex(p2);
                    GL.Vertex(p3);
                }

                p0 = p3;
            }

            GL.End();
            GL.Begin(GL.LINES);
            GL.Color(SplineColor);
            p0 = m_points[0];
            for (var i = 1; i < m_points.Length; i += 3)
            {
                var p1 = m_points[i];
                var p2 = m_points[i + 1];
                var p3 = m_points[i + 2];

                var len = (p0 - p1).magnitude + (p1 - p2).magnitude + (p2 - p3).magnitude;
                var count = Mathf.CeilToInt(Smoothness * len);
                if (count <= 0) count = 1;

                for (var j = 0; j < count; ++j)
                {
                    var t = (float)j / count;
                    var point = CurveUtils.GetPoint(p0, p1, p2, p3, t);

                    GL.Vertex(point);

                    t = ((float)j + 1) / count;
                    point = CurveUtils.GetPoint(p0, p1, p2, p3, t);

                    GL.Vertex(point);
                }

                p0 = p3;
            }


            ShowTwistAngles();
            GL.End();
            GL.PopMatrix();
        }

        public static event EventHandler ConvergingSplineChanged;

        private static void SetSplieMaterialZTest(bool value)
        {
            if (value)
                SplineMaterial.SetInt("_ZTest", (int)CompareFunction.LessEqual);
            else
                SplineMaterial.SetInt("_ZTest", (int)CompareFunction.Always);
        }

        private static void InitSplineMaterial()
        {
            var shader = Shader.Find("Battlehub/SplineEditor/Spline");
            SplineMaterial = new Material(shader);
            SplineMaterial.name = "SplineMaterial";
            SplineMaterial.color = SplineColor;
            SetSplieMaterialZTest(SplineMaterialZTest);

            var glRenderer = FindObjectOfType<GLRenderer>();
            if (glRenderer == null)
            {
                var go = new GameObject();
                go.name = "GLRenderer";
                go.AddComponent<GLRenderer>();
            }
        }

        protected virtual void ShowTwistAngles()
        {
            GL.Color(SplineRuntimeEditor.ControlPointLineColor);
            var steps = GetStepsPerCurve() * CurveCount;
            for (var i = 0; i <= steps; i++) DrawTwistAngle(i, steps);

            if (ActiveSpline == this && ActiveControlPointIndex > -1 &&
                ActiveControlPointIndex < ActiveSpline.ControlPointCount)
            {
                GL.Color(SplineColor);
                var curveIndex = (ActiveControlPointIndex + 1) / 3;
                curveIndex = Math.Min(curveIndex, CurveCount - 1);
                steps = GetStepsPerCurve() * 5;
                var twist = GetTwist(ActiveControlPointIndex);
                var firstStep = Mathf.CeilToInt(twist.T1 * steps);
                var lastStep = Mathf.CeilToInt(twist.T2 * steps);

                for (var i = firstStep; i <= lastStep; ++i) DrawTwistAngle(curveIndex, i, steps);
            }
        }

        private void DrawTwistAngle(int i, int steps)
        {
            var t = i / (float)steps;
            var dir = GetDirection(t);
            var point = GetPointLocal(t);
            var twistAngle = GetTwist(t);
            Vector3 v3;
            var up = GetUpVector();
            if (Math.Abs(Vector3.Dot(dir, up)) < 1.0f)
                v3 = Vector3.Cross(dir, up).normalized;
            else
                v3 = Vector3.Cross(dir, GetSideVector()).normalized;
            if (dir == Vector3.zero) return;
            GL.Vertex(point);
            GL.Vertex(point + Quaternion.AngleAxis(twistAngle, dir) * Quaternion.LookRotation(v3, up) *
                Vector3.forward * 0.5f);
        }

        private void DrawTwistAngle(int curveIndex, int i, int steps)
        {
            var t = i / (float)steps;
            var dir = GetDirection(t, curveIndex);
            var point = GetPointLocal(t, curveIndex);
            var twistAngle = GetTwist(t, curveIndex);
            Vector3 v3;
            var up = GetUpVector();
            if (Math.Abs(Vector3.Dot(dir, up)) < 1.0f)
                v3 = Vector3.Cross(dir, up).normalized;
            else
                v3 = Vector3.Cross(dir, GetSideVector()).normalized;
            if (dir == Vector3.zero) return;
            GL.Vertex(point);
            GL.Vertex(point + Quaternion.AngleAxis(twistAngle, dir) * Quaternion.LookRotation(v3, up) *
                Vector3.forward * 0.5f);
        }


        protected virtual int GetStepsPerCurve()
        {
            return 5;
        }

        protected virtual Vector3 GetUpVector()
        {
            return Vector3.up;
        }

        protected virtual Vector3 GetSideVector()
        {
            return Vector3.forward;
        }

        //EVENTS
        public event ControlPointChanged ControlPointPositionChanged;
        public event ControlPointChanged ControlPointModeChanged;
        public event ControlPointChanged ControlPointConnectionChanged;
        public event ControlPointChanged ControlPointThicknessChanged;
        public event ControlPointChanged ControlPointTwistChanged;

        private void RaiseControlPointThicknessChanged(int index)
        {
            if (ControlPointThicknessChanged != null) ControlPointThicknessChanged(index);
        }

        private void RaisControlPointTwistChanged(int index)
        {
            if (ControlPointTwistChanged != null) ControlPointTwistChanged(index);
        }

        private void RaiseControlPointChanged(int index)
        {
            if (ControlPointPositionChanged != null) ControlPointPositionChanged(index);
        }

        private void RaiseControlPointModeChanged(int modeIndex)
        {
            if (ControlPointModeChanged != null)
            {
                var pointIndex = modeIndex * 3 - 1;

                ControlPointModeChanged(pointIndex);
                ControlPointModeChanged(pointIndex + 1);
                ControlPointModeChanged(pointIndex + 2);
            }
        }

        private void RaiseControlPointConnectionChanged(int index)
        {
            if (ControlPointConnectionChanged != null) ControlPointConnectionChanged(index);
        }

        protected virtual float GetMag()
        {
            return 1.0f;
        }

        //LIFECYCLE METHOD OVERRIDES
        protected virtual void AwakeOverride()
        {
        }

        protected virtual void OnDestroyOverride()
        {
        }

        protected virtual void OnEnableOverride()
        {
        }

        protected virtual void OnDisableOverride()
        {
        }

        protected virtual void StartOverride()
        {
        }

        protected virtual void UpdateOverride()
        {
        }

        protected virtual void ResetOverride()
        {
        }

        private void OnRuntimeEditorCreated(object sender, EventArgs e)
        {
            if (IsSelected)
                if (GLRenderer.Instance != null)
                    GLRenderer.Instance.Add(this);

            if (this)
                if (!GetComponent<ExposeToEditor>())
                    gameObject.AddComponent<ExposeToEditor>();
        }

        private void OnRuntimeEditorDestroyed(object sender, EventArgs e)
        {
            if (GLRenderer.Instance != null) GLRenderer.Instance.Remove(this);

            if (this)
            {
                var exposeToEditor = GetComponent<ExposeToEditor>();
                if (exposeToEditor) DestroyImmediate(exposeToEditor);
            }
        }

        //UPGRADE METHOD
        private void SyncArrays()
        {
            if (m_points != null && m_points.Length > 0)
            {
                var modesLength = m_points.Length / 3 + 1;
                if (m_modes.Length != modesLength)
                {
                    Debug.Log("Synchronize modes");
                    Array.Resize(ref m_modes, modesLength);
                }

                if (m_settings == null) m_settings = new ControlPointSetting[0];
                if (m_settings.Length != modesLength)
                {
                    Debug.Log("Synchronize settings");
                    var oldLength = m_settings.Length;
                    Array.Resize(ref m_settings, modesLength);
                    for (var i = oldLength; i < m_settings.Length; ++i)
                    {
                        m_settings[i].Thickness = new Thickness(Vector3.one, 0.0f, 1.0f);
                        m_settings[i].Twist = new Twist(0.0f, 0.0f, 1.0f);
                    }
                }
            }
        }

        /// <summary>
        ///     Select Spline (make it visible in unity or runtime editor)
        /// </summary>
        public void Select()
        {
            if (IsSelected) return;

            SelectRecursive(Root);


#if UNITY_EDITOR
            UpdateSceneCameras();
#endif
        }

        private void SelectRecursive(SplineBase spline)
        {
            if (GLRenderer.Instance != null) GLRenderer.Instance.Add(spline);

            var ctrlPoints = spline.GetSplineControlPoints();
            for (var i = 0; i < ctrlPoints.Length; ++i)
            {
                var ctrlPoint = ctrlPoints[i];
                ctrlPoint.gameObject.SetActive(true);
            }

            spline.IsSelected = true;
            for (var i = 0; i < spline.m_children.Length; ++i) SelectRecursive(spline.m_children[i]);
        }

        /// <summary>
        ///     Unselect spline (hide in unity or runtime editor)
        /// </summary>
        public void Unselect()
        {
            if (!IsSelected) return;

            UnselectRecursive(Root);
        }

        private void UnselectRecursive(SplineBase spline)
        {
            if (GLRenderer.Instance != null) GLRenderer.Instance.Remove(spline);

            var ctrlPoints = spline.GetSplineControlPoints();
            for (var i = 0; i < ctrlPoints.Length; ++i)
            {
                var ctrlPoint = ctrlPoints[i];
                if (ctrlPoint) ctrlPoint.gameObject.SetActive(false);
            }

            spline.IsSelected = false;
            for (var i = 0; i < spline.m_children.Length; ++i) UnselectRecursive(spline.m_children[i]);
        }

        /// <summary>
        ///     Get interpolated point of specified curve (world space coordinates)
        /// </summary>
        /// <param name="t">[0.0 - beginning of the curve, 1.0 - end of the curve]</param>
        /// <param name="curveIndex">[0 to CurveCount-1]</param>
        /// <returns>world space point</returns>
        public Vector3 GetPoint(float t, int curveIndex)
        {
            curveIndex *= 3;
            return transform.TransformPoint(CurveUtils.GetPoint(m_points[curveIndex], m_points[curveIndex + 1],
                m_points[curveIndex + 2], m_points[curveIndex + 3], t));
        }

        /// <summary>
        ///     Get interpolated point of specified curve (object space coordinates)
        /// </summary>
        /// <param name="t">[0.0 - beginning of the curve, 1.0 - end of the curve]</param>
        /// <param name="curveIndex">[0 to CurveCount-1]</param>
        /// <returns>object space point</returns>
        public Vector3 GetPointLocal(float t, int curveIndex)
        {
            curveIndex *= 3;
            return CurveUtils.GetPoint(m_points[curveIndex], m_points[curveIndex + 1], m_points[curveIndex + 2],
                m_points[curveIndex + 3], t);
        }

        /// <summary>
        ///     Returns curve index, and remainder of t
        /// </summary>
        /// <param name="t">in t [0, 1] out remainder</param>
        /// <returns></returns>
        public int ToCurveIndex(ref float t)
        {
            int i;
            if (t >= 1f)
            {
                t = 1f;
                i = (m_points.Length - 1) / 3 - 1;
            }
            else
            {
                t = Mathf.Clamp01(t) * CurveCount;
                i = (int)t;
                t -= i;
            }

            return i;
        }

        /// <summary>
        ///     Returns curve index
        /// </summary>
        /// <param name="t">in t [0, 1]</param>
        /// <returns></returns>
        public int ToCurveIndex(float t)
        {
            int i;
            if (t >= 1f)
            {
                t = 1f;
                i = (m_points.Length - 1) / 3 - 1;
            }
            else
            {
                t = Mathf.Clamp01(t) * CurveCount;
                i = (int)t;
                t -= i;
            }

            return i;
        }

        /// <summary>
        ///     Get interpolated point (world space coordinates)
        /// </summary>
        /// <param name="t">[0.0 - beginning of the spline, 1.0 - end of the spline]</param>
        /// <returns>world space point</returns>
        public Vector3 GetPoint(float t)
        {
            var i = ToCurveIndex(ref t);
            return GetPoint(t, i);
        }


        /// <summary>
        ///     Get interpolated point (object space coordinates)
        /// </summary>
        /// <param name="t">[0.0 - beginning of the spline, 1.0 - end of the spline]</param>
        /// <returns>object space point</returns>
        public Vector3 GetPointLocal(float t)
        {
            var i = ToCurveIndex(ref t);
            return GetPointLocal(t, i);
        }

        /// <summary>
        ///     Get twist angle (degrees)
        /// </summary>
        /// <param name="t">[0.0 - beginning of the curve, 1.0 - end of the curve]</param>
        /// <param name="curveIndex">[0 to CurveCount-1]</param>
        /// <returns>twist angle in degrees</returns>
        public float GetTwist(float t, int curveIndex)
        {
            var current = m_settings[curveIndex].Twist;
            var next = m_settings[curveIndex + 1].Twist;

            var t1 = Mathf.Clamp01(current.T1);
            var t2 = Mathf.Clamp01(current.T2);

            if (t <= t1)
                t = 0.0f;
            else if (t >= t2)
                t = 1.0f;
            else
                t = Mathf.Clamp01((t - t1) / (t2 - t1));

            return Mathf.Lerp(current.Data, next.Data, t);
        }

        /// <summary>
        ///     Get twist angle (degrees)
        /// </summary>
        /// <param name="t">[0.0 - beginning of the spline, 1.0 - end of the spline]</param>
        /// <returns>twist angle in degrees</returns>
        public float GetTwist(float t)
        {
            var i = ToCurveIndex(ref t);
            return GetTwist(t, i);
        }

        /// <summary>
        ///     Get thickness scale factors ( initial thickess is (1, 1, 1) )
        /// </summary>
        /// <param name="t">[0.0 - beginning of the curve, 1.0 - end of the curve]</param>
        /// <param name="curveIndex">[0 to CurveCount-1]</param>
        /// <returns>thickness scale factors</returns>
        public Vector3 GetThickness(float t, int curveIndex)
        {
            var current = m_settings[curveIndex].Thickness;
            var next = m_settings[curveIndex + 1].Thickness;

            var t1 = Mathf.Clamp01(current.T1);
            var t2 = Mathf.Clamp01(current.T2);

            if (t <= t1)
                t = 0.0f;
            else if (t >= t2)
                t = 1.0f;
            else
                t = Mathf.Clamp01((t - t1) / (t2 - t1));

            return Vector3.Lerp(current.Data, next.Data, t);
        }


        /// <summary>
        ///     Get thickness scale factors ( initial thickess is (1, 1, 1) )
        /// </summary>
        /// <param name="t">[0.0 - beginning of the spline, 1.0 - end of the spline]</param>
        /// <returns>thickness scale factors</returns>
        public Vector3 GetThickness(float t)
        {
            var i = ToCurveIndex(ref t);
            return GetThickness(t, i);
        }

        /// <summary>
        ///     Get control point by index (world space coordinates)
        /// </summary>
        /// <param name="index">[0, ControlPointCount - 1]</param>
        /// <returns>control point coordinates in world space</returns>
        public Vector3 GetControlPoint(int index)
        {
            return transform.TransformPoint(m_points[index]);
        }

        /// <summary>
        ///     Get control point by index (object space coordinates)
        /// </summary>
        /// <param name="index">[0, ControlPointCount - 1]</param>
        /// <returns>control point coordinates in object space</returns>
        public Vector3 GetControlPointLocal(int index)
        {
            return m_points[index];
        }


        /// <summary>
        ///     Get control point settings by index
        /// </summary>
        /// <param name="index">[0, ControlPointCount - 1]</param>
        /// <returns>control point settings</returns>
        public ControlPointSetting GetSetting(int index)
        {
            return m_settings[(index + 1) / 3];
        }

        /// <summary>
        ///     Get branches by index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>SplineBranch array</returns>
        public SplineBranch[] GetBranches(int index)
        {
            return m_settings[(index + 1) / 3].Branches;
        }


        /// <summary>
        ///     Determine whether spline has branches connected to control point
        /// </summary>
        /// <param name="index">control point index</param>
        /// <returns>true if has branches</returns>
        public bool HasBranches(int index)
        {
            var settingIndex = (index + 1) / 3;
            if (settingIndex >= m_settings.Length || settingIndex < 0) return false;
            var setting = m_settings[settingIndex];
            if (setting.Branches == null) return false;
            return setting.Branches.Length > 0;
        }

        /// <summary>
        ///     Get Spline by branch
        /// </summary>
        /// <param name="branch">branch</param>
        /// <returns>SplineBase</returns>
        public SplineBase BranchToSpline(SplineBranch branch)
        {
            return m_branches[branch.SplineIndex];
        }

        /// <summary>
        ///     Get twist settings by index
        /// </summary>
        /// <param name="index">[0, ControlPointCount - 1]</param>
        /// <returns>twist settings</returns>
        public Twist GetTwist(int index)
        {
            return m_settings[(index + 1) / 3].Twist;
        }

        /// <summary>
        ///     Get thickness settings by index
        /// </summary>
        /// <param name="index">[0, ControlPointCount - 1]</param>
        /// <returns>thickness settings</returns>
        public Thickness GetThickness(int index)
        {
            return m_settings[(index + 1) / 3].Thickness;
        }

        /// <summary>
        ///     Set twist
        /// </summary>
        /// <param name="index">[0, ControlPointCount - 1]</param>
        /// <param name="twist">twist settings</param>
        public void SetTwist(int index, Twist twist)
        {
            SetValue(index, twist,
                (i, val, r) =>
                {
                    var settingIndex = (index + 1) / 3;
                    m_settings[settingIndex].Twist = val;
                },
                (i, val, branch, r) => { branch.SetTwist(i, val); },
                GetTwist);

            if (m_loop)
            {
                var settingIndex = (index + 1) / 3;
                if (settingIndex == m_settings.Length - 1)
                {
                    var settings = m_settings[m_settings.Length - 1];
                    m_settings[0] = new ControlPointSetting(settings.Twist, settings.Thickness, m_settings[0].Branches);
                }
                else if (settingIndex == 0)
                {
                    var settings = m_settings[0];
                    m_settings[m_settings.Length - 1] = new ControlPointSetting(settings.Twist, settings.Thickness,
                        m_settings[m_settings.Length - 1].Branches);
                }
            }

#if UNITY_EDITOR
            m_persistentVersions[0]++;
            OnVersionChanged();
#endif

            RaisControlPointTwistChanged(index);
            OnCurveChanged(index, Math.Max(0, (index - 1) / 3));
        }


        /// <summary>
        ///     Set thickness
        /// </summary>
        /// <param name="index">[0, ControlPointCount - 1]</param>
        /// <param name="thickness">thickness settings</param>
        public void SetThickness(int index, Thickness thickness)
        {
            SetValue(index, thickness,
                (i, val, r) =>
                {
                    var settingIndex = (index + 1) / 3;
                    m_settings[settingIndex].Thickness = val;
                },
                (i, val, branch, r) => { branch.SetThickness(i, val); },
                GetThickness);

            if (m_loop)
            {
                var settingIndex = (index + 1) / 3;
                if (settingIndex == m_settings.Length - 1)
                {
                    var settings = m_settings[m_settings.Length - 1];
                    m_settings[0] = new ControlPointSetting(settings.Twist, settings.Thickness, m_settings[0].Branches);
                }
                else if (settingIndex == 0)
                {
                    var settings = m_settings[0];
                    m_settings[m_settings.Length - 1] = new ControlPointSetting(settings.Twist, settings.Thickness,
                        m_settings[m_settings.Length - 1].Branches);
                }
            }

#if UNITY_EDITOR
            m_persistentVersions[0]++;
            OnVersionChanged();
#endif
            RaiseControlPointThicknessChanged(index);
            OnCurveChanged(index, Math.Max(0, (index - 1) / 3));
        }

        /// <summary>
        ///     Set Control Point (world space coordinates)
        /// </summary>
        /// <param name="index">[0, ControlPointCount - 1]</param>
        /// <param name="point">world space point</param>
        public bool SetControlPoint(int index, Vector3 point)
        {
            return SetControlPointLocal(index, transform.InverseTransformPoint(point));
        }

        private bool _SetControlPointUnchecked(int index, Vector3 point)
        {
            return _SetControlPointLocalUnchecked(index, transform.InverseTransformPoint(point));
        }


        /// <summary>
        ///     Set Control Point (object space coordinates)
        /// </summary>
        /// <param name="index">[0, ControlPointCount - 1]</param>
        /// <param name="point">object space point</param>
        public bool SetControlPointLocal(int index, Vector3 point)
        {
            if (IsControlPointLocked(index)) return false;

            return _SetControlPointLocalUnchecked(index, point);
        }

        private bool _SetControlPointLocalUnchecked(int index, Vector3 point)
        {
            if (index % 3 == 0)
            {
                var delta = point - m_points[index];
                if (m_loop)
                {
                    if (index == 0)
                    {
                        JustChangeControlPointValue(1, delta);
                        RaiseControlPointChanged(1);

                        JustChangeControlPointValue(m_points.Length - 2, delta);
                        RaiseControlPointChanged(m_points.Length - 2);

                        SetControlPointValue(m_points.Length - 1, point);
                        RaiseControlPointChanged(m_points.Length - 1);
                    }
                    else if (index == m_points.Length - 1)
                    {
                        SetControlPointValue(0, point);
                        RaiseControlPointChanged(0);

                        JustChangeControlPointValue(1, delta);
                        RaiseControlPointChanged(1);

                        JustChangeControlPointValue(index - 1, delta);
                        RaiseControlPointChanged(index - 1);
                    }
                    else
                    {
                        JustChangeControlPointValue(index - 1, delta);
                        RaiseControlPointChanged(index - 1);
                        JustChangeControlPointValue(index + 1, delta);
                        RaiseControlPointChanged(index + 1);
                    }
                }
                else
                {
                    if (index > 0)
                    {
                        JustChangeControlPointValue(index - 1, delta);
                        RaiseControlPointChanged(index - 1);
                    }

                    if (index + 1 < m_points.Length)
                    {
                        JustChangeControlPointValue(index + 1, delta);
                        RaiseControlPointChanged(index + 1);
                    }
                }
            }

            SetControlPointValue(index, point);
            RaiseControlPointChanged(index);


            EnforceMode(index);

#if UNITY_EDITOR
            m_persistentVersions[0]++;
            OnVersionChanged();
#endif

            OnCurveChanged(index, Math.Max(0, (index - 1) / 3));

            return true;
        }


        /// <summary>
        ///     Get control point mode
        /// </summary>
        /// <param name="index">[0, ControlPointCount - 1]</param>
        /// <returns>ControlPointMode</returns>
        public ControlPointMode GetControlPointMode(int index)
        {
            return m_modes[(index + 1) / 3];
        }


        /// <summary>
        ///     Set control point mode for all control points
        /// </summary>
        /// <param name="mode">ControlPointMode</param>
        public void SetControlPointMode(ControlPointMode mode)
        {
            SetControlPointModeRecursive(this, mode);
        }

        private void SetControlPointModeRecursive(SplineBase spline, ControlPointMode mode)
        {
            for (var i = 0; i <= spline.CurveCount; ++i) spline.SetControlPointMode(i * 3, mode);

            if (spline.Children != null)
                for (var i = 0; i < spline.Children.Length; ++i)
                    SetControlPointModeRecursive(spline.Children[i], mode);
        }

        /// <summary>
        ///     Set control point mode by index
        /// </summary>
        /// <param name="index">[0, ControlPointCount - 1]</param>
        /// <param name="mode">ControlPointMode</param>
        /// <param name="raiseCurveChanged">Notify derived class? (default true)</param>
        public void SetControlPointMode(int index, ControlPointMode mode, bool raiseCurveChanged = true)
        {
            SetControlPointModeValue(index, mode, raiseCurveChanged);

            var modeIndex = (index + 1) / 3;
            RaiseControlPointModeChanged(modeIndex);
            if (m_loop)
            {
                if (modeIndex == 0)
                {
                    SetControlPointModeValue(ControlPointCount - 1, mode, raiseCurveChanged);
                    RaiseControlPointModeChanged(m_modes.Length - 1);
                }
                else if (modeIndex == m_modes.Length - 1)
                {
                    SetControlPointModeValue(0, mode, raiseCurveChanged);
                    RaiseControlPointModeChanged(0);
                }
            }

            EnforceMode(index);

#if UNITY_EDITOR
            m_persistentVersions[0]++;
            OnVersionChanged();
#endif

            if (raiseCurveChanged) OnCurveChanged(index, Math.Max(0, (index - 1) / 3));
        }

        private void SetControlPointModeValue(int index, ControlPointMode mode, bool raiseCurveChanged)
        {
            var modeIndex = (index + 1) / 3;
            if (m_modes[modeIndex] == mode) return;

            m_modes[modeIndex] = mode;
            SetBranchControlPointModes(index, mode, raiseCurveChanged);
        }

        private void SetBranchControlPointModes(int index, ControlPointMode mode, bool raiseCurveChanged)
        {
            var modeIndex = (index + 1) / 3;
            var settings = m_settings[modeIndex];
            var connections = settings.Branches;
            if (connections != null)
                for (var i = 0; i < connections.Length; ++i)
                {
                    var connection = connections[i];
                    var connectedSpline = m_branches[connection.SplineIndex];
                    if (connectedSpline != null)
                    {
                        if (connection.Inbound)
                            connectedSpline.SetControlPointMode(connectedSpline.ControlPointCount - 1, mode,
                                raiseCurveChanged);
                        else
                            connectedSpline.SetControlPointMode(0, mode, raiseCurveChanged);
                    }
                }

            if (modeIndex == 0 && PrevSpline != null)
                PrevSpline.SetControlPointMode(PrevControlPointIndex, mode, raiseCurveChanged);

            if (modeIndex == m_settings.Length - 1 && NextSpline != null)
                NextSpline.SetControlPointMode(NextControlPointIndex, mode, raiseCurveChanged);
        }

        /// <summary>
        ///     Get velocity (world space coordinates)
        /// </summary>
        /// <param name="t">[0 - beginning of curve, 1 - end of curve]</param>
        /// <param name="curveIndex">[0, CurveCount - 1]</param>
        /// <returns>world space velocity vector</returns>
        public Vector3 GetVelocity(float t, int curveIndex)
        {
            var pointIndex = curveIndex * 3;
            return transform.TransformVector(CurveUtils.GetFirstDerivative(m_points[pointIndex],
                m_points[pointIndex + 1], m_points[pointIndex + 2], m_points[pointIndex + 3], t));

            //- transform.position;
        }

        /// <summary>
        ///     Get Velocity (world space coordinates)
        /// </summary>
        /// <param name="t">[0 - beginning of the spline, 1 - end of the spline]</param>
        /// <returns>world space velocity vector</returns>
        public Vector3 GetVelocity(float t)
        {
            int i;
            if (t >= 1f)
            {
                t = 1f;
                i = (m_points.Length - 1) / 3 - 1;
            }
            else
            {
                t = Mathf.Clamp01(t) * CurveCount;
                i = (int)t;
                t -= i;
            }

            return GetVelocity(t, i);
        }


        /// <summary>
        ///     Get direction (world space coordinates)
        /// </summary>
        /// <param name="t">[0 - beginning of curve, 1 - end of curve]</param>
        /// <param name="curveIndex">[0, CurveCount - 1]</param>
        /// <returns>world space direction vector</returns>
        public Vector3 GetDirection(float t, int curveIndex)
        {
            return GetVelocity(t, curveIndex).normalized;
        }


        /// <summary>
        ///     Get direction (world space coordinates)
        /// </summary>
        /// <param name="t">[0 - beginning of the spline, 1 - end of the spline]</param>
        /// <returns>world space direction vector</returns>
        public Vector3 GetDirection(float t)
        {
            return GetVelocity(t).normalized;
        }

        /// <summary>
        ///     Get SplineControlPoint components
        /// </summary>
        /// <returns>SplineControlPoint[]</returns>
        public virtual SplineControlPoint[] GetSplineControlPoints()
        {
            var ctrlPoints = new List<SplineControlPoint>(transform.childCount);
            foreach (Transform child in transform)
            {
                var ctrlPoint = child.GetComponent<SplineControlPoint>();
                if (ctrlPoint != null) ctrlPoints.Add(ctrlPoint);
            }

            return ctrlPoints.ToArray();
        }

        /// <summary>
        ///     Align branch with NextSpline
        /// </summary>
        public void AlignWithNextSpline()
        {
            if (NextSpline == null) return;


            if (m_nextControlPointIndex == 0)
                NextSpline.AlignWithBeginning(m_points, (m_nextControlPointIndex - 1) / 3, GetMag());
            else
                NextSpline.AlignWithBeginning(m_points, (m_nextControlPointIndex - 1) / 3, GetMag(), 1.0f);

            for (var i = 0; i < m_points.Length; ++i)
                m_points[i] = transform.InverseTransformPoint(NextSpline.transform.TransformPoint(m_points[i]));

            EnforceMode(ControlPointCount - 1);

#if UNITY_EDITOR
            m_persistentVersions[0]++;
            OnVersionChanged();
#endif
        }

        /// <summary>
        ///     Align branch with PrevSpline
        /// </summary>
        public void AlignWithPrevSpline()
        {
            if (PrevSpline == null) return;
            if (m_prevControlPointIndex == 0)
                PrevSpline.AlignWithEnding(m_points, (m_prevControlPointIndex - 1) / 3, GetMag(), 0.0f);
            else
                PrevSpline.AlignWithEnding(m_points, (m_prevControlPointIndex - 1) / 3, GetMag());

            for (var i = 0; i < m_points.Length; ++i)
                m_points[i] = transform.InverseTransformPoint(PrevSpline.transform.TransformPoint(m_points[i]));

            EnforceMode(0);

#if UNITY_EDITOR
            m_persistentVersions[0]++;
            OnVersionChanged();
#endif
        }

        public bool IsControlPointLocked(int index)
        {
            if (index >= 2 && index <= ControlPointCount - 3) return false;

            if (index % 3 != 0)
            {
                var isFree = GetControlPointMode(index) == ControlPointMode.Free;
                if (isFree) return false;
            }

            if (PrevSpline != null)
            {
                if (PrevControlPointIndex == PrevSpline.ControlPointCount - 1)
                {
                    var branches = PrevSpline.GetBranches(PrevControlPointIndex);
                    SplineBase branch = null;
                    for (var i = 0; i < branches.Length; ++i)
                        if (!branches[i].Inbound)
                            branch = PrevSpline.BranchToSpline(branches[i]);
                    if (branch == this) // show additional control point for first OUTBOUND branch only
                    {
                        if (index < 1) return true;
                        if (Loop)
                            if (index > ControlPointCount - 2)
                                return true;
                    }
                    else
                    {
                        if (index < 2) return true;
                        if (Loop)
                            if (index > ControlPointCount - 3)
                                return true;
                    }
                }
                else
                {
                    if (index < 2) return true;
                    if (Loop)
                        if (index > ControlPointCount - 3)
                            return true;
                }
            }

            if (NextSpline != null)
            {
                if (NextControlPointIndex == 0)
                {
                    var branches = NextSpline.GetBranches(NextControlPointIndex);
                    SplineBase branch = null;
                    for (var i = 0; i < branches.Length; ++i)
                        if (branches[i].Inbound)
                            branch = NextSpline.BranchToSpline(branches[i]);

                    if (branch == this) // show additional control point for first INBOUND branch only
                    {
                        if (index > ControlPointCount - 2) return true;
                        if (Loop)
                            if (index < 1)
                                return true;
                    }
                    else
                    {
                        if (index > ControlPointCount - 3) return true;
                        if (Loop)
                            if (index < 2)
                                return true;
                    }
                }
                else
                {
                    if (index > ControlPointCount - 3) return true;
                    if (Loop)
                        if (index < 2)
                            return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Set Branch
        /// </summary>
        /// <param name="branch"></param>
        /// <param name="connectionPointIndex"></param>
        /// <param name="isInbound"></param>
        public void SetBranch(SplineBase branch, int connectionPointIndex, bool isInbound)
        {
            if (branch == this) throw new InvalidOperationException("branch == this");

            if (branch.Loop) throw new InvalidOperationException("Unable to connect branch. Branch has loop");

            //Case when branch will be connected to the same point as the branch of the branch.
            //This will force branch of the branch to be collapsed to the single point with no degree of freedom
            //This will also causes stack overflow. We have to prevent this
            var branches = GetBranches(connectionPointIndex);
            var branchBranches = branch.GetBranches(isInbound ? branch.ControlPointCount - 1 : 0);
            if (branches != null && branchBranches != null)
                for (var i = 0; i < branches.Length; ++i)
                {
                    var spline = m_branches[branches[i].SplineIndex];
                    for (var j = 0; j < branchBranches.Length; ++j)
                    {
                        var branchSpline = branch.m_branches[branchBranches[j].SplineIndex];
                        if (branchSpline == spline) //this spline will be collapsed to single control point
                        {
                            //terminate;
                            Debug.LogError("Unable to connect branch. Connection will lead to illegal structure");
                            return;
                        }
                    }
                }


            connectionPointIndex = (connectionPointIndex + 1) / 3 * 3;
            var connectionPoint = GetControlPoint(connectionPointIndex);
            branch.transform.SetParent(transform, true);
            var branchConnectionPoint = branch.transform.InverseTransformPoint(connectionPoint);

            var thickness = GetThickness(connectionPointIndex);
            thickness.T1 = 0.0f;
            thickness.T2 = 1.0f;

            var twist = GetTwist(connectionPointIndex);
            twist.T1 = 0.0f;
            twist.T2 = 1.0f;

            if (isInbound)
            {
                branch.SetThickness(branch.ControlPointCount - 1, thickness);
                branch.SetTwist(branch.ControlPointCount - 1, twist);
                branch.SetControlPointValue(branch.ControlPointCount - 1, branchConnectionPoint);
                branch.RaiseControlPointChanged(branch.ControlPointCount - 1);

                var mode = GetControlPointMode(connectionPointIndex);
                if (mode == ControlPointMode.Free || (connectionPointIndex == 0 && mode != ControlPointMode.Mirrored))
                {
                    var toConnectionPoint =
                        branchConnectionPoint - branch.GetControlPointLocal(branch.ControlPointCount - 1);
                    branch.ChangeControlPointValue(branch.ControlPointCount - 2, toConnectionPoint);
                    branch.RaiseControlPointChanged(branch.ControlPointCount - 2);
                }
                else if (mode == ControlPointMode.Aligned)
                {
                    branch.SetControlPointValue(branch.ControlPointCount - 2,
                        branch.transform.InverseTransformPoint(GetControlPoint(connectionPointIndex - 1)));
                    branch.RaiseControlPointChanged(branch.ControlPointCount - 2);
                }
            }
            else
            {
                branch.SetThickness(0, thickness);
                branch.SetTwist(0, twist);
                branch.SetControlPointValue(0, branchConnectionPoint);
                branch.RaiseControlPointChanged(0);

                var mode = GetControlPointMode(connectionPointIndex);
                if (mode == ControlPointMode.Free ||
                    (connectionPointIndex == ControlPointCount - 1 && mode != ControlPointMode.Mirrored))
                {
                    var toConnectionPoint = branchConnectionPoint - branch.GetControlPointLocal(0);
                    branch.ChangeControlPointValue(1, toConnectionPoint);
                    branch.RaiseControlPointChanged(1);
                }
                else if (mode == ControlPointMode.Aligned)
                {
                    branch.SetControlPointValue(1,
                        branch.transform.InverseTransformPoint(GetControlPoint(connectionPointIndex + 1)));
                    branch.RaiseControlPointChanged(1);
                }
            }

            Reconnect(branch, connectionPointIndex, isInbound);
            if (isInbound)
                branch.SetControlPointMode(branch.ControlPointCount - 1, GetControlPointMode(connectionPointIndex));
            else
                branch.SetControlPointMode(0, GetControlPointMode(connectionPointIndex));

            if (IsSelected) branch.Select();

#if UNITY_EDITOR
            m_persistentVersions[0]++;
            OnVersionChanged();
#endif
        }

        private void Reconnect(SplineBase branch, int connectionPointIndex, bool isInbound)
        {
            if (isInbound)
            {
                if (branch.m_nextSpline != null) branch.m_nextSpline.Disconnect(branch, isInbound);

                branch.m_nextSpline = this;
                branch.m_nextControlPointIndex = connectionPointIndex;
            }
            else
            {
                if (branch.m_prevSpline != null) branch.m_prevSpline.Disconnect(branch, isInbound);
                branch.m_prevSpline = this;
                branch.m_prevControlPointIndex = connectionPointIndex;
            }

            Connect(branch, connectionPointIndex, isInbound);

            EnforceBranchModes(connectionPointIndex);
        }

        private void Connect(SplineBase branch, int connectionPointIndex, bool isInbound)
        {
            var splineIndex = Array.IndexOf(m_branches, branch);
            if (splineIndex < 0)
            {
                Array.Resize(ref m_branches, m_branches.Length + 1);

                splineIndex = m_branches.Length - 1;
                m_branches[splineIndex] = branch;
            }

            var settingIndex = (connectionPointIndex + 1) / 3;
            var settings = m_settings[settingIndex];

            if (settings.Branches == null)
                settings.Branches = new SplineBranch[1];
            else
                Array.Resize(ref settings.Branches, settings.Branches.Length + 1);

            settings.Branches[settings.Branches.Length - 1] = new SplineBranch(splineIndex, isInbound);
            m_settings[settingIndex] = settings;

            RaiseControlPointConnectionChanged(connectionPointIndex);
            if (isInbound)
            {
                branch.RaiseControlPointConnectionChanged(branch.ControlPointCount - 1);
                branch.RaiseControlPointConnectionChanged(branch.ControlPointCount - 2);
            }
            else
            {
                branch.RaiseControlPointConnectionChanged(0);
                branch.RaiseControlPointConnectionChanged(1);
            }
        }


        /// <summary>
        ///     Disconnect all branches from point by index
        /// </summary>
        /// <param name="index">point index</param>
        public void Disconnect(int index)
        {
            var branches = GetBranches(index);
            if (branches == null || branches.Length == 0) return;

            for (var i = branches.Length - 1; i >= 0; i--)
            {
                var branch = branches[i];
                var spline = m_branches[branch.SplineIndex];
                Disconnect(spline, branch.Inbound);
            }
        }

        /// <summary>
        ///     Disconnect branch
        /// </summary>
        /// <param name="spline"></param>
        public void Disconnect(SplineBase spline)
        {
            Disconnect(spline, true);
            Disconnect(spline, false);
        }

        /// <summary>
        ///     Disconnect branch
        /// </summary>
        /// <param name="spline"></param>
        public void Disconnect(SplineBase branch, bool isInbound)
        {
            var splineIndex = Array.IndexOf(m_branches, branch);
            if (splineIndex < 0) return;

            int settingsIndex;
            if (isInbound)
            {
                settingsIndex = (branch.m_nextControlPointIndex + 1) / 3;
                branch.m_nextSpline = null;
                branch.m_nextControlPointIndex = -1;
            }
            else
            {
                settingsIndex = (branch.m_prevControlPointIndex + 1) / 3;
                branch.m_prevSpline = null;
                branch.m_prevControlPointIndex = -1;
            }

            if (settingsIndex >= m_settings.Length) return;

            var settings = m_settings[settingsIndex];
            var connectionIndex = -1;
            for (var i = 0; i < settings.Branches.Length; ++i)
            {
                var connection = settings.Branches[i];
                if (connection.SplineIndex == splineIndex)
                    if (connection.Inbound == isInbound)
                        connectionIndex = i;
            }

            if (connectionIndex >= 0)
            {
                for (var i = connectionIndex; i < settings.Branches.Length - 1; i++)
                    settings.Branches[i] = settings.Branches[i + 1];

                Array.Resize(ref settings.Branches, settings.Branches.Length - 1);
                m_settings[settingsIndex] = settings;
            }

            if (branch.m_nextSpline == null && branch.m_prevSpline == null)
            {
                for (var i = splineIndex; i < m_branches.Length - 1; ++i) m_branches[i] = m_branches[i + 1];
                Array.Resize(ref m_branches, m_branches.Length - 1);
                CleanupSplineConnections(splineIndex);
            }


#if UNITY_EDITOR
            m_persistentVersions[0]++;
            OnVersionChanged();
#endif

            RaiseControlPointConnectionChanged(settingsIndex * 3);
            if (isInbound)
            {
                branch.RaiseControlPointConnectionChanged(branch.ControlPointCount - 1);
                branch.RaiseControlPointConnectionChanged(branch.ControlPointCount - 2);
            }
            else
            {
                branch.RaiseControlPointConnectionChanged(0);
                branch.RaiseControlPointConnectionChanged(1);
            }
        }

        private void UpdateChildrenAndParent()
        {
            if (transform.parent != null)
                m_parent = transform.parent.GetComponentInParent<SplineBase>();
            else
                m_parent = null;

            var children = new List<SplineBase>();
            foreach (Transform childTransform in transform)
            {
                var childSpline = childTransform.GetComponent<SplineBase>();
                if (childSpline != null) children.Add(childSpline);
            }

            m_children = children.ToArray();
        }


        private void ShiftConnectionIndices(int settingIndex, int offset)
        {
            for (var i = 0; i < m_branches.Length; ++i)
            {
                var branch = m_branches[i];
                if (branch.PrevSpline == this)
                    if (branch.m_prevControlPointIndex >= settingIndex * 3)
                        branch.m_prevControlPointIndex += offset;
                if (branch.NextSpline == this)
                    if (branch.m_nextControlPointIndex >= settingIndex * 3)
                        branch.m_nextControlPointIndex += offset;
            }
        }

        private void CleanupSplineConnections(int splineIndex)
        {
            for (var i = 0; i < m_settings.Length; ++i)
            {
                var settings = m_settings[i];
                if (settings.Branches != null)
                {
                    for (var j = 0; j < settings.Branches.Length; ++j)
                    {
                        var connection = settings.Branches[j];
                        if (connection.SplineIndex == splineIndex)
                            throw new InvalidOperationException(
                                "connection.SplineIndex == splineIndex. SplineConnection with index " + splineIndex +
                                " should be removed");

                        if (connection.SplineIndex > splineIndex)
                        {
                            connection.SplineIndex--;
                            settings.Branches[j] = connection;
                        }
                    }

                    m_settings[i] = settings;
                }
            }
        }

        /// <summary>
        ///     Smooth spline
        ///     https://www.particleincell.com/2012/bezier-splines/
        /// </summary>
        public void Smooth()
        {
            var offset = m_points[0];
            ShiftPoints(-offset);
            var n = m_points.Length / 3;

            var a = new float[n];
            var b = new float[n];
            var c = new float[n];
            var r = new Vector3[n];

            a[0] = 0.0f;
            b[0] = 2.0f;
            c[0] = 1.0f;
            r[0] = m_points[0] + 2 * m_points[3];

            for (var i = 1; i < n - 1; i++)
            {
                a[i] = 1.0f;
                b[i] = 4.0f;
                c[i] = 1.0f;
                r[i] = 4 * m_points[i * 3] + 2 * m_points[(i + 1) * 3];
            }

            a[n - 1] = 2.0f;
            b[n - 1] = 7.0f;
            c[n - 1] = 0.0f;
            r[n - 1] = 8 * m_points[(n - 1) * 3] + m_points[n * 3];

            for (var i = 1; i < n; i++)
            {
                var m = a[i] / b[i - 1];
                b[i] = b[i] - m * c[i - 1];
                r[i] = r[i] - m * r[i - 1];
            }

            m_points[(n - 1) * 3 + 1] = r[n - 1] / b[n - 1];
            for (var i = n - 2; i >= 0; --i) m_points[i * 3 + 1] = (r[i] - c[i] * m_points[(i + 1) * 3 + 1]) / b[i];

            for (var i = 0; i < n - 1; i++)
                m_points[i * 3 + 2] = 2.0f * m_points[(i + 1) * 3] - m_points[(i + 1) * 3 + 1];

            m_points[(n - 1) * 3 + 2] = 0.5f * (m_points[n * 3] + m_points[(n - 1) * 3 + 1]);
            ShiftPoints(offset);

            if (Loop) EnforceMode(m_points.Length - 2);

            SyncCtrlPoints();
            OnCurveChanged();

            if (Children != null)
                for (var i = 0; i < Children.Length; ++i)
                {
                    var child = Children[i];
                    child.Smooth();
                }

            EnforceModeRecursive();
        }

        private void EnforceModeRecursive()
        {
            EnforceMode(1);
            if (Children != null)
                for (var i = 0; i < Children.Length; ++i)
                {
                    var child = Children[i];
                    child.EnforceModeRecursive();
                }
        }

        private void ShiftPoints(Vector3 offset)
        {
            for (var i = 0; i < m_points.Length; ++i) m_points[i] += offset;
        }

        /// <summary>
        ///     Calculate distance between first and last control points of curve
        /// </summary>
        /// <param name="curveIndex">[0, CurveCount - 1]</param>
        /// <returns>distance in world space</returns>
        public float EvalDistance(int curveIndex)
        {
            var prev = GetPoint(0.0f, curveIndex);
            var next = GetPoint(1.0f, curveIndex);

            return (next - prev).magnitude;
        }

        /// <summary>
        ///     Calclulate distance between first and last control points of spline
        /// </summary>
        /// <returns>distance in world space</returns>
        public float EvalDistance()
        {
            var prev = GetPoint(0.0f);
            var next = GetPoint(1.0f);

            return (next - prev).magnitude;
        }

        /// <summary>
        ///     Approximately calculate curve length
        /// </summary>
        /// <param name="curveIndex">[0, CurveCount - 1]</param>
        /// <param name="steps">[1, reasonably big value] the bigger value the greater precision (keep as low as possible)</param>
        /// <returns>length in world space</returns>
        public float EvalCurveLength(int curveIndex, int steps = 4)
        {
            if (steps < 1) steps = 1;

            var length = 0.0f;
            var prevPoint = GetPoint(0.0f, curveIndex);
            for (var i = 1; i <= steps; ++i)
            {
                float t = i;
                t /= 3;
                var point = GetPoint(t, curveIndex);
                length += (point - prevPoint).magnitude;
                prevPoint = point;
            }

            return length;
        }

        /// <summary>
        ///     Approximately calculate spline length
        /// </summary>
        /// <param name="steps">[1, reasonably big value] the bigger value the greater precision (keep as low as possible)</param>
        /// <returns></returns>
        public float EvalSplineLength(int steps = 4)
        {
            if (steps < 1) steps = 1;

            var length = 0.0f;
            for (var i = 0; i < CurveCount; ++i) length += EvalCurveLength(i, steps);
            return length;
        }

        /// <summary>
        ///     Save spline to SplineSnapshot struct
        /// </summary>
        /// <returns>SplineSnapshot</returns>
        public virtual SplineSnapshot Save()
        {
            return new SplineSnapshot(m_points, m_settings, m_modes, m_loop);
        }

        /// <summary>
        ///     Load spline from SplineSnapshot struct
        /// </summary>
        public virtual void Load(SplineSnapshot snapshot)
        {
            LoadSpline(snapshot);
        }

        protected void LoadSpline(SplineSnapshot settings)
        {
            m_points = settings.Points;
            m_settings = settings.ControlPointSettings;
            m_modes = settings.Modes;
            m_loop = settings.Loop;
            SyncCtrlPoints();
        }

        /// <summary>
        ///     Set group of control points
        /// </summary>
        /// <param name="curveIndex">first curve index</param>
        protected void SetPoints(int curveIndex, Vector3[] points, ControlPointMode mode, bool raiseCurveChanged)
        {
            var index = curveIndex * 3;

            for (var i = 0; i < points.Length; ++i)
            {
                SetControlPointValue(index, points[i]);
                RaiseControlPointChanged(index);

                SetControlPointMode(index, mode, raiseCurveChanged);
                index++;
            }

            EnforceMode(index);

#if UNITY_EDITOR
            m_persistentVersions[0]++;
            OnVersionChanged();
#endif

            if (raiseCurveChanged) OnCurveChanged(index, Math.Max(0, (index - 1) / 3));
        }


        private void JustChangeControlPointValue(int index, Vector3 delta)
        {
            m_points[index] += delta;
        }

        private void ChangeControlPointValue(int index, Vector3 delta)
        {
            SetControlPointValue(index, m_points[index] + delta);
        }

        private void JustSetControlPointValue(int index, Vector3 point)
        {
            m_points[index] = point;
        }

        private void SetControlPointValue(int index, Vector3 point)
        {
            if (m_points[index] == point) return;

            m_points[index] = point;
            SetBranchControlPoints(index, point);
        }

        private void SetBranchControlPoints(int index, Vector3 point)
        {
            var settingIndex = (index + 1) / 3;
            var knotIndex = settingIndex * 3;
            var settings = m_settings[settingIndex];
            var connections = settings.Branches;
            if (connections != null)
                for (var i = 0; i < connections.Length; ++i)
                {
                    var connection = connections[i];
                    var connectedSpline = m_branches[connection.SplineIndex];
                    if (connectedSpline != null)
                    {
                        if (connection.Inbound)
                        {
                            if ((connectedSpline.m_nextControlPointIndex + 1) / 3 == settingIndex)
                            {
                                if (index == knotIndex) // mid (green) control point
                                    connectedSpline._SetControlPointUnchecked(connectedSpline.ControlPointCount - 1,
                                        transform.TransformPoint(point));
                                else if (index == knotIndex - 1) //pref (red) control point
                                    if (GetControlPointMode(index) != ControlPointMode.Free)
                                        connectedSpline._SetControlPointUnchecked(connectedSpline.ControlPointCount - 2,
                                            transform.TransformPoint(point));
                            }
                        }
                        else
                        {
                            if ((connectedSpline.m_prevControlPointIndex + 1) / 3 == settingIndex)
                            {
                                if (index == knotIndex) // mid (green) control point
                                    connectedSpline._SetControlPointUnchecked(0, transform.TransformPoint(point));
                                else if (index == knotIndex + 1) // next (red) control point
                                    if (GetControlPointMode(index) != ControlPointMode.Free)
                                        connectedSpline._SetControlPointUnchecked(1, transform.TransformPoint(point));
                            }
                        }
                    }
                }
        }

        /// <summary>
        ///     Update control point (possibly adjacent point) according to it's control point mode
        /// </summary>
        /// <param name="index">[0, ControlPointCount - 1]</param>
        private void EnforceMode(int index)
        {
            var modeIndex = (index + 1) / 3;
            var mode = m_modes[modeIndex];

            var isEdge = modeIndex == 0 || modeIndex == m_modes.Length - 1;

            if (mode == ControlPointMode.Free || (!m_loop && isEdge))
            {
                if (isEdge) EnforceBranchModes(index);
                return;
            }

            var middleIndex = modeIndex * 3;
            int fixedIndex, enforcedIndex;
            if (index <= middleIndex)
            {
                fixedIndex = middleIndex - 1;
                if (fixedIndex < 0) fixedIndex = m_points.Length - 2;
                enforcedIndex = middleIndex + 1;
                if (enforcedIndex >= m_points.Length) enforcedIndex = 1;
            }
            else
            {
                fixedIndex = middleIndex + 1;
                if (fixedIndex >= m_points.Length) fixedIndex = 1;
                enforcedIndex = middleIndex - 1;
                if (enforcedIndex < 0) enforcedIndex = m_points.Length - 2;
            }

            var middle = m_points[middleIndex];
            var enforcedTangent = middle - m_points[fixedIndex];
            if (mode == ControlPointMode.Aligned)
                enforcedTangent = enforcedTangent.normalized * Vector3.Distance(middle, m_points[enforcedIndex]);

            SetControlPointValue(enforcedIndex, middle + enforcedTangent);
            RaiseControlPointChanged(enforcedIndex);

            if (isEdge)
            {
                EnforceBranchModes(index);
                EnforceBranchModes(enforcedIndex);
            }
        }

        private void EnforceBranchModes(int index)
        {
            var modeIndex = (index + 1) / 3;
            var mode = m_modes[modeIndex];
            if (mode == ControlPointMode.Free) return;

            var setting = m_settings[modeIndex];
            if (setting.Branches == null) return;

            var middleIndex = modeIndex * 3;
            for (var i = 0; i < setting.Branches.Length; ++i)
            {
                var connection = setting.Branches[i];
                var branch = m_branches[connection.SplineIndex];

                int fixedIndex;
                int enforcedIndex;
                if (connection.Inbound)
                {
                    fixedIndex = middleIndex + 1;
                    enforcedIndex = branch.ControlPointCount - 2;
                }
                else
                {
                    fixedIndex = middleIndex - 1;
                    enforcedIndex = 1;
                }

                EnforceBranchMode(mode, middleIndex, branch, fixedIndex, enforcedIndex);
            }

            if (modeIndex == 0)
            {
                if (PrevSpline != null)
                    EnforceBranchMode(mode, middleIndex, PrevSpline, middleIndex + 1, m_prevControlPointIndex - 1);
                else if (NextSpline != null)
                    if (Loop)
                    {
                        var modifiedMidIndex = (m_modes.Length - 1) * 3;
                        EnforceBranchMode(mode, modifiedMidIndex, NextSpline, modifiedMidIndex - 1,
                            m_nextControlPointIndex + 1);
                    }
            }
            else if (modeIndex == m_modes.Length - 1)
            {
                if (NextSpline != null)
                    EnforceBranchMode(mode, middleIndex, NextSpline, middleIndex - 1, m_nextControlPointIndex + 1);
                else if (PrevSpline != null)
                    if (Loop)
                    {
                        var modifiedMidIndex = 0;
                        EnforceBranchMode(mode, modifiedMidIndex, PrevSpline, modifiedMidIndex + 1,
                            m_prevControlPointIndex - 1);
                    }
            }
        }

        private void EnforceBranchMode(ControlPointMode mode, int middleIndex, SplineBase branch, int fixedIndex,
            int enforcedIndex)
        {
            if (fixedIndex < 0 || fixedIndex >= m_points.Length)
            {
                if (fixedIndex < 0)
                    fixedIndex = 1;
                else
                    fixedIndex = m_points.Length - 2;
                var fixedPoint = branch.transform.InverseTransformPoint(transform.TransformPoint(m_points[fixedIndex]));
                if (branch.m_points[enforcedIndex] != fixedPoint)
                    branch._SetControlPointLocalUnchecked(enforcedIndex, fixedPoint);
                return;
            }

            if (enforcedIndex < 0 || enforcedIndex >= branch.m_points.Length)
            {
                if (enforcedIndex < 0)
                    enforcedIndex = 1;
                else
                    enforcedIndex = branch.m_points.Length - 2;

                var fixedPoint = branch.transform.InverseTransformPoint(transform.TransformPoint(m_points[fixedIndex]));
                if (branch.m_points[enforcedIndex] != fixedPoint)
                    branch._SetControlPointLocalUnchecked(enforcedIndex, fixedPoint);
                return;
            }

            var middle = m_points[middleIndex];
            var enforcedTangent = middle - m_points[fixedIndex];
            if (mode == ControlPointMode.Aligned)
            {
                var branchPoint =
                    transform.InverseTransformPoint(branch.transform.TransformPoint(branch.m_points[enforcedIndex]));
                enforcedTangent = enforcedTangent.normalized * Vector3.Distance(middle, branchPoint);
            }

            var point = branch.transform.InverseTransformPoint(transform.TransformPoint(middle + enforcedTangent));
            if (branch.m_points[enforcedIndex] != point) branch._SetControlPointLocalUnchecked(enforcedIndex, point);
        }

        /// <summary>
        ///     Align curve to a straight line
        /// </summary>
        /// <param name="curveIndex">[0, CurveCount-1]</param>
        /// <param name="length">desired length</param>
        /// <param name="toLast">use direction of last control point? (default true)</param>
        protected void AlignCurve(int curveIndex, float length, bool toLast = true)
        {
            var firstPointIndex = curveIndex * 3;
            var lastPointIndex = firstPointIndex + 3;
            var lastPoint = m_points[lastPointIndex];
            var firstPoint = m_points[firstPointIndex];

            Vector3 dir;
            if (toLast)
                dir = transform.InverseTransformDirection(GetDirection(1.0f, curveIndex));
            else
                dir = transform.InverseTransformDirection(GetDirection(0.0f, curveIndex));

            if (toLast)
            {
                for (var i = lastPointIndex - 1; i >= firstPointIndex; --i)
                {
                    lastPoint -= dir * length / 3;
                    SetControlPointValue(i, lastPoint);
                    RaiseControlPointChanged(i);
                }

                var offset = firstPoint - m_points[firstPointIndex];
                for (var i = firstPointIndex - 1; i >= 0; i--)
                {
                    ChangeControlPointValue(i, -offset);
                    RaiseControlPointChanged(i);
                }
            }
            else
            {
                for (var i = firstPointIndex + 1; i <= lastPointIndex; ++i)
                {
                    firstPoint += dir * length / 3;
                    SetControlPointValue(i, firstPoint);
                    RaiseControlPointChanged(i);
                }

                var offset = lastPoint - m_points[lastPointIndex];
                for (var i = lastPointIndex + 1; i < m_points.Length; i++)
                {
                    ChangeControlPointValue(i, -offset);
                    RaiseControlPointChanged(i);
                }
            }

            EnforceMode(firstPointIndex - 1);
            EnforceMode(lastPointIndex + 1);
        }

        /// <summary>
        ///     Remove curve by index
        /// </summary>
        /// <param name="curveIndex">[0, CurveCount - 1]</param>
        /// <returns></returns>
        protected bool RemoveCurve(int curveIndex)
        {
            if (m_points.Length <= 4) return false;

            if (curveIndex >= CurveCount || curveIndex < 0) throw new ArgumentOutOfRangeException("curveIndex");

            if (curveIndex == 0)
            {
                if (m_prevSpline != null) m_prevSpline.Disconnect(this, false);
            }
            else if (curveIndex == CurveCount - 1)
            {
                if (m_nextSpline != null) m_nextSpline.Disconnect(this, true);
            }

            var pointIndex = curveIndex * 3;
            var enforceMode = true;
            if (curveIndex == CurveCount - 1)
            {
                enforceMode = false;
                pointIndex += 3;
            }

            for (var i = pointIndex; i < m_points.Length - 3; ++i) JustSetControlPointValue(i, m_points[i + 3]);

            if (curveIndex == 0)
            {
                Disconnect(0);
                ShiftConnectionIndices(0, -3);
            }

            if (curveIndex == CurveCount - 1)
            {
                var settingIndex = curveIndex + 1;
                Disconnect(settingIndex * 3);
                // ShiftConnectionIndices(settingIndex * 3, -3);
            }
            else
            {
                var settingIndex = curveIndex;
                Disconnect(settingIndex * 3);
                ShiftConnectionIndices(settingIndex * 3, -3);
            }


            var modeIndex = (pointIndex + 1) / 3;
            for (var i = modeIndex; i < m_modes.Length - 1; ++i)
            {
                m_settings[i] = m_settings[i + 1];
                m_modes[i] = m_modes[i + 1];
                RaiseControlPointModeChanged(i);
            }

            Array.Resize(ref m_points, m_points.Length - 3);
            Array.Resize(ref m_settings, m_settings.Length - 1);
            Array.Resize(ref m_modes, m_modes.Length - 1);

            if (enforceMode) EnforceMode(pointIndex + 1);

            if (m_loop)
            {
                SetControlPointValue(m_points.Length - 1, m_points[0]);
                var setting = m_settings[m_settings.Length - 1];
                m_settings[0] = new ControlPointSetting(setting.Twist, setting.Thickness, m_settings[0].Branches);
                m_modes[m_modes.Length - 1] = m_modes[0];
                RaiseControlPointModeChanged(m_modes.Length - 1);
                EnforceMode(1);
            }

            SyncCtrlPoints();
            return true;
        }


        /// <summary>
        ///     Subdivide spline
        /// </summary>
        /// <param name="firstCurveIndex">[0, CurveCount - 1]</param>
        /// <param name="lastCurveIndex">[firstCurveIndex, CurveCount - 1]</param>
        /// <param name="curveCount">curve count</param>
        protected void Subdivide(int firstCurveIndex, int lastCurveIndex, int curveCount)
        {
            var firstPointIndex = firstCurveIndex * 3;
            var lastPointIndex = lastCurveIndex * 3 + 3;
            var pointsLength = m_points.Length;

            var midPointsCount = lastPointIndex - firstPointIndex - 1;
            var newMidPointsCount = curveCount * 3 - 1;
            var deltaPoints = newMidPointsCount - midPointsCount;

            var midPoints = new Vector3[newMidPointsCount];
            var firstPoint = m_points[firstPointIndex];
            var lastPoint = m_points[lastPointIndex];

            var setting = m_settings[(firstPointIndex + 1) / 3];
            var midPointSetting = new ControlPointSetting(setting.Twist, setting.Thickness);
            var midMode = m_modes[(firstPointIndex + 1) / 3];

            var deltaT = 1.0f / (newMidPointsCount + 1);
            var t = 0.0f;

            for (var i = 0; i < newMidPointsCount; ++i)
            {
                t += deltaT;
                midPoints[i] = Vector3.Lerp(firstPoint, lastPoint, t);
            }

            if (deltaPoints > 0)
            {
                Array.Resize(ref m_points, m_points.Length + deltaPoints);
                Array.Resize(ref m_settings, m_settings.Length + deltaPoints / 3);
                Array.Resize(ref m_modes, m_modes.Length + deltaPoints / 3);

                for (var i = pointsLength - 1; i >= lastPointIndex; i--)
                    SetControlPointValue(i + deltaPoints, m_points[i]);

                for (var i = pointsLength / 3; i >= (lastPointIndex + 1) / 3; i--)
                {
                    m_settings[i + deltaPoints / 3] = m_settings[i];
                    m_modes[i + deltaPoints / 3] = m_modes[i];
                    RaiseControlPointModeChanged(i + deltaPoints / 3);
                }
            }
            else if (deltaPoints < 0)
            {
                for (var i = lastPointIndex; i < pointsLength; i++) SetControlPointValue(i + deltaPoints, m_points[i]);

                for (var i = (lastPointIndex + 1) / 3; i < (pointsLength + 1) / 3; i++)
                {
                    m_settings[i + deltaPoints / 3] = m_settings[i];
                    m_modes[i + deltaPoints / 3] = m_modes[i];
                    RaiseControlPointModeChanged(i + deltaPoints / 3);
                }

                Array.Resize(ref m_points, m_points.Length + deltaPoints);
                Array.Resize(ref m_settings, m_settings.Length + deltaPoints / 3);
                Array.Resize(ref m_modes, m_modes.Length + deltaPoints / 3);
            }

            for (var i = 0; i < newMidPointsCount; ++i) SetControlPointValue(firstPointIndex + i + 1, midPoints[i]);
            for (var i = 0; i < newMidPointsCount / 3; ++i)
            {
                m_settings[(firstPointIndex + 1) / 3 + i + 1] = midPointSetting;
                m_modes[(firstPointIndex + 1) / 3 + i + 1] = midMode;
                RaiseControlPointModeChanged((firstPointIndex + 1) / 3 + i + 1);
            }

            var prevPointIndex = firstPointIndex - 1;
            var nextPointIndex = firstPointIndex + newMidPointsCount + 2;
            if (m_loop)
            {
                if (prevPointIndex == -1) prevPointIndex = m_points.Length - 1;
                if (nextPointIndex == m_points.Length) nextPointIndex = 0;
            }

            if (nextPointIndex < m_points.Length) EnforceMode(nextPointIndex);

            if (prevPointIndex >= 0) EnforceMode(prevPointIndex);

            SyncCtrlPoints();
        }

        /// <summary>
        ///     InsertCurve
        /// </summary>
        /// <param name="points">control points to insert</param>
        /// <param name="setting">control point setting (twist angle and thickness) applied to inserted control points</param>
        /// <param name="mode">controp point mode applied to inserted control points</param>
        /// <param name="curveIndex">insert before curveIndex</param>
        /// <param name="length">length of curve (distance between first and last control points)</param>
        /// <param name="enforceNeighbour"></param>
        /// <param name="shrinkPreceding">
        ///     false - shift all controls before newly inserted control points using -(GetDirection(0.0f, curveIndex) * length)
        ///     vector
        ///     true - shrink preceding curve
        /// </param>
        private void InsertCurve(Vector3[] points, ControlPointSetting setting, ControlPointMode mode, int curveIndex,
            float length, bool enforceNeighbour, bool shrinkPreceding)
        {
            if (curveIndex == 0 && shrinkPreceding)
            {
                if (Loop)
                    curveIndex = CurveCount;
                else
                    curveIndex = 1;
            }

            var pointIndex = curveIndex * 3;
            var prevCurveIndex = curveIndex - 1;
            var prevPointIndex = prevCurveIndex * 3;

            Array.Resize(ref m_points, m_points.Length + points.Length);
            Array.Resize(ref m_settings, m_settings.Length + points.Length / 3);
            Array.Resize(ref m_modes, m_modes.Length + points.Length / 3);

            var modeIndex = (pointIndex + 1) / 3;
            ShiftConnectionIndices(modeIndex, points.Length);

            for (var i = m_modes.Length - 1; i >= modeIndex + points.Length / 3; --i)
            {
                m_settings[i] = m_settings[i - points.Length / 3];
                m_modes[i] = m_modes[i - points.Length / 3];
            }

            for (var i = m_points.Length - 1; i >= pointIndex + points.Length; --i)
                JustSetControlPointValue(i, m_points[i - points.Length]);

            if (shrinkPreceding)
            {
                float count = points.Length + 3;
                for (var i = points.Length - 1; i >= 0; i--) points[i] = GetPointLocal((i + 4) / count, prevCurveIndex);

                var shrinkedPoints = new Vector3[3];
                for (var i = 2; i >= 0; i--) shrinkedPoints[i] = GetPointLocal((i + 1) / count, prevCurveIndex);

                for (var i = pointIndex; i >= pointIndex - 2; i--)
                    SetControlPointValue(i, shrinkedPoints[2 + i - pointIndex]);
            }

            for (var i = m_modes.Length - 1; i >= modeIndex + points.Length / 3; --i) RaiseControlPointModeChanged(i);

            for (var i = modeIndex; i < modeIndex + points.Length / 3; ++i)
            {
                m_settings[i] = setting;
                m_modes[i] = mode;
            }

            if (shrinkPreceding)
            {
                for (var i = pointIndex; i < pointIndex + points.Length; ++i)
                    SetControlPointValue(i + 1, points[i - pointIndex]);
            }
            else
            {
                for (var i = pointIndex; i < pointIndex + points.Length; ++i)
                    SetControlPointValue(i, points[i - pointIndex]);

                var dir = transform.InverseTransformDirection(GetDirection(0.0f, curveIndex));
                for (var i = pointIndex - 1; i >= 0; i--) ChangeControlPointValue(i, -dir * length);
            }

            for (var i = modeIndex; i < modeIndex + points.Length / 3; ++i) RaiseControlPointModeChanged(i);

            if (shrinkPreceding)
            {
                EnforceMode(pointIndex + points.Length + 1);
                EnforceMode(pointIndex - 1);
                EnforceMode(prevPointIndex - 1);
            }
            else
            {
                if (enforceNeighbour)
                    EnforceMode(pointIndex + points.Length + 1);
                else
                    EnforceMode(pointIndex + points.Length - 1);
            }


            if (m_loop)
            {
                var copySetting = m_settings[m_settings.Length - 1];
                m_settings[0] =
                    new ControlPointSetting(copySetting.Twist, copySetting.Thickness, m_settings[0].Branches);
                m_modes[m_modes.Length - 1] = m_modes[0];
                SetControlPointValue(m_points.Length - 1, m_points[0]);
                RaiseControlPointModeChanged(m_modes.Length - 1);
                EnforceMode(1);
            }

            SyncCtrlPoints();
        }

        protected void PrependCurve(Vector3[] points, int curveIndex, float length, bool enforceNeighbour,
            bool shrinkPreceding)
        {
            if (m_prevSpline != null && curveIndex == 0)
                throw new InvalidOperationException(
                    "Can't prepend curve to the connected end of the spline. Previous spline " + m_prevSpline.name);

            var setting = GetSetting(curveIndex * 3);
            setting.Branches = new SplineBranch[0];
            InsertCurve(points, setting, GetControlPointMode(curveIndex * 3), curveIndex, length, enforceNeighbour,
                shrinkPreceding);
        }

        protected void AppendCurve(Vector3[] points, bool enforceNeighbour)
        {
            var setting = GetSetting(m_points.Length - 1);
            AppendCurve(points, new ControlPointSetting(setting.Twist, setting.Thickness),
                GetControlPointMode(m_points.Length - 1), enforceNeighbour);
        }

        private void AppendCurve(Vector3[] points, ControlPointSetting setting, ControlPointMode mode,
            bool enforceNeighbour)
        {
            if (m_nextSpline != null)
                throw new InvalidOperationException(
                    "Can't append curve to the connected end of the spline. Next spline " + m_nextSpline.name);

            Array.Resize(ref m_points, m_points.Length + points.Length);
            Array.Resize(ref m_settings, m_settings.Length + points.Length / 3);
            Array.Resize(ref m_modes, m_modes.Length + points.Length / 3);

            for (var i = 0; i < points.Length / 3; i++)
            {
                m_settings[m_settings.Length - points.Length / 3 + i] = setting;
                m_modes[m_modes.Length - points.Length / 3 + i] = mode;
            }

            for (var i = 0; i < points.Length; i++)
                SetControlPointValue(m_points.Length - points.Length + i, points[i]);

            for (var i = 0; i < points.Length / 3; i++)
                RaiseControlPointModeChanged(m_modes.Length - points.Length / 3 + i);

            if (enforceNeighbour)
                EnforceMode(m_points.Length - points.Length - 2);
            else
                EnforceMode(m_points.Length - points.Length);

            if (m_loop)
            {
                var copySetting = m_settings[m_settings.Length - 1];
                m_settings[0] =
                    new ControlPointSetting(copySetting.Twist, copySetting.Thickness, m_settings[0].Branches);

                m_modes[0] = m_modes[m_modes.Length - 1];
                SetControlPointValue(0, m_points[m_points.Length - 1]);
                RaiseControlPointModeChanged(0);
                EnforceMode(m_points.Length - 1);
            }

            SyncCtrlPoints();
        }

        protected void AlignWithEnding(Vector3[] points, int curveIndex, float mag, float offset = 1.0f)
        {
            if (points.Length == 0) return;

            var dir = transform.InverseTransformDirection(GetDirection(offset, curveIndex));
            var point = GetPointLocal(offset, curveIndex);

            var deltaT = 1.0f / 3.0f;
            var t = deltaT;
            if (points.Length % 2 == 0) t = 0.0f;
            for (var i = 0; i < points.Length; ++i)
            {
                points[i] = point + dir * mag * t;
                t += deltaT;
            }
        }

        protected void AlignWithBeginning(Vector3[] points, int curveIndex, float mag, float offset = 0.0f)
        {
            if (points.Length == 0) return;

            var dir = GetDirection(offset, curveIndex);
            var point = GetPointLocal(offset, curveIndex);

            dir = transform.InverseTransformDirection(dir);

            var deltaT = 1.0f / 3.0f;
            var t = 1.0f;

            for (var i = 0; i < points.Length; ++i)
            {
                points[i] = point - dir * mag * t;
                t -= deltaT;
            }
        }

        protected virtual void OnCurveChanged(int pointIndex, int curveIndex)
        {
        }

        protected virtual void OnCurveChanged()
        {
        }


        protected virtual void AddControlPointComponent(GameObject ctrlPoint)
        {
            ctrlPoint.AddComponent<SplineControlPoint>();
        }

        private void SyncCtrlPoints(bool silent = false)
        {
            var runtimeEditor = SplineRuntimeEditor.Instance;
            var ctrlPoints = GetSplineControlPoints();
            var delta = ControlPointCount - ctrlPoints.Length;
            if (delta > 0)
            {
                for (var i = 0; i < delta; ++i)
                {
                    var ctrlPoint = new GameObject();
                    ctrlPoint.SetActive(IsSelected);
                    ctrlPoint.transform.parent = transform;
                    ctrlPoint.transform.rotation = Quaternion.identity;
                    ctrlPoint.transform.localScale = Vector3.one;

                    if (runtimeEditor != null)
                    {
                        var renderer = ctrlPoint.AddComponent<MeshRenderer>();
                        var filter = ctrlPoint.AddComponent<MeshFilter>();
                        filter.sharedMesh = runtimeEditor.ControlPointMesh;
                        renderer.sharedMaterial = runtimeEditor.NormalMaterial;
                        renderer.enabled = true; // settings.RuntimeEditing;
                    }

                    ctrlPoint.name = "ctrl point";
                    AddControlPointComponent(ctrlPoint);

#if UNITY_EDITOR
                    if (!silent)
                    {
                        Undo.RegisterCreatedObjectUndo(ctrlPoint, Undo.GetCurrentGroupName());
                        EditorUtility.SetDirty(ctrlPoint);
                    }
#endif
                }

                ctrlPoints = GetSplineControlPoints();
            }
            else if (delta < 0)
            {
                delta = -delta;
#if UNITY_EDITOR
                if (silent)
                {
                    for (var i = 0; i < delta; ++i)
                    {
                        var ctrlPoint = ctrlPoints[i];
                        if (ctrlPoint.gameObject != null) DestroyImmediate(ctrlPoint.gameObject);
                    }

                    ctrlPoints = GetSplineControlPoints();
                }
                else
                {
                    var ctrlPointsList = new List<SplineControlPoint>(ctrlPoints);
                    var controlPointsToRemove = new SplineControlPoint[delta];
                    var skip = 0;
                    for (var i = 0; i < delta; ++i)
                    {
                        if (Selection.activeGameObject == ctrlPoints[i].gameObject)
                        {
                            delta++;
                            skip++;
                            continue;
                        }

                        controlPointsToRemove[i - skip] = ctrlPoints[i];
                        ctrlPointsList.Remove(ctrlPoints[i]);
                    }

                    m_controlPointsToRemoveQueue.Enqueue(controlPointsToRemove);

                    EditorApplication.CallbackFunction removeCallback = RemoveControlPoints;
                    m_removeCallbacksQueue.Enqueue(removeCallback);

                    EditorApplication.delayCall += removeCallback;
                    ctrlPoints = ctrlPointsList.ToArray();
                }
#else
                for (int i = 0; i < delta; ++i)
                {
                    SplineControlPoint ctrlPoint = ctrlPoints[i];
                    if (ctrlPoint.gameObject != null)
                    {
                        GameObject.DestroyImmediate(ctrlPoint.gameObject);
                    }
                }
                ctrlPoints = GetSplineControlPoints();
#endif
            }

            for (var i = 0; i < ControlPointCount; ++i)
            {
                var ctrlPoint = ctrlPoints[i];
                ctrlPoint.Index = i;
                RaiseControlPointChanged(i);
                RaiseControlPointModeChanged(i);
            }
        }

        private void SetValue<T>(int controlPointIndex, T value,
            Action<int, T, bool> setter,
            Action<int, T, SplineBase, bool> branchSetter,
            Func<int, T> getter, bool raiseCurveChanged = true)
        {
            if (getter(controlPointIndex).Equals(value)) return;

            setter(controlPointIndex, value, raiseCurveChanged);
            SetBranchValues(controlPointIndex, value, branchSetter, raiseCurveChanged);
        }

        private void SetBranchValues<T>(int controlPointIndex, T value, Action<int, T, SplineBase, bool> branchSetter,
            bool raiseCurveChanged)
        {
            var settingIndex = (controlPointIndex + 1) / 3;
            var settings = m_settings[settingIndex];
            var branches = settings.Branches;
            if (branches != null)
                for (var i = 0; i < branches.Length; ++i)
                {
                    var branch = branches[i];
                    var connectedBranch = m_branches[branch.SplineIndex];
                    if (connectedBranch != null)
                    {
                        if (branch.Inbound)
                            branchSetter(connectedBranch.ControlPointCount - 1, value, connectedBranch,
                                raiseCurveChanged);
                        else
                            branchSetter(0, value, connectedBranch, raiseCurveChanged);
                    }
                }

            if (settingIndex == 0 && PrevSpline != null)
                branchSetter(PrevControlPointIndex, value, PrevSpline, raiseCurveChanged);

            if (settingIndex == m_settings.Length - 1 && NextSpline != null)
                branchSetter(NextControlPointIndex, value, NextSpline, raiseCurveChanged);
        }


#if UNITY_EDITOR
        // UNITY_EDITOR UNDO/REDO SUPPORT
        private bool m_initialized;

        [SerializeField] [HideInInspector] private int[] m_persistentVersions = new int[2];

        private readonly int[] m_currentVersions = new int[2];

        protected void OnVersionChanged()
        {
            SyncVersions();
        }

        private void SyncVersions()
        {
            Array.Copy(m_persistentVersions, m_currentVersions, m_persistentVersions.Length);
        }

        protected int[] PersistentVersions
        {
            get => m_persistentVersions;
            set => m_persistentVersions = value;
        }

        private int[] ChangedVersions()
        {
            var changed = new List<int>();
            for (var i = 0; i < m_currentVersions.Length; ++i)
                if (m_currentVersions[i] != m_persistentVersions[i])
                    changed.Add(i);

            if (changed.Count > 0) return changed.ToArray();

            return null;
        }

        private void OnUndoRedoPerformed()
        {
            var changed = ChangedVersions();
            if (changed != null)
            {
                SyncVersions();
                SyncCtrlPoints(true);
                OnSplineUndoRedo(changed);
            }
        }

        protected virtual void OnSplineUndoRedo(int[] changed)
        {
        }
#endif

#if UNITY_EDITOR
        [DidReloadScripts(100)]
        private static void OnScriptsReloaded()
        {
            if (Selection.activeGameObject != null)
            {
                var controlPoint = Selection.activeGameObject.GetComponent<SplineControlPoint>();
                if (controlPoint != null)
                {
                    ActiveControlPointIndex = controlPoint.Index;
                    ActiveSpline = controlPoint.GetComponentInParent<Spline>();
                }
                else
                {
                    var spline = Selection.activeGameObject.GetComponent<SplineBase>();
                    ActiveControlPointIndex = -1;
                    ActiveSpline = spline;
                }
            }

            if (GLRenderer.Instance != null)
            {
                var splines = FindObjectsOfType<SplineBase>().ToArray();
                for (var i = 0; i < splines.Length; i++)
                {
                    if (splines[i].IsSelected) GLRenderer.Instance.Add(splines[i]);
                    splines[i].SubscribeUndoRedo();
                }

                UpdateSceneCameras();
            }
            else
            {
                var splines = FindObjectsOfType<SplineBase>().ToArray();

                for (var i = 0; i < splines.Length; i++) splines[i].SubscribeUndoRedo();
            }
        }

        private static void UpdateSceneCameras()
        {
            var cameras = SceneView.GetAllSceneCameras();
            for (var i = 0; i < cameras.Length; ++i)
            {
                var sceneCamera = cameras[i];
                if (!sceneCamera.GetComponent<GLCamera>()) sceneCamera.gameObject.AddComponent<GLCamera>();
            }
        }

        private void SubscribeUndoRedo()
        {
            Undo.undoRedoPerformed -= OnUndoRedoPerformed;
            Undo.undoRedoPerformed += OnUndoRedoPerformed;
        }

#endif


#if UNITY_EDITOR
        /// <summary>
        ///     Delayed destroy (to update editor windows correctly)
        /// </summary>
        private readonly Queue<EditorApplication.CallbackFunction> m_removeCallbacksQueue = new();

        private readonly Queue<SplineControlPoint[]> m_controlPointsToRemoveQueue = new();
        private void RemoveControlPoints()
        {
            var cb = m_removeCallbacksQueue.Dequeue();
            var controlPointsToRemove = m_controlPointsToRemoveQueue.Dequeue();
            EditorApplication.delayCall -= cb;
            for (var i = 0; i < controlPointsToRemove.Length; ++i)
            {
                var controlPoint = controlPointsToRemove[i];
                if (controlPoint != null) Undo.DestroyObjectImmediate(controlPoint.gameObject);
            }
        }
#endif
    }
}