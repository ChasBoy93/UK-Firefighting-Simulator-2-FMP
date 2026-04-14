using UnityEngine;

public class StationReturnTrigger : MonoBehaviour
{
    public CallManager callManager;

    private bool used = false;

    void OnEnable()
    {
        used = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (used)
        {
            return;

        }
        if (!other.CompareTag("Fire Appliance"))
        {
            return;
        }

        used = true;

        if (callManager != null)
        {
            callManager.ReturnToStation();
        }

        gameObject.SetActive(false);
    }
}