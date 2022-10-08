using System;
using System.Linq;
using Battlehub.RTEditor;
using Battlehub.RTHandles;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;

namespace Battlehub.SplineEditor
{
    [ExecuteInEditMode]
    public class SplineRuntimeEditor : MonoBehaviour
    {
        public static readonly Color MirroredModeColor = Color.red;
        public static readonly Color AlignedModeColor = Color.blue;
        public static readonly Color FreeModeColor = Color.yellow;
        public static readonly Color ControlPointLineColor = Color.gray;

        public Camera Camera;
        public float SelectionMargin = 20;

        private bool m_isApplicationQuit;

        public Mesh ControlPointMesh { get; private set; }

        public Material ConnectedMaterial { get; private set; }

        public Material MirroredModeMaterial { get; private set; }

        public Material AlignedModeMaterial { get; private set; }

        public Material FreeModeMaterial { get; private set; }

        public Material NormalMaterial { get; private set; }

        public static SplineRuntimeEditor Instance { get; private set; }

        private void Awake()
        {
#if UNITY_EDITOR
            Selection.activeObject = null;
#endif

            if (Camera == null)
            {
                Camera = Camera.main;
                if (Camera.main == null) Debug.LogError("Add Camera with MainCamera Tag");
            }

            if (Instance != null) Debug.LogWarning("Another instance of SplineEditorSettings already exist");

            if (MirroredModeMaterial == null)
            {
                var shader = Shader.Find("Battlehub/SplineEditor/SSBillboard");

                MirroredModeMaterial = new Material(shader);
                MirroredModeMaterial.name = "MirroredModeMaterial";
                MirroredModeMaterial.color = MirroredModeColor;
                MirroredModeMaterial.SetInt("_Cull", (int)CullMode.Off);
                MirroredModeMaterial.SetInt("_ZWrite", 1);
                MirroredModeMaterial.SetInt("_ZTest", (int)CompareFunction.Always);
            }

            if (AlignedModeMaterial == null)
            {
                AlignedModeMaterial = Instantiate(MirroredModeMaterial);
                AlignedModeMaterial.name = "AlignedModeMaterial";
                AlignedModeMaterial.color = AlignedModeColor;
            }

            if (FreeModeMaterial == null)
            {
                FreeModeMaterial = Instantiate(MirroredModeMaterial);
                FreeModeMaterial.name = "FreeModeMaterial";
                FreeModeMaterial.color = FreeModeColor;
            }

            if (NormalMaterial == null)
            {
                NormalMaterial = Instantiate(MirroredModeMaterial);
                NormalMaterial.name = "SplineMaterial";
                NormalMaterial.color = Color.green;
            }

            if (ConnectedMaterial == null)
            {
                ConnectedMaterial = Instantiate(MirroredModeMaterial);
                ConnectedMaterial.name = "BranchMaterial";
                ConnectedMaterial.color = new Color32(0xa5, 0x00, 0xff, 0xff);
            }

            if (ControlPointMesh == null)
            {
                ControlPointMesh = new Mesh();
                ControlPointMesh.name = "control point mesh";
                ControlPointMesh.vertices = new[]
                {
                    new Vector3(0, 0, 0),
                    new Vector3(0, 0, 0),
                    new Vector3(0, 0, 0),
                    new Vector3(0, 0, 0)
                };
                ControlPointMesh.triangles = new[]
                {
                    0, 1, 2, 0, 2, 3
                };
                ControlPointMesh.uv = new[]
                {
                    new Vector2(-1, -1),
                    new Vector2(1, -1),
                    new Vector2(1, 1),
                    new Vector2(-1, 1)
                };
                ControlPointMesh.RecalculateBounds();
            }

            Instance = this;
            EnableRuntimeEditing();

            RuntimeSelection.SelectionChanged += OnRuntimeSelectionChanged;
        }

        private void Start()
        {
            if (Created != null) Created(this, EventArgs.Empty);
        }

        private void LateUpdate()
        {
            if (Instance == null)
            {
                Instance = this;
                var splines = FindObjectsOfType<SplineBase>();
                for (var i = 0; i < splines.Length; ++i)
                {
                    var spline = splines[i];
                    if (spline.IsSelected) spline.Select();
                }
            }
        }

        private void OnDestroy()
        {
            if (!Application.isPlaying) DisableRuntimeEditing();

            var enteringPlayMode = false;
#if UNITY_EDITOR
            enteringPlayMode = EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying;
#endif


            if (!m_isApplicationQuit && !enteringPlayMode)
            {
                var controlPoints = Resources.FindObjectsOfTypeAll<SplineControlPoint>();
                for (var i = 0; i < controlPoints.Length; ++i)
                {
                    var controlPoint = controlPoints[i];
                    if (controlPoint != null) controlPoint.DestroyRuntimeComponents();
                }
            }

            if (Destroyed != null) Destroyed(this, EventArgs.Empty);

            RuntimeSelection.SelectionChanged -= OnRuntimeSelectionChanged;

            Instance = null;
        }

        private void OnApplicationQuit()
        {
            m_isApplicationQuit = true;
        }

        public static event EventHandler Created;
        public static event EventHandler Destroyed;

        private void DisableRuntimeEditing()
        {
            if (Camera != null)
            {
                var glCamera = Camera.GetComponent<GLCamera>();
                if (glCamera != null) DestroyImmediate(glCamera);
            }
        }

        private void EnableRuntimeEditing()
        {
            if (Camera == null) return;
            if (!Camera.GetComponent<GLCamera>()) Camera.gameObject.AddComponent<GLCamera>();
        }

        private void OnRuntimeSelectionChanged(Object[] unselected)
        {
            SplineBase minSpline = null;
            var minIndex = -1;
            var minDistance = float.PositiveInfinity;
            if (unselected != null)
            {
                var gameObjects = unselected.OfType<GameObject>().ToArray();

                for (var i = 0; i < gameObjects.Length; ++i)
                {
                    var go = gameObjects[i];
                    if (go == null) continue;

                    var spline = go.GetComponentInParent<SplineBase>();
                    if (spline == null) continue;

                    spline.Select();
                    var distance = minDistance;
                    SplineBase hitSpline;
                    var selectedIndex = HitTestRecursive(spline.Root, minDistance, out hitSpline, out distance);
                    if (distance < minDistance && selectedIndex != -1)
                    {
                        minDistance = distance;
                        minIndex = selectedIndex;
                        minSpline = hitSpline;
                    }

                    spline.Unselect();
                }

                if (minSpline != null)
                {
                    var ctrlPoint = minSpline.GetSplineControlPoints().Where(p => p.Index == minIndex).FirstOrDefault();
                    if (ctrlPoint != null) RuntimeSelection.activeObject = ctrlPoint.gameObject;
                    minSpline.Select();

                    return;
                }
            }

            if (RuntimeSelection.gameObjects != null)
            {
                var gameObjects = RuntimeSelection.gameObjects;
                if (gameObjects != null)
                    for (var i = 0; i < gameObjects.Length; ++i)
                    {
                        var spline = gameObjects[i].GetComponentInParent<SplineBase>();
                        if (spline != null) spline.Select();
                    }
            }
        }

        private int HitTestRecursive(SplineBase spline, float distance, out SplineBase resultSpline,
            out float resultDistance)
        {
            resultSpline = null;
            resultDistance = float.MaxValue;
            var minIndex = -1;

            float minDistance;
            var index = HitTest(spline, out minDistance);
            if (index > -1 && minDistance < distance)
            {
                resultSpline = spline;
                resultDistance = minDistance;
                distance = minDistance;
                minIndex = index;
            }

            if (spline.Children != null)
                for (var i = 0; i < spline.Children.Length; ++i)
                {
                    var child = spline.Children[i];
                    SplineBase childResult;
                    float childDistance;
                    var childIndex = HitTestRecursive(child, distance, out childResult, out childDistance);
                    if (childIndex > -1)
                    {
                        resultSpline = childResult;
                        resultDistance = childDistance;
                        distance = minDistance;

                        minIndex = childIndex;
                    }
                }

            return minIndex;
        }

        private int HitTest(SplineBase spline, out float minDistance)
        {
            minDistance = float.PositiveInfinity;
            if (Camera == null)
            {
                Debug.LogError("Camera is null");
                return -1;
            }

            if (RuntimeSelection.gameObjects == null) return -1;

            var controlPoints = new Vector3[spline.ControlPointCount];
            for (var j = 0; j < controlPoints.Length; j++) controlPoints[j] = spline.GetControlPoint(j);

            minDistance = SelectionMargin * SelectionMargin;
            var selectedIndex = -1;
            Vector2 mousePositon = Input.mousePosition;
            for (var i = 0; i < controlPoints.Length; ++i)
            {
                var ctrlPoint = controlPoints[i];
                if (spline.IsControlPointLocked(i)) continue;
                Vector2 pt = Camera.WorldToScreenPoint(ctrlPoint);
                var mag = (pt - mousePositon).sqrMagnitude;
                if (mag < minDistance)
                {
                    minDistance = mag;
                    selectedIndex = i;
                }
            }

            return selectedIndex;
        }

        public void OnClosed()
        {
            if (RuntimeSelection.gameObjects == null) return;

            var gameObjects = RuntimeSelection.gameObjects.OfType<GameObject>().ToArray();
            for (var i = 0; i < gameObjects.Length; ++i)
            {
                var go = gameObjects[i];
                if (go == null) continue;

                var spline = go.GetComponentInParent<SplineBase>();
                if (spline == null) continue;

                spline.Unselect();
            }
        }

        public void OnOpened()
        {
        }
    }
}