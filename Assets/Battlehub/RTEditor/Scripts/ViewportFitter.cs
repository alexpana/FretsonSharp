﻿using UnityEngine;
using UnityEngine.Events;

namespace Battlehub.RTEditor
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    public class ViewportFitter : MonoBehaviour
    {
        public UnityEvent ViewportRectChanged;
        public Camera Camera;

        private RectTransform m_viewport;
        private float m_viewportHeight;
        private Vector3 m_viewportPosition;
        private float m_viewportWidth;

        private void Awake()
        {
            m_viewport = GetComponent<RectTransform>();
            if (Camera == null) Camera = Camera.main;
            if (Camera == null)
            {
                Debug.LogWarning("Set Camera");
                return;
            }

            var canvas = m_viewport.GetComponentInParent<Canvas>();
            if (canvas == null)
            {
                gameObject.SetActive(false);
                return;
            }

            if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            {
                gameObject.SetActive(false);
                Debug.LogWarning("ViewportFitter requires canvas.renderMode -> RenderMode.ScreenSpaceOverlay");
                return;
            }

            Camera.pixelRect = new Rect(new Vector2(0, 0), new Vector2(Screen.width, Screen.height));
        }

        private void Start()
        {
            var rect = m_viewport.rect;
            UpdateViewport();
            m_viewportHeight = rect.height;
            m_viewportWidth = rect.width;
            m_viewportPosition = m_viewport.position;
        }

        private void OnEnable()
        {
            var rect = m_viewport.rect;
            UpdateViewport();
            m_viewportHeight = rect.height;
            m_viewportWidth = rect.width;
            m_viewportPosition = m_viewport.position;
        }

        private void OnDisable()
        {
            if (Camera != null)
            {
                Camera.rect = new Rect(0, 0, 1, 1);
                ViewportRectChanged.Invoke();
            }
        }

        private void OnGUI()
        {
            if (m_viewport != null)
            {
                var rect = m_viewport.rect;
                if (m_viewportHeight != rect.height || m_viewportWidth != rect.width ||
                    m_viewportPosition != m_viewport.position)
                {
                    UpdateViewport();
                    m_viewportHeight = rect.height;
                    m_viewportWidth = rect.width;
                    m_viewportPosition = m_viewport.position;
                }
            }
        }

        private void UpdateViewport()
        {
            if (Camera == null) return;

            var corners = new Vector3[4];
            m_viewport.GetWorldCorners(corners);
            Camera.pixelRect = new Rect(corners[0],
                new Vector2(corners[2].x - corners[0].x, corners[1].y - corners[0].y));

            ViewportRectChanged.Invoke();
        }
    }
}