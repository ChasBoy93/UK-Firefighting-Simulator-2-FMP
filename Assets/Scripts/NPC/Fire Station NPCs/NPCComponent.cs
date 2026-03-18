using UnityEngine;

namespace Game.FireStationNavigation
{
    public class NPCComponent : MonoBehaviour
    {
        protected NPC npc;

        protected virtual void Awake()
        {
            npc = GetComponentInParent<NPC>();
        }
    }
}

