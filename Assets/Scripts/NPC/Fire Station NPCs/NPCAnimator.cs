using UnityEngine;

namespace Game.FireStationNavigation
{
    public class NPCAnimator : NPCComponent
    {
        private void Update()
        {
            npc.animator.SetFloat("Speed", npc.CurrentSpeed);
        }
    }
}