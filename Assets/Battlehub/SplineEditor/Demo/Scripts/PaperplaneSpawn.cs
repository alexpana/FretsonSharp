using UnityEngine;

namespace Battlehub.SplineEditor
{
    public class PaperplaneSpawn : MonoBehaviour
    {
        public float Interval = 2.0f;

        public SmoothFollow SmoothFollow;
        public GameObject PaperplanePrefab;
        public string SplineName = "Spline";
        private SplineBase m_spline;
        private float m_timeElapsed;

        private void Start()
        {
            if (m_spline == null)
            {
                var go = GameObject.Find(SplineName);
                if (go != null) m_spline = go.GetComponent<SplineBase>();

                if (m_spline == null)
                {
                    Debug.LogError("Unable to find spline " + m_spline);
                    enabled = false;
                    return;
                }
            }

            Spawn();
        }


        private void Update()
        {
            m_timeElapsed += Time.deltaTime;
            if (m_timeElapsed >= Interval)
            {
                Spawn();
                m_timeElapsed = 0;
            }
        }

        private void Spawn()
        {
            var index = 0;
            var nextIndex = index + 1;
            var twist = m_spline.GetTwist(index);
            var ptPrev = m_spline.GetControlPoint(index);
            var pt = m_spline.GetControlPoint(nextIndex);
            var paperplaneGo = Instantiate(PaperplanePrefab, m_spline.GetPoint(0.0f),
                Quaternion.AngleAxis(twist.Data, pt - ptPrev) * Quaternion.LookRotation(pt - ptPrev));
            var splineFollow = paperplaneGo.GetComponent<SplineFollow>();
            splineFollow.Spline = m_spline;

            if (!SmoothFollow.enabled)
            {
                SmoothFollow.SetTarget(paperplaneGo.transform);
                SmoothFollow.enabled = true;
            }
        }
    }
}