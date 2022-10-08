using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Battlehub.RTHandles
{
    public interface IGL
    {
        void Draw();
    }

    [ExecuteInEditMode]
    public class GLRenderer : MonoBehaviour
    {
        private List<IGL> m_renderObjects;

        public static GLRenderer Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null) Debug.LogWarning("Another instance of GLLinesRenderer aleready exist");
            Instance = this;

            m_renderObjects = new List<IGL>();
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public void Add(IGL gl)
        {
            if (m_renderObjects.Contains(gl)) return;

            m_renderObjects.Add(gl);
        }

        public void Remove(IGL line)
        {
            m_renderObjects.Remove(line);
        }

        public void Draw()
        {
            if (m_renderObjects == null) return;

            GL.PushMatrix();
            try
            {
                for (var i = 0; i < m_renderObjects.Count; ++i)
                {
                    var line = m_renderObjects[i];
                    line.Draw();
                }
            }
            finally
            {
                GL.PopMatrix();
            }
        }


#if UNITY_EDITOR
        private void Update()
        {
            if (Instance == null)
            {
                Instance = this;
                m_renderObjects = new List<IGL>();
            }
        }

        [DidReloadScripts(99)]
        private static void OnScriptsReloaded()
        {
            if (Instance == null)
            {
                var glRenderer = FindObjectOfType<GLRenderer>();
                if (glRenderer != null)
                {
                    glRenderer.m_renderObjects = new List<IGL>();
                    Instance = glRenderer;
                }
            }
        }
#endif
    }
}