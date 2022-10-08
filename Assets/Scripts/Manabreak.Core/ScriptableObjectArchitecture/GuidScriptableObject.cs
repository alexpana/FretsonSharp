using System;
using UnityEngine;

namespace Core.ScriptableObjectArchitecture
{
    /// <summary>
    ///     ScriptableObject that stores a GUID for unique identification. The population of this field is implemented
    ///     inside an Editor script.
    /// </summary>
    [Serializable]
    public abstract class GuidScriptableObject : ScriptableObject
    {
        [HideInInspector] [SerializeField] private byte[] m_Guid;

        public Guid Guid => new(m_Guid);

        private void OnValidate()
        {
            if (m_Guid.Length == 0) m_Guid = Guid.NewGuid().ToByteArray();
        }
    }
}