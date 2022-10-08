using UnityEditor;
using UnityEditor.UI;

namespace geniikw.DataRenderer2D.Hole
{
    [CustomEditor(typeof(UIHole), true)]
    public class HoleEditor : ImageEditor
    {
        private SerializedProperty m_holeInfo;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_holeInfo = serializedObject.FindProperty("hole");
        }


        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(m_holeInfo, true);
            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();
        }
    }
}