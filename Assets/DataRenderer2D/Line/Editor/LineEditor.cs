using UnityEditor;
using UnityEngine;

namespace geniikw.DataRenderer2D.Editors
{
    public class LineEditor : Editor
    {
        //private LineInpectorHandler _inspector;
        private MonoBehaviour _owner;
        private PointHandler _pointHandler;

        protected void OnEnable()
        {
            _owner = target as MonoBehaviour;

            _pointHandler = new PointHandler(_owner, serializedObject);
            //_inspector = new LineInpectorHandler(serializedObject);
        }

        protected void OnSceneGUI()
        {
            _pointHandler.OnSceneGUI();
        }
    }

    [CustomEditor(typeof(WorldLine))]
    public class WorldLineEditor : LineEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("MakeNewMesh")) ((WorldLine)target).MakeNewMesh();
        }
    }

    [CustomEditor(typeof(GizmoLine))]
    public class NoRenderLineEditor : LineEditor
    {
    }
}