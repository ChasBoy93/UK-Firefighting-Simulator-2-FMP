using game.FireStationNavigation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.FireStationNavigation
{
    public class NPCGenerator : MonoBehaviour
    {
        [SerializeField] private NPC[] npcPrefabs;
        [SerializeField] private Area[] areas;
        [SerializeField] private int count = 5;

        private void Start()
        {
            if (npcPrefabs == null || npcPrefabs.Length == 0 || areas == null || areas.Length == 0)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                Area chosenArea = areas[Random.Range(0, areas.Length)];
                Vector3 position = chosenArea.GetRandomPoint();
                Quaternion rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0);

                NPC prefab = npcPrefabs[Random.Range(0, npcPrefabs.Length)];
                NPC npc = Instantiate(prefab, position, rotation);

                var wander = npc.GetComponent<NPCWander>();
                if (wander != null)
                {
                    wander.area = chosenArea;
                }
            }
        }
    }
}
