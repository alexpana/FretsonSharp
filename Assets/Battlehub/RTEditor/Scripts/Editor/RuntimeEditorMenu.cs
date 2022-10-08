using UnityEditor;
using UnityEngine;

namespace Battlehub.RTEditor
{
    public static class RTEditorMenu
    {
        private const string root = "Battlehub/RTEditor/";

        public static GameObject InstantiateRuntimeEditor()
        {
            return InstantiatePrefab("RuntimeEditor.prefab");
        }


        public static GameObject InstantiatePrefab(string name)
        {
            var prefab = AssetDatabase.LoadAssetAtPath("Assets/" + root + "Prefabs/" + name, typeof(GameObject));
            return (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        }
    }
}