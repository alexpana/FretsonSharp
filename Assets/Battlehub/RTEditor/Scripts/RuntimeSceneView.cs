using System.Linq;
using Battlehub.RTHandles;
using Battlehub.UIControls;
using Battlehub.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battlehub.RTEditor
{
    public delegate void UnityEditorToolChanged();

    public class UnityEditorToolsListener
    {
        public static event UnityEditorToolChanged ToolChanged;

        public static void Update()
        {
#if UNITY_EDITOR
            if (m_tool != Tools.current)
            {
                if (ToolChanged != null) ToolChanged();
                m_tool = Tools.current;
            }
#endif
        }

#if UNITY_EDITOR
        private static Tool m_tool;
        static UnityEditorToolsListener()
        {
            m_tool = Tools.current;
        }
#endif
    }

    public delegate void RuntimeSelectionChanged(Object[] unselectedObjects);

    public static class RuntimeSelection
    {
        private static Object m_activeObject;

        private static Object[] m_objects;

        public static GameObject activeGameObject
        {
            get => activeObject as GameObject;
            set => activeObject = value;
        }

        public static Object activeObject
        {
            get => m_activeObject;
            set
            {
                if (m_activeObject != value)
                {
                    m_activeObject = value;
                    var unselectedObjects = m_objects;
                    if (m_activeObject != null)
                        m_objects = new[] { value };
                    else
                        m_objects = new Object[0];
                    if (SelectionChanged != null) SelectionChanged(unselectedObjects);
                }
            }
        }

        public static Object[] objects
        {
            get => m_objects;
            set
            {
                var oldObjects = m_objects;

                if (value == null)
                {
                    m_objects = null;
                    m_activeObject = null;
                }
                else
                {
                    m_objects = value.ToArray();
                    if (m_activeObject == null || !m_objects.Contains(m_activeObject))
                        m_activeObject = m_objects.OfType<GameObject>().FirstOrDefault();
                }

                if (oldObjects == m_objects) return;

                if (SelectionChanged == null) return;

                if (oldObjects == null || m_objects == null)
                {
                    SelectionChanged(oldObjects);
                }
                else
                {
                    if (oldObjects.Length != m_objects.Length)
                        SelectionChanged(oldObjects);
                    else
                        for (var i = 0; i < m_objects.Length; ++i)
                            if (m_objects[i] != oldObjects[i])
                            {
                                SelectionChanged(oldObjects);
                                break;
                            }
                }
            }
        }

        public static GameObject[] gameObjects
        {
            get
            {
                if (m_objects == null) return null;

                return m_objects.OfType<GameObject>().ToArray();
            }
        }

        public static Transform activeTransform
        {
            get
            {
                if (m_activeObject == null) return null;

                if (m_activeObject is GameObject) return ((GameObject)m_activeObject).transform;
                return null;
            }
            set
            {
                if (value)
                    m_activeObject = value.gameObject;
                else
                    m_activeObject = null;
            }
        }

        public static event RuntimeSelectionChanged SelectionChanged;

        public static void Select(Object activeGameObject, Object[] selection)
        {
            m_activeObject = activeGameObject;
            objects = selection;
        }
    }

    public class RuntimeSceneView : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Texture2D ViewTexture;
        public Texture2D MoveTexture;
        public Camera Camera;
        public Transform Pivot;
        public float RotationSensitivity = 1f;
        public float ZoomSensitivity = 8f;
        public float PanSensitivity = 100f;
        private bool m_handleInput;
        private Vector3 m_lastMousePosition;
        private bool m_lockInput;

        private MouseOrbit m_mouseOrbit;

        private bool m_pan;

        private PositionHandle m_positionHandle;
        private bool m_rotate;
        private RotationHandle m_rotationHandle;
        private ScaleHandle m_scaleHandle;

        public bool IsPointerOver { get; private set; }

        private void Awake()
        {
            if (Camera == null) Camera = Camera.main;

            if (Run.Instance == null)
            {
                var runGO = new GameObject();
                runGO.name = "Run";
                runGO.AddComponent<Run>();
            }

            RuntimeTools.Current = RuntimeTool.View;
            var positionHandle = new GameObject();
            positionHandle.name = "PositionHandle";
            positionHandle.transform.SetParent(transform, false);
            m_positionHandle = positionHandle.AddComponent<PositionHandle>();
            positionHandle.SetActive(false);

            var rotationHandle = new GameObject();
            rotationHandle.name = "RotationHandle";
            rotationHandle.transform.SetParent(transform, false);
            m_rotationHandle = rotationHandle.AddComponent<RotationHandle>();
            rotationHandle.SetActive(false);

            var scaleHandle = new GameObject();
            scaleHandle.name = "ScaleHandle";
            scaleHandle.transform.SetParent(transform, false);
            m_scaleHandle = scaleHandle.AddComponent<ScaleHandle>();
            scaleHandle.SetActive(false);

            RuntimeSelection.SelectionChanged += OnRuntimeSelectionChanged;
            RuntimeTools.ToolChanged += OnRuntimeToolChanged;
            UnityEditorToolsListener.ToolChanged += OnUnityEditorToolChanged;
            RuntimeTools.Current = RuntimeTool.Move;

            Camera.fieldOfView = 60;
            OnProjectionChanged();
        }

        private void Start()
        {
            m_mouseOrbit = Camera.gameObject.GetComponent<MouseOrbit>();
            if (m_mouseOrbit == null) m_mouseOrbit = Camera.gameObject.AddComponent<MouseOrbit>();
            UnlockInput();
            m_mouseOrbit.enabled = false;
        }

        private void Update()
        {
#if UNITY_EDITOR
            UnityEditorToolsListener.Update();
#endif

            HandleInput();
        }

        private void OnDestroy()
        {
            RuntimeTools.Current = RuntimeTool.None;
            RuntimeSelection.SelectionChanged -= OnRuntimeSelectionChanged;
            RuntimeTools.ToolChanged -= OnRuntimeToolChanged;
            UnityEditorToolsListener.ToolChanged -= OnUnityEditorToolChanged;
        }

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            var go = eventData.pointerDrag;
            if (go != null)
            {
                var itemContainer = go.GetComponent<ItemContainer>();
                if (itemContainer != null && itemContainer.Item != null)
                {
                    var item = itemContainer.Item;
                    if (item != null && item is GameObject)
                    {
                        var prefab = item as GameObject;
                        if (RuntimePrefabs.IsPrefab(prefab.transform))
                        {
                            var ray = Camera.ScreenPointToRay(Input.mousePosition);
                            //Plane p = new Plane(Vector3.up, Vector3.zero);
                            float distance;
                            //if(!p.Raycast(ray, out distance))
                            {
                                distance = 15;
                            }
                            var worldPoint = ray.GetPoint(distance);
                            var prefabInstance = Instantiate(prefab);
                            var exposeToEditor = prefabInstance.GetComponent<ExposeToEditor>();
                            if (exposeToEditor != null) exposeToEditor.SetName(prefab.name);
                            prefabInstance.transform.position = worldPoint;
                            prefabInstance.transform.rotation = prefab.transform.rotation;
                            prefabInstance.transform.localScale = prefab.transform.localScale;
                            RuntimeSelection.activeGameObject = prefabInstance;
                        }
                    }
                }
            }
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            IsPointerOver = true;
            SetCursor();
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            IsPointerOver = false;
        }


        public void LockInput()
        {
            m_lockInput = true;
        }

        public void UnlockInput()
        {
            m_lockInput = false;
            if (m_mouseOrbit != null)
            {
                Pivot.position = Camera.transform.position + Camera.transform.forward * m_mouseOrbit.Distance;
                m_mouseOrbit.Target = Pivot;
                m_mouseOrbit.SyncAngles();
            }
        }

        public void OnProjectionChanged()
        {
            var fov = Camera.fieldOfView * Mathf.Deg2Rad;
            var distance = (Camera.transform.position - Pivot.position).magnitude;
            var objSize = distance * Mathf.Sin(fov / 2);
            Camera.orthographicSize = objSize;
        }

        private void OnRuntimeToolChanged()
        {
            SetCursor();

            if (RuntimeSelection.activeTransform == null) return;

            if (m_positionHandle != null)
            {
                m_positionHandle.gameObject.SetActive(false);
                if (RuntimeTools.Current == RuntimeTool.Move)
                {
                    m_positionHandle.transform.position = RuntimeSelection.activeTransform.position;
                    m_positionHandle.Targets = RuntimeSelection.gameObjects.Where(g => g.GetComponent<ExposeToEditor>())
                        .Select(g => g.transform).OrderByDescending(g => RuntimeSelection.activeTransform == g)
                        .ToArray(); //active game object first
                    m_positionHandle.gameObject.SetActive(m_positionHandle.Targets.Length > 0);
                }
            }

            if (m_rotationHandle != null)
            {
                m_rotationHandle.gameObject.SetActive(false);
                if (RuntimeTools.Current == RuntimeTool.Rotate)
                {
                    m_rotationHandle.transform.position = RuntimeSelection.activeTransform.position;
                    m_rotationHandle.Targets = RuntimeSelection.gameObjects.Where(g => g.GetComponent<ExposeToEditor>())
                        .Select(g => g.transform).OrderByDescending(g => RuntimeSelection.activeTransform == g)
                        .ToArray();
                    m_rotationHandle.gameObject.SetActive(m_rotationHandle.Targets.Length > 0);
                }
            }

            if (m_scaleHandle != null)
            {
                m_scaleHandle.gameObject.SetActive(false);
                if (RuntimeTools.Current == RuntimeTool.Scale)
                {
                    m_scaleHandle.transform.position = RuntimeSelection.activeTransform.position;
                    m_scaleHandle.Targets = RuntimeSelection.gameObjects.Where(g => g.GetComponent<ExposeToEditor>())
                        .Select(g => g.transform).OrderByDescending(g => RuntimeSelection.activeTransform == g)
                        .ToArray();
                    m_scaleHandle.gameObject.SetActive(m_scaleHandle.Targets.Length > 0);
                }
            }


#if UNITY_EDITOR
            switch (RuntimeTools.Current)
            {
                case RuntimeTool.None:
                    Tools.current = Tool.None;
                    break;
                case RuntimeTool.Move:
                    Tools.current = Tool.Move;
                    break;
                case RuntimeTool.Rotate:
                    Tools.current = Tool.Rotate;
                    break;
                case RuntimeTool.Scale:
                    Tools.current = Tool.Scale;
                    break;
                case RuntimeTool.View:
                    Tools.current = Tool.View;
                    break;
            }
#endif
        }

        private void OnUnityEditorToolChanged()
        {
#if UNITY_EDITOR
            switch (Tools.current)
            {
                case Tool.None:
                    RuntimeTools.Current = RuntimeTool.None;
                    break;
                case Tool.Move:
                    RuntimeTools.Current = RuntimeTool.Move;
                    break;
                case Tool.Rotate:
                    RuntimeTools.Current = RuntimeTool.Rotate;
                    break;
                case Tool.Scale:
                    RuntimeTools.Current = RuntimeTool.Scale;
                    break;
                case Tool.View:
                    RuntimeTools.Current = RuntimeTool.View;
                    break;
                default:
                    RuntimeTools.Current = RuntimeTool.None;
                    break;
            }
#endif
        }

        private void OnRuntimeSelectionChanged(Object[] unselected)
        {
            if (RuntimeSelection.activeGameObject == null ||
                RuntimePrefabs.IsPrefab(RuntimeSelection.activeGameObject.transform))
            {
                if (m_positionHandle != null) m_positionHandle.gameObject.SetActive(false);
                if (m_rotationHandle != null) m_rotationHandle.gameObject.SetActive(false);
                if (m_scaleHandle != null) m_scaleHandle.gameObject.SetActive(false);
            }
            else
            {
                OnRuntimeToolChanged();
            }
        }

        private void HandleInput()
        {
            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
            {
                m_handleInput = false;
                m_mouseOrbit.enabled = false;
                m_rotate = false;
                SetCursor();
                return;
            }

            if (m_lockInput) return;


            if (Input.GetKeyDown(KeyCode.F)) Focus();

            var pan = Input.GetMouseButton(2) || Input.GetMouseButton(1) ||
                      (Input.GetMouseButton(0) && RuntimeTools.Current == RuntimeTool.View);
            var rotate = Input.GetKey(KeyCode.AltGr) || Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);
            if (pan != m_pan)
            {
                m_pan = pan;
                if (m_pan)
                    if (RuntimeTools.Current != RuntimeTool.View)
                        m_rotate = false;
                SetCursor();
            }
            else
            {
                if (rotate != m_rotate)
                {
                    m_rotate = rotate;
                    SetCursor();
                }
            }

            var isLocked = m_rotate || pan;
            RuntimeTools.IsLocked = isLocked;
            if (!isLocked)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                    RuntimeTools.Current = RuntimeTool.View;
                else if (Input.GetKeyDown(KeyCode.W))
                    RuntimeTools.Current = RuntimeTool.Move;
                else if (Input.GetKeyDown(KeyCode.E))
                    RuntimeTools.Current = RuntimeTool.Rotate;
                else if (Input.GetKeyDown(KeyCode.R)) RuntimeTools.Current = RuntimeTool.Scale;
            }

            if (!IsPointerOver) return;

            if (Input.GetKeyDown(KeyCode.X))
            {
                if (RuntimeTools.PivotRotation == RuntimePivotRotation.Local)
                    RuntimeTools.PivotRotation = RuntimePivotRotation.Global;
                else
                    RuntimeTools.PivotRotation = RuntimePivotRotation.Local;
            }

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            {
                m_handleInput = !m_positionHandle.IsDragging;
                m_lastMousePosition = Input.mousePosition;
                if (m_rotate) m_mouseOrbit.enabled = true;
            }

            var mouseWheel = Input.GetAxis("Mouse ScrollWheel");
            if (mouseWheel != 0)
                if (!(EventSystem.current && EventSystem.current.IsPointerOverGameObject()) || IsPointerOver)
                    m_mouseOrbit.Zoom();

            if (m_handleInput)
                if (isLocked)
                    if (m_pan && (!m_rotate || RuntimeTools.Current != RuntimeTool.View))
                        Pan();
        }

        private void Focus()
        {
            if (RuntimeSelection.activeTransform == null) return;

            var bounds = CalculateBounds(RuntimeSelection.activeTransform);
            var fov = Camera.fieldOfView * Mathf.Deg2Rad;
            var objSize = Mathf.Max(bounds.extents.y, bounds.extents.x, bounds.extents.z) * 2.0f;
            var distance = Mathf.Abs(objSize / Mathf.Sin(fov / 2.0f));

            Pivot.position = bounds.center;
            const float duration = 0.5f;
            Run.Instance.Animation(new Vector3AnimationInfo(Camera.transform.position,
                Pivot.position - distance * Camera.transform.forward, duration, Vector3AnimationInfo.EaseOutCubic,
                (target, value, t, completed) =>
                {
                    if (Camera) Camera.transform.position = value;
                }));

            Run.Instance.Animation(new FloatAnimationInfo(m_mouseOrbit.Distance, distance, duration,
                Vector3AnimationInfo.EaseOutCubic,
                (target, value, t, completed) =>
                {
                    if (m_mouseOrbit) m_mouseOrbit.Distance = value;
                }));

            Run.Instance.Animation(new FloatAnimationInfo(Camera.orthographicSize, objSize, duration,
                Vector3AnimationInfo.EaseOutCubic,
                (target, value, t, completed) =>
                {
                    if (Camera) Camera.orthographicSize = value;
                }));
        }

        private Bounds CalculateBounds(Transform t)
        {
            var renderer = t.GetComponentInChildren<Renderer>();
            if (renderer)
            {
                var bounds = renderer.bounds;
                if (bounds.size == Vector3.zero && bounds.center != renderer.transform.position)
                    bounds = TransformBounds(renderer.transform.localToWorldMatrix, bounds);
                CalculateBounds(t, ref bounds);
                if (bounds.extents == Vector3.zero) bounds.extents = new Vector3(0.5f, 0.5f, 0.5f);
                return bounds;
            }

            return new Bounds(t.position, new Vector3(0.5f, 0.5f, 0.5f));
        }

        private void CalculateBounds(Transform t, ref Bounds totalBounds)
        {
            foreach (Transform child in t)
            {
                var renderer = child.GetComponent<Renderer>();
                if (renderer)
                {
                    var bounds = renderer.bounds;
                    if (bounds.size == Vector3.zero && bounds.center != renderer.transform.position)
                        bounds = TransformBounds(renderer.transform.localToWorldMatrix, bounds);
                    totalBounds.Encapsulate(bounds.min);
                    totalBounds.Encapsulate(bounds.max);
                }

                CalculateBounds(child, ref totalBounds);
            }
        }

        public static Bounds TransformBounds(Matrix4x4 matrix, Bounds bounds)
        {
            var center = matrix.MultiplyPoint(bounds.center);

            // transform the local extents' axes
            var extents = bounds.extents;
            var axisX = matrix.MultiplyVector(new Vector3(extents.x, 0, 0));
            var axisY = matrix.MultiplyVector(new Vector3(0, extents.y, 0));
            var axisZ = matrix.MultiplyVector(new Vector3(0, 0, extents.z));

            // sum their absolute value to get the world extents
            extents.x = Mathf.Abs(axisX.x) + Mathf.Abs(axisY.x) + Mathf.Abs(axisZ.x);
            extents.y = Mathf.Abs(axisX.y) + Mathf.Abs(axisY.y) + Mathf.Abs(axisZ.y);
            extents.z = Mathf.Abs(axisX.z) + Mathf.Abs(axisY.z) + Mathf.Abs(axisZ.z);

            return new Bounds { center = center, extents = extents };
        }

        private void Pan()
        {
            var delta = m_lastMousePosition - Input.mousePosition;

            delta = delta / Mathf.Sqrt(Camera.pixelHeight * Camera.pixelHeight + Camera.pixelWidth * Camera.pixelWidth);

            delta *= PanSensitivity;

            delta = Camera.cameraToWorldMatrix.MultiplyVector(delta);
            Camera.transform.position += delta;
            Pivot.position += delta;

            m_lastMousePosition = Input.mousePosition;
        }

        private void SetCursor()
        {
            if (!IsPointerOver)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                return;
            }

            if (m_pan)
            {
                if (m_rotate && RuntimeTools.Current == RuntimeTool.View)
                    Cursor.SetCursor(ViewTexture, Vector2.zero, CursorMode.Auto);
                else
                    Cursor.SetCursor(MoveTexture, Vector2.zero, CursorMode.Auto);
            }
            else if (m_rotate)
            {
                Cursor.SetCursor(ViewTexture, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                if (RuntimeTools.Current == RuntimeTool.View)
                    Cursor.SetCursor(MoveTexture, Vector2.zero, CursorMode.Auto);
                else
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
        }
    }
}