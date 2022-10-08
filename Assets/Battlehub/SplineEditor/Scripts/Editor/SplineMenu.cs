using System.Linq;
using Battlehub.RTEditor;
using Battlehub.Utils;
using UnityEditor;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battlehub.SplineEditor
{
    public static class SplineMenu
    {
        private const string root = "Battlehub/SplineEditor/";

        public static GameObject InstantiatePrefab(string name)
        {
            var prefab = AssetDatabase.LoadAssetAtPath("Assets/" + root + "Prefabs/" + name, typeof(GameObject));
            return (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        }

        [MenuItem("Tools/Spline/Create Runtime Editor", validate = true)]
        public static bool CanCreateRuntimeEditor()
        {
            return !Object.FindObjectOfType<SplineRuntimeEditor>() && SplineRuntimeEditor.Instance == null;
        }

        [MenuItem("Tools/Spline/Create Runtime Editor")]
        public static void CreateRuntimeEditor()
        {
            var uiCommandsGo = InstantiatePrefab("CommandsPanel.prefab");
            CreateRuntimeEditor(uiCommandsGo, "Spline Runtime Editor");
        }

        public static void CreateRuntimeEditor(GameObject commandsPanel, string name)
        {
            if (!Object.FindObjectOfType<EventSystem>())
            {
                var es = new GameObject();
                es.AddComponent<EventSystem>();
                es.AddComponent<StandaloneInputModule>();
                es.name = "EventSystem";
            }

            var go = new GameObject();
            go.name = name;
            var srtEditor = go.AddComponent<SplineRuntimeEditor>();

            var uiEditorGO = RTEditorMenu.InstantiateRuntimeEditor();
            uiEditorGO.transform.SetParent(go.transform, false);

            var rtEditor = uiEditorGO.GetComponent<RuntimeEditor>();
            UnityEventTools.AddPersistentListener(rtEditor.Closed, srtEditor.OnClosed);


            var placeholders = uiEditorGO.GetComponentsInChildren<Placeholder>(true);
            var cmd = placeholders.Where(p => p.Id == Placeholder.CommandsPlaceholder).First();

            commandsPanel.transform.SetParent(cmd.transform, false);

            Undo.RegisterCreatedObjectUndo(go, "Battlehub.Spline.CreateRuntimeEditor");
        }

        [MenuItem("GameObject/Spline", false, 10, validate = true)]
        [MenuItem("Tools/Spline/Create", validate = true)]
        public static bool CanCreate()
        {
            return SceneView.lastActiveSceneView != null && SceneView.lastActiveSceneView.camera != null;
        }

        [MenuItem("GameObject/Spline", false, 10)]
        [MenuItem("Tools/Spline/Create")]
        public static void Create()
        {
            var sceneCam = SceneView.lastActiveSceneView.camera;
            var thickness = new Thickness(Vector3.one, 0, 1);
            var twist = new Twist(0, 0, 1);
            Selection.activeGameObject = SplineEditor
                .CreateSpline(sceneCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 5f)), thickness, twist).gameObject;
        }

        [MenuItem("Tools/Spline/Branching/OUT -> Branch", validate = true)]
        public static bool CanCreateOutBranch()
        {
            if (SplineBaseEditor.ConvergingSpline) return false;

            var selected = Selection.activeGameObject;
            if (selected == null) return false;
            return selected.GetComponent<SplineControlPoint>() && selected.GetComponentInParent<Spline>();
        }

        [MenuItem("Tools/Spline/Branching/OUT -> Branch")]
        public static void CreateOutBranch()
        {
            var selected = Selection.activeGameObject;
            var ctrlPoint = selected.GetComponent<SplineControlPoint>();
            var spline = selected.GetComponentInParent<Spline>();
            SplineControlPointEditor.CreateBranch(spline, ctrlPoint.Index, false);
        }

        [MenuItem("Tools/Spline/Branching/Branch -> IN", validate = true)]
        public static bool CanCreateInBranch()
        {
            if (SplineBaseEditor.ConvergingSpline) return false;

            var selected = Selection.activeObject as GameObject;
            if (selected == null) return false;
            return selected.GetComponent<SplineControlPoint>() && selected.GetComponentInParent<Spline>();
        }

        [MenuItem("Tools/Spline/Branching/Branch -> IN")]
        public static void CreateInBranch()
        {
            var selected = Selection.activeGameObject;
            var ctrlPoint = selected.GetComponent<SplineControlPoint>();
            var spline = selected.GetComponentInParent<Spline>();
            SplineControlPointEditor.CreateBranch(spline, ctrlPoint.Index, true);
        }

        [MenuItem("Tools/Spline/Branching/Converge", validate = true)]
        public static bool CanConverge()
        {
            if (SplineBaseEditor.ConvergingSpline) return false;

            var selected = Selection.activeObject as GameObject;
            if (selected == null) return false;
            var ctrlPoint = selected.GetComponent<SplineControlPoint>();
            var spline = selected.GetComponentInParent<Spline>();
            if (spline == null || ctrlPoint == null) return false;

            if ((ctrlPoint.Index == 0 && spline.PrevSpline == null) ||
                (ctrlPoint.Index == spline.ControlPointCount - 1 && spline.NextSpline == null))
                if (!spline.HasBranches(ctrlPoint.Index))
                    return true;

            return false;
        }

        [MenuItem("Tools/Spline/Branching/Converge")]
        public static void Converge()
        {
            var selected = Selection.activeObject as GameObject;
            SplineBaseEditor.ConvergingSpline = selected.GetComponentInParent<Spline>();
        }

        [MenuItem("Tools/Spline/Branching/Cancel Converge", validate = true)]
        public static bool CanCancelConverge()
        {
            return SplineBaseEditor.ConvergingSpline;
        }

        [MenuItem("Tools/Spline/Branching/Cancel Converge")]
        public static void CancelConverge()
        {
            SplineBaseEditor.ConvergingSpline = null;
        }

        [MenuItem("Tools/Spline/Branching/Separate", validate = true)]
        public static bool CanSeparate()
        {
            if (SplineBaseEditor.ConvergingSpline) return false;

            var selected = Selection.activeObject as GameObject;
            if (selected == null) return false;
            var ctrlPoint = selected.GetComponent<SplineControlPoint>();
            var spline = selected.GetComponentInParent<Spline>();
            if (spline == null || ctrlPoint == null) return false;

            return spline.HasBranches(ctrlPoint.Index);
        }

        [MenuItem("Tools/Spline/Branching/Separate")]
        public static void Separate()
        {
            var selected = Selection.activeObject as GameObject;
            var ctrlPoint = selected.GetComponent<SplineControlPoint>();
            var spline = selected.GetComponentInParent<Spline>();
            SplineControlPointEditor.Separate(spline, ctrlPoint.Index);
        }

        [MenuItem("Tools/Spline/Set Mode/Free", validate = true)]
        private static bool CanSetFreeMode()
        {
            return CanSetMode();
        }

        [MenuItem("Tools/Spline/Set Mode/Aligned", validate = true)]
        private static bool CanSetAlignedMode()
        {
            return CanSetMode();
        }

        [MenuItem("Tools/Spline/Set Mode/Mirrored", validate = true)]
        private static bool CanSetMirroredMode()
        {
            return CanSetMode();
        }

        private static bool CanSetMode()
        {
            if (SplineBaseEditor.ConvergingSpline) return false;

            var selected = Selection.gameObjects;
            return selected.Any(s => s.GetComponentInParent<Spline>());
        }

        [MenuItem("Tools/Spline/Set Mode/Free")]
        private static void SetFreeMode()
        {
            var gameObjects = Selection.gameObjects;
            for (var i = 0; i < gameObjects.Length; ++i) SetMode(gameObjects[i], ControlPointMode.Free);
        }

        [MenuItem("Tools/Spline/Set Mode/Aligned")]
        private static void SetAlignedMode()
        {
            var gameObjects = Selection.gameObjects;
            for (var i = 0; i < gameObjects.Length; ++i) SetMode(gameObjects[i], ControlPointMode.Aligned);
        }

        [MenuItem("Tools/Spline/Set Mode/Mirrored")]
        private static void SetMirroredMode()
        {
            var gameObjects = Selection.gameObjects;
            for (var i = 0; i < gameObjects.Length; ++i) SetMode(gameObjects[i], ControlPointMode.Mirrored);
        }

        private static void SetMode(GameObject selected, ControlPointMode mode)
        {
            var spline = selected.GetComponentInParent<Spline>();
            if (spline == null) return;

            var selectedControlPoint = selected.GetComponent<SplineControlPoint>();
            if (selectedControlPoint != null)
                SplineBaseEditor.SetMode(spline, mode, selectedControlPoint.Index);
            else
                SplineBaseEditor.SetMode(spline, mode);
        }

        [MenuItem("Tools/Spline/Append _&3", validate = true)]
        private static bool CanAppend()
        {
            if (SplineBaseEditor.ConvergingSpline) return false;

            var selected = Selection.activeObject as GameObject;
            if (selected == null) return false;

            var spline = selected.GetComponentInParent<Spline>();
            return spline != null && spline.NextSpline == null;
        }

        [MenuItem("Tools/Spline/Append _&3")]
        public static void Append()
        {
            var selected = Selection.activeObject as GameObject;
            var spline = selected.GetComponentInParent<Spline>();
            SplineEditor.Append(spline);
            Selection.activeGameObject = spline.GetSplineControlPoints().Last().gameObject;
        }

        [MenuItem("Tools/Spline/Insert _&4", validate = true)]
        private static bool CanInsert()
        {
            if (SplineBaseEditor.ConvergingSpline) return false;

            var selected = Selection.activeObject as GameObject;
            if (selected == null) return false;

            return selected.GetComponent<SplineControlPoint>();
        }

        [MenuItem("Tools/Spline/Insert _&4")]
        private static void Insert()
        {
            var selected = Selection.activeObject as GameObject;
            var spline = selected.GetComponentInParent<Spline>();
            var ctrlPoint = selected.GetComponent<SplineControlPoint>();
            SplineEditor.Insert(spline, ctrlPoint.Index);
            Selection.activeGameObject = spline.GetSplineControlPoints().ElementAt(ctrlPoint.Index + 3).gameObject;
        }

        [MenuItem("Tools/Spline/Prepend _&5", validate = true)]
        private static bool CanPrepend()
        {
            if (SplineBaseEditor.ConvergingSpline) return false;

            var selected = Selection.activeObject as GameObject;
            if (selected == null) return false;

            var spline = selected.GetComponentInParent<Spline>();
            return spline != null && spline.PrevSpline == null;
        }

        [MenuItem("Tools/Spline/Prepend _&5")]
        public static void Prepend()
        {
            var selected = Selection.activeObject as GameObject;
            var spline = selected.GetComponentInParent<Spline>();
            SplineEditor.Prepend(spline);
            Selection.activeGameObject = spline.GetSplineControlPoints().First().gameObject;
        }

        [MenuItem("Tools/Spline/Remove Curve", validate = true)]
        private static bool CanRemove()
        {
            if (SplineBaseEditor.ConvergingSpline) return false;

            var selected = Selection.activeObject as GameObject;
            if (selected == null) return false;

            return selected.GetComponent<SplineControlPoint>() && selected.GetComponentInParent<Spline>();
        }

        [MenuItem("Tools/Spline/Remove Curve")]
        private static void Remove()
        {
            var selected = Selection.activeObject as GameObject;
            var ctrlPoint = selected.GetComponent<SplineControlPoint>();
            var spline = selected.GetComponentInParent<Spline>();
            Selection.activeGameObject = spline.gameObject;
            SplineControlPointEditor.Remove(spline, ctrlPoint.Index);
        }

        [MenuItem("Tools/Spline/Smooth", validate = true)]
        private static bool CanSmooth()
        {
            if (SplineBaseEditor.ConvergingSpline) return false;

            var selected = Selection.activeObject as GameObject;
            if (selected == null) return false;

            return selected.GetComponentInParent<Spline>();
        }

        [MenuItem("Tools/Spline/Smooth")]
        private static void Smooth()
        {
            var selected = Selection.activeObject as GameObject;
            var spline = selected.GetComponentInParent<Spline>();
            SplineBaseEditor.Smooth(spline);
        }


        [MenuItem("Tools/Spline/ZTest/Turn On", validate = true)]
        private static bool CanZTestOn()
        {
            return !SplineBase.SplineMaterialZTest;
        }

        [MenuItem("Tools/Spline/ZTest/Turn Off", validate = true)]
        private static bool CanZTestOff()
        {
            return SplineBase.SplineMaterialZTest;
        }

        [MenuItem("Tools/Spline/ZTest/Turn On")]
        private static void ZTestOn()
        {
            SplineBase.SplineMaterialZTest = true;
        }

        [MenuItem("Tools/Spline/ZTest/Turn Off")]
        private static void ZTestOff()
        {
            SplineBase.SplineMaterialZTest = false;
        }

        [MenuItem("Tools/Spline/Save In Play Mode/Enable", validate = true)]
        private static bool CanEnableSavePlayMode()
        {
            var selected = Selection.activeObject as GameObject;
            if (selected == null) return false;
            var spline = selected.GetComponentInParent<Spline>();
            if (spline == null) return false;

            if (selected.GetComponent<SaveInPlayMode>() != null) return false;

            return true;
        }

        [MenuItem("Tools/Spline/Save In Play Mode/Enable")]
        private static void EnableSavePlayMode()
        {
            var selected = Selection.activeObject as GameObject;
            Undo.RegisterCreatedObjectUndo(selected.AddComponent<SaveInPlayMode>(),
                "Tools.Spline.SaveInPlayMode.Enable");
        }

        [MenuItem("Tools/Spline/Save In Play Mode/Disable", validate = true)]
        private static bool CanDisableSavePlayMode()
        {
            var selected = Selection.activeObject as GameObject;
            if (selected == null) return false;
            var spline = selected.GetComponentInParent<Spline>();
            if (spline == null) return false;

            if (selected.GetComponent<SaveInPlayMode>() == null) return false;

            return true;
        }

        [MenuItem("Tools/Spline/Save In Play Mode/Disable")]
        private static void DisableSavePlayMode()
        {
            var selected = Selection.activeObject as GameObject;
            Undo.DestroyObjectImmediate(selected.GetComponent<SaveInPlayMode>());
        }
    }
}