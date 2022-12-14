using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
using UnityEditor;
#endif

namespace Battlehub.Utils
{
    [ExecuteInEditMode]
    public class SaveInPlayMode : MonoBehaviour
    {
#if UNITY_EDITOR
        private const string m_root = "Battlehub/SaveInPlayMode";
        private const string m_path = "Assets/" + m_root + "/Prefabs/P";
        private string m_name;
        private GameObject m_go;
        private GameObject m_prevPrefabObject;

        private readonly HashSet<Object> m_destroyObjects = new();

        public void ScheduleDestroy(Object o)
        {
            if (!m_destroyObjects.Contains(o)) m_destroyObjects.Add(o);
        }

        private void Awake()
        {
            if (!Application.isPlaying)
            {
                var prefabType = PrefabUtility.GetPrefabType(gameObject);
                if (prefabType == PrefabType.Prefab || prefabType == PrefabType.PrefabInstance)
                    m_prevPrefabObject = (GameObject)PrefabUtility.GetPrefabParent(gameObject);

                var prefab =
                    (GameObject)AssetDatabase.LoadAssetAtPath(m_path + GetInstanceID() + ".prefab", typeof(GameObject));
                m_name = gameObject.name;
                if (prefab != null)
                {
                    m_go = PrefabUtility.ConnectGameObjectToPrefab(gameObject, prefab);


                    EditorApplication.delayCall += Cleanup;
                }
            }
        }

        private void Cleanup()
        {
            EditorApplication.delayCall -= Cleanup;

            m_go.SetActive(true);
            m_go.name = m_name;

            PrefabUtility.DisconnectPrefabInstance(m_go);
            AssetDatabase.DeleteAsset(m_path + GetInstanceID() + ".prefab");

            if (m_prevPrefabObject != null)
            {
                PrefabUtility.ReplacePrefab(m_go, m_prevPrefabObject, ReplacePrefabOptions.ConnectToPrefab);
                PrefabUtility.ReconnectToLastPrefab(m_go);
            }

            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }

        private bool m_isApplicationQuit = false;

        private void OnApplicationQuit()
        {
            foreach (var obj in m_destroyObjects)
                if (obj != null)
                    DestroyImmediate(obj);

            m_isApplicationQuit = true;
        }

        private void OnDestroy()
        {
            if (m_isApplicationQuit)
            {
                var path = m_path + GetInstanceID();
                if (!Directory.Exists(Application.dataPath + "/" + m_root + "/Prefabs"))
                    AssetDatabase.CreateFolder("Assets/" + m_root, "Prefabs");

                var filters = gameObject.GetComponentsInChildren<MeshFilter>();
                for (var i = 0; i < filters.Length; ++i)
                {
                    var filter = filters[i];
                    var mesh = filter.sharedMesh;

                    if (string.IsNullOrEmpty(AssetDatabase.GetAssetPath(mesh)))
                    {
                        //AssetDatabase.CreateAsset(mesh, AssetDatabase.GenerateUniqueAssetPath(path + "mesh"));
                    }

                    AssetDatabase.SaveAssets();
                }


                Undo.ClearUndo(gameObject);
                PrefabUtility.CreatePrefab(path + ".prefab", gameObject, ReplacePrefabOptions.Default);
            }
        }
#endif
    }
}