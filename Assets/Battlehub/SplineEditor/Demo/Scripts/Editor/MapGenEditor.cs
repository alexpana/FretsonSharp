using UnityEditor;
using UnityEngine;

namespace Battlehub.SplineEditor
{
    [CustomEditor(typeof(MapGen))]
    public class MapGenEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Generate"))
            {
                var gen = (MapGen)target;
                gen.Generate();
            }
        }
    }
}