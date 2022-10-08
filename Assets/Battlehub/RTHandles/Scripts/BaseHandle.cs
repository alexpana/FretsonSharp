using UnityEngine;

namespace Battlehub.RTHandles
{
    public enum RuntimeTool
    {
        None,
        Move,
        Rotate,
        Scale,
        View
    }

    public enum RuntimePivotRotation
    {
        Local,
        Global
    }

    public delegate void RuntimeToolChanged();

    public delegate void RuntimePivotRotationChanged();

    public static class RuntimeTools
    {
        private static RuntimeTool m_current;
        private static RuntimePivotRotation m_pivotRotation;

        public static bool IsLocked { get; set; }

        public static bool IsDragDrop { get; set; }

        public static bool IsSceneGizmoSelected { get; set; }

        public static RuntimeTool Current
        {
            get => m_current;
            set
            {
                if (m_current != value)
                {
                    m_current = value;
                    if (ToolChanged != null) ToolChanged();
                }
            }
        }

        public static RuntimePivotRotation PivotRotation
        {
            get => m_pivotRotation;
            set
            {
                if (m_pivotRotation != value)
                {
                    m_pivotRotation = value;
                    if (PivotRotationChanged != null) PivotRotationChanged();
                }
            }
        }

        public static event RuntimeToolChanged ToolChanged;
        public static event RuntimePivotRotationChanged PivotRotationChanged;
    }

    public abstract class BaseHandle : MonoBehaviour, IGL
    {
        private static BaseHandle m_draggingTool;

        public KeyCode SnapToGridKey = KeyCode.LeftControl;
        public Camera Camera;
        public float SelectionMargin = 10;
        public Transform[] Targets;

        protected float EffectiveGridSize { get; private set; }

        public Transform Target => Targets[0];

        public bool IsDragging { get; private set; }

        protected abstract RuntimeTool Tool { get; }

        protected Quaternion Rotation
        {
            get
            {
                if (Targets == null || Targets.Length <= 0 || Target == null) return Quaternion.identity;

                return RuntimeTools.PivotRotation == RuntimePivotRotation.Local ? Target.rotation : Quaternion.identity;
            }
        }

        protected RuntimeHandleAxis SelectedAxis { get; set; }

        protected Plane DragPlane { get; set; }

        protected abstract float CurrentGridSize { get; }

        private void Start()
        {
            if (Camera == null) Camera = Camera.main;

            if (GLRenderer.Instance == null)
            {
                var glRenderer = new GameObject();
                glRenderer.name = "GLRenderer";
                glRenderer.AddComponent<GLRenderer>();
            }

            if (Camera != null)
                if (!Camera.GetComponent<GLCamera>())
                    Camera.gameObject.AddComponent<GLCamera>();

            if (Targets == null || Targets.Length == 0) Targets = new[] { transform };

            if (GLRenderer.Instance != null) GLRenderer.Instance.Add(this);

            if (Targets[0].position != transform.position) transform.position = Targets[0].position;

            StartOverride();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if ((RuntimeTools.Current != Tool && RuntimeTools.Current != RuntimeTool.None) ||
                    RuntimeTools.IsLocked) return;

                if (Camera == null)
                {
                    Debug.LogError("Camera is null");
                    return;
                }

                if (m_draggingTool != null) return;

                IsDragging = OnBeginDrag();
                if (IsDragging) m_draggingTool = this;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                OnDrop();
                IsDragging = false;
                m_draggingTool = null;
            }
            else
            {
                if (IsDragging)
                {
                    if (Input.GetKey(SnapToGridKey))
                        EffectiveGridSize = CurrentGridSize;
                    else
                        EffectiveGridSize = 0;

                    OnDrag();
                }
            }

            UpdateOverride();
        }

        private void OnEnable()
        {
            if (GLRenderer.Instance != null) GLRenderer.Instance.Add(this);

            OnEnableOverride();
        }

        private void OnDisable()
        {
            if (GLRenderer.Instance != null) GLRenderer.Instance.Remove(this);

            OnDisableOverride();
        }

        private void OnDestroy()
        {
            if (GLRenderer.Instance != null) GLRenderer.Instance.Remove(this);

            OnDestroyOverride();
        }

        void IGL.Draw()
        {
            DrawOverride();
        }

        protected virtual void StartOverride()
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

        protected virtual bool OnBeginDrag()
        {
            return false;
        }

        protected virtual void OnDrag()
        {
        }

        protected virtual void OnDrop()
        {
        }

        protected virtual void UpdateOverride()
        {
            if (Targets != null && Targets.Length > 0 && Targets[0] != null &&
                Targets[0].position != transform.position)
            {
                if (IsDragging)
                {
                    var offset = transform.position - Targets[0].position;
                    Targets[0].position = transform.position;
                    for (var i = 1; i < Targets.Length; ++i) Targets[i].position += offset;
                }
                else
                {
                    transform.position = Targets[0].position;
                    transform.rotation = Targets[0].rotation;
                }
            }
        }

        protected bool HitCenter()
        {
            Vector2 screenCenter = Camera.WorldToScreenPoint(transform.position);
            Vector2 mouse = Input.mousePosition;

            return (mouse - screenCenter).magnitude <= SelectionMargin;
        }

        protected bool HitAxis(Vector3 axis, Matrix4x4 matrix, out float distanceToAxis)
        {
            axis = matrix.MultiplyVector(axis);
            Vector2 screenVectorBegin = Camera.WorldToScreenPoint(transform.position);
            Vector2 screenVectorEnd = Camera.WorldToScreenPoint(axis + transform.position);

            Vector3 screenVector = screenVectorEnd - screenVectorBegin;
            var screenVectorMag = screenVector.magnitude;
            screenVector.Normalize();
            if (screenVector != Vector3.zero)
            {
                var perp = PerpendicularClockwise(screenVector).normalized;
                Vector2 mousePosition = Input.mousePosition;
                var relMousePositon = mousePosition - screenVectorBegin;

                distanceToAxis = Mathf.Abs(Vector2.Dot(perp, relMousePositon));
                var hitPoint = relMousePositon - perp * distanceToAxis;
                var vectorSpaceCoord = Vector2.Dot(screenVector, hitPoint);

                var result = vectorSpaceCoord <= screenVectorMag + SelectionMargin &&
                             vectorSpaceCoord >= -SelectionMargin && distanceToAxis <= SelectionMargin;
                if (!result)
                {
                    distanceToAxis = float.PositiveInfinity;
                }
                else
                {
                    if (screenVectorMag < SelectionMargin) distanceToAxis = 0.0f;
                }

                return result;
            }
            else
            {
                Vector2 mousePosition = Input.mousePosition;

                distanceToAxis = (screenVectorBegin - mousePosition).magnitude;
                var result = distanceToAxis <= SelectionMargin;
                if (!result)
                    distanceToAxis = float.PositiveInfinity;
                else
                    distanceToAxis = 0.0f;
                return result;
            }
        }

        protected Plane GetDragPlane(Matrix4x4 matrix, Vector3 axis)
        {
            var plane = new Plane(matrix.MultiplyVector(axis).normalized, matrix.MultiplyPoint(Vector3.zero));
            return plane;
        }

        protected Plane GetDragPlane()
        {
            var toCam = Camera.cameraToWorldMatrix
                .MultiplyVector(Vector3.forward); //Camera.transform.position - transform.position;
            var dragPlane = new Plane(toCam.normalized, transform.position);
            return dragPlane;
        }

        protected bool GetPointOnDragPlane(Vector3 screenPos, out Vector3 point)
        {
            return GetPointOnDragPlane(DragPlane, screenPos, out point);
        }

        protected bool GetPointOnDragPlane(Plane dragPlane, Vector3 screenPos, out Vector3 point)
        {
            var ray = Camera.ScreenPointToRay(screenPos);
            float distance;
            if (dragPlane.Raycast(ray, out distance))
            {
                point = ray.GetPoint(distance);
                return true;
            }

            point = Vector3.zero;
            return false;
        }

        private static Vector2 PerpendicularClockwise(Vector2 vector2)
        {
            return new Vector2(-vector2.y, vector2.x);
        }

        protected virtual void DrawOverride()
        {
        }
    }
}