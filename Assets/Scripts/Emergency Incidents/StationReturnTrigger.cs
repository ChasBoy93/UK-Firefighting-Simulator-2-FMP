using UnityEngine;

public class StationReturnTrigger : MonoBehaviour
{
    public CallManager callManager;

    private bool used = false; 

    void OnTriggerEnter(Collider other)
    {
        if (used) return; 

        if (!other.CompareTag("Fire Appliance")) return;

        used = true; 

        if (callManager != null)
        {
            callManager.ReturnToStation();
        }


        Invoke("DisableTrigger", 0.2f);
    }

    void DisableTrigger()
    {
        gameObject.SetActive(false);
    }

    public void ResetTrigger()
    {
        used = false;
    }
}