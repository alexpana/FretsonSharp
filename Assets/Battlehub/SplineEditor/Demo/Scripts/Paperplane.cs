using UnityEngine;

namespace Battlehub.SplineEditor
{
    public class Paperplane : MonoBehaviour
    {
        public GameObject ExplosionPrefab;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                    if (hit.transform == transform)
                        Explode();
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            Explode();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void Explode()
        {
            if (ExplosionPrefab != null)
            {
                var explosion = Instantiate(ExplosionPrefab, transform.position, transform.rotation);
                Destroy(explosion, 3.0f);
            }


            Destroy(gameObject);
        }
    }
}