using UnityEngine;
using UnityEngine.AI;

namespace Game.FireStationNavigation
{
    public class Area : MonoBehaviour
    {
        public float radius = 20f;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public Vector3 GetRandomPoint()
        {
            Vector3 randomDestination = Random.insideUnitSphere * radius;
            randomDestination.y = 0;

            Vector3 randomPoint = transform.position + randomDestination;

            NavMeshHit hit;
            Vector3 finalPosition = transform.position;

            if(NavMesh.SamplePosition(randomPoint, out hit, 2f, 1))
            {
                finalPosition = hit.position;
            }

            return finalPosition;
        }
    }
}
