using UnityEngine;

namespace Battlehub.RTEditor
{
    [AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
    public class MouseOrbit : MonoBehaviour
    {
        public Transform Target;
        public float Distance = 5.0f;
        public float XSpeed = 5.0f;
        public float YSpeed = 5.0f;

        public float YMinLimit = -360f;
        public float YMaxLimit = 360f;

        public float DistanceMin = .5f;
        public float DistanceMax = 5000f;
        private Camera m_camera;

        private float m_x = 0.0f;
        private float m_y = 0.0f;

        private void Awake()
        {
            m_camera = GetComponent<Camera>();
        }

        private void Start()
        {
            SyncAngles();
        }

        private void LateUpdate()
        {
            var deltaX = Input.GetAxis("Mouse X");
            var deltaY = Input.GetAxis("Mouse Y");

            deltaX = deltaX * XSpeed;
            deltaY = deltaY * YSpeed;

            m_x += deltaX;
            m_y -= deltaY;
            m_y = ClampAngle(m_y, YMinLimit, YMaxLimit);

            Zoom();
        }

        public void SyncAngles()
        {
            var angles = transform.eulerAngles;
            m_x = angles.y;
            m_y = angles.x;
        }

        public void Zoom()
        {
            var rotation = Quaternion.Euler(m_y, m_x, 0);
            transform.rotation = rotation;

            var mwheel = Input.GetAxis("Mouse ScrollWheel");

            if (m_camera.orthographic)
            {
                m_camera.orthographicSize -= mwheel * m_camera.orthographicSize;
                if (m_camera.orthographicSize < 0.01f) m_camera.orthographicSize = 0.01f;
            }

            Distance = Mathf.Clamp(Distance - mwheel * Distance, DistanceMin, DistanceMax);
            var negDistance = new Vector3(0.0f, 0.0f, -Distance);
            var position = rotation * negDistance + Target.position;
            transform.position = position;
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F) angle += 360F;
            if (angle > 360F) angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }
    }
}