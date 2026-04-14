using UnityEngine;

public class StationExitTrigger : MonoBehaviour
{
    public CallManager callManager;

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
    }
}