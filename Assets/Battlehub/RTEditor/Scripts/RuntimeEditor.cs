using System.Collections.Generic;
using System.Linq;
using Battlehub.RTHandles;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Battlehub.RTEditor
{
    public class RuntimeEditor : MonoBehaviour
    {
        public UnityEvent Opened;
        public UnityEvent Closed;

        public GameObject[] Prefabs;

        public GameObject Grid;
        public GameObject SceneGizmo;
        public GameObject EditButton;
        public GameObject CloseButton;
        public GameObject EditorRoot;
        public Camera SceneCamera;
        public RuntimeSceneView SceneView;

        public KeyCode MultiselectKey = KeyCode.LeftControl;
        public KeyCode RangeSelectKey = KeyCode.LeftShift;
        public KeyCode DuplicateKey = KeyCode.D;
        public KeyCode DuplicateKey2 = KeyCode.LeftShift;

        private bool m_isOn;
        private int m_raycastLayer = 31;

        private LayerMask m_raycastLayerMask = 1 << 31;

        public int RaycastLayer
        {
            get => m_raycastLayer;
            set
            {
                m_raycastLayer = value;
                m_raycastLayerMask = 1 << value;
            }
        }

        public bool IsOn
        {
            get => m_isOn;
            set
            {
                if (m_isOn != value)
                {
                    m_isOn = value;
                    if (m_isOn)
                        ShowEditor();
                    else
                        CloseEditor();
                }
            }
        }

        public static RuntimeEditor Instance { get; private set; }

        private void Awake()
        {
            ExposeToEditor.Started += OnObjectStarted;

            if (Instance != null) Debug.LogWarning("Another instance of RuntimeEditor exists");
            Instance = this;

            if (SceneCamera == null) SceneCamera = Camera.main;

            SceneView.Camera = SceneCamera;
        }

        private void Start()
        {
            ShowEditor();
            CloseEditor();

            ExposeToEditor.Awaked += OnObjectAwaked;
            ExposeToEditor.Enabled += OnObjectEnabled;
            ExposeToEditor.Disabled += OnObjectDisabled;
            ExposeToEditor.Destroyed += OnObjectDestroyed;
            if (m_isOn)
                ShowEditor();
            else
                CloseEditor();
        }

        private void LateUpdate()
        {
            if (Input.GetKeyDown(DuplicateKey) && Input.GetKey(DuplicateKey2))
            {
                var selectedObjects = RuntimeSelection.objects;
                if (selectedObjects != null && selectedObjects.Length > 0)
                {
                    var duplicates = new Object[selectedObjects.Length];
                    for (var i = 0; i < duplicates.Length; ++i)
                    {
                        var go = selectedObjects[i] as GameObject;
                        var duplicate = Instantiate(selectedObjects[i]);
                        var duplicateGo = duplicate as GameObject;
                        if (go != null && duplicateGo != null)
                            if (go.transform.parent != null)
                                duplicateGo.transform.SetParent(go.transform.parent, true);

                        duplicates[i] = duplicate;
                    }

                    RuntimeSelection.objects = duplicates;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (PositionHandle.Current != null && PositionHandle.Current.IsDragging) return;
                if (ScaleHandle.Current != null && ScaleHandle.Current.IsDragging) return;
                if (RotationHandle.Current != null && RotationHandle.Current.IsDragging) return;

                if (!SceneView.IsPointerOver && EventSystem.current != null &&
                    EventSystem.current.IsPointerOverGameObject()) return;

                if (RuntimeTools.IsLocked) return;

                if (RuntimeTools.IsSceneGizmoSelected) return;

                var rangeSelect = Input.GetKey(RangeSelectKey);
                var multiselect = Input.GetKey(MultiselectKey) || rangeSelect;
                var ray = SceneCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, float.MaxValue, m_raycastLayerMask.value))
                {
                    var exposeToEditor = hitInfo.collider.gameObject.GetComponent<ExposeToEditor>();
                    if (exposeToEditor != null)
                    {
                        if (multiselect)
                        {
                            List<Object> selection;
                            if (RuntimeSelection.objects != null)
                                selection = RuntimeSelection.objects.ToList();
                            else
                                selection = new List<Object>();

                            if (selection.Contains(exposeToEditor.gameObject))
                            {
                                selection.Remove(exposeToEditor.gameObject);
                                if (rangeSelect) selection.Insert(0, exposeToEditor.gameObject);
                            }
                            else
                            {
                                selection.Insert(0, exposeToEditor.gameObject);
                            }

                            RuntimeSelection.Select(exposeToEditor.gameObject, selection.ToArray());
                        }
                        else
                        {
                            RuntimeSelection.activeObject = exposeToEditor.gameObject;
                        }
                    }
                    else
                    {
                        if (!multiselect) RuntimeSelection.activeObject = null;
                    }
                }
                else
                {
                    if (!multiselect) RuntimeSelection.activeObject = null;
                }
            }
        }

        private void OnApplicationQuit()
        {
            ExposeToEditor.Awaked -= OnObjectAwaked;
            ExposeToEditor.Started -= OnObjectStarted;
            ExposeToEditor.Enabled -= OnObjectEnabled;
            ExposeToEditor.Disabled -= OnObjectDisabled;
            ExposeToEditor.Destroyed -= OnObjectDestroyed;
        }

        private void Destroy()
        {
            ExposeToEditor.Awaked -= OnObjectAwaked;
            ExposeToEditor.Started -= OnObjectStarted;
            ExposeToEditor.Enabled -= OnObjectEnabled;
            ExposeToEditor.Disabled -= OnObjectDisabled;
            ExposeToEditor.Destroyed -= OnObjectDestroyed;
        }

        private void OnObjectAwaked(ExposeToEditor obj)
        {
        }

        private void OnObjectStarted(ExposeToEditor obj)
        {
            obj.gameObject.layer = m_raycastLayer;
        }

        private void OnObjectEnabled(ExposeToEditor obj)
        {
            obj.gameObject.layer = m_raycastLayer;
        }

        private void OnObjectDisabled(ExposeToEditor obj)
        {
        }

        private void OnObjectDestroyed(ExposeToEditor obj)
        {
        }

        private void ShowEditor()
        {
            if (SceneGizmo != null) SceneGizmo.SetActive(true);
            if (Grid != null) Grid.SetActive(true);
            EditButton.SetActive(false);
            EditorRoot.SetActive(true);

            //RuntimeSelection.activeObject = null;
#if UNITY_EDITOR
            Selection.activeObject = null;
#endif

            Opened.Invoke();
        }

        private void CloseEditor()
        {
            if (SceneGizmo != null) SceneGizmo.SetActive(false);
            if (Grid != null) Grid.SetActive(false);
            EditButton.SetActive(true);
            EditorRoot.SetActive(false);

            //RuntimeSelection.activeObject = null;
#if UNITY_EDITOR
            Selection.activeObject = null;
#endif

            Closed.Invoke();
        }
    }
}