using UnityEngine;
using UnityEngine.AI;

namespace Game.FireStationNavigation
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class NPC : MonoBehaviour
    {
        [HideInInspector]
        public NavMeshAgent agent;

        [HideInInspector]
        public Animator animator;

        public float CurrentSpeed
        {
            get { return agent.velocity.magnitude; }
        }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }
    }
}
