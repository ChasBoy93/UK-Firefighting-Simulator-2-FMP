using System.Collections.Generic;
using UnityEngine;

public class StationExitTrigger : MonoBehaviour
{
    public CallManager callManager;
    public List<GameObject> waypointIndicators;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Fire Appliance"))
        {
            return;
        }

        if (callManager != null)
        {
            callManager.HandleStationExit();
        }

        foreach (GameObject waypoint in waypointIndicators)
        {
            if (waypoint != null)
            {
                waypoint.SetActive(true);
            }
        }
    }
}