using System;
using UnityEditor;
using UnityEngine;

namespace geniikw.DataRenderer2D.Editors
{
    public class EditorSetting : ScriptableObject
    {
        public static EditorSetting m_instance;

        [Header("if you want to edit position in inspector, set false")]
        public bool onlyViewWidth = true;

        public static EditorSetting Get
        {
            get
            {
                if (m_instance == null)
                {
                    var find = AssetDatabase.FindAssets("t:EditorSetting", null);

                    if (find.Length == 0)
                        throw new Exception("Can't find setting file");

                    m_instance = AssetDatabase.LoadAssetAtPath<EditorSetting>(AssetDatabase.GUIDToAssetPath(find[0]));
                }

                return m_instance;
            }
        }
    }
}