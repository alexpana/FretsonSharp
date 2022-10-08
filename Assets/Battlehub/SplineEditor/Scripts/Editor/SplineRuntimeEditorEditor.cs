using Battlehub.Utils;
using UnityEditor;

namespace Battlehub.SplineEditor
{
    [CustomEditor(typeof(SplineRuntimeEditor))]
    public class SplineRuntimeEditorEditor : Editor
    {
        private PropertyField[] m_fields;
        private SplineRuntimeEditor m_instance;

        public void OnEnable()
        {
            m_instance = (SplineRuntimeEditor)target;
            m_fields = ExposeProperties.GetProperties(m_instance);
        }

        public override void OnInspectorGUI()
        {
            if (m_instance == null) return;

            DrawDefaultInspector();
            ExposeProperties.Expose(m_fields);
        }
    }
}