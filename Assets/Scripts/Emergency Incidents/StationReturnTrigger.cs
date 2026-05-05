using System.Collections.Generic;
using UnityEngine;

public class StationReturnTrigger : MonoBehaviour
{
    public CallManager callManager;

    private bool used = false;

    public List<GameObject> incidentInstructions;
    public List<Fire> fires;

    void OnEnable()
    {
        used = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (used) return;

        if (!other.CompareTag("Fire Appliance")) return;

        used = true;

        if (callManager != null)
        {
            callManager.ReturnToStation();
        }

        gameObject.SetActive(false);

        foreach (GameObject instructions in incidentInstructions)
        {
            if (instructions != null)
                instructions.SetActive(false);
        }

        foreach (Fire fire in fires)
        {
            if (fire != null)
                fire.ResetFire();
        }
    }
}