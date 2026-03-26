using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationExitTrigger : MonoBehaviour
{
    public GameObject returnTrigger;
    public float delay = 2f;

    [SerializeField] private List<Fire> fireScripts = new List<Fire>();
    [SerializeField] private float resetIntensity = 1.0f;

    private bool canTrigger = true; 

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Fire Appliance")) return;

        if (!canTrigger) return; 

        canTrigger = false;

        if (returnTrigger != null)
        {
            StartCoroutine(ActivateTriggerAfterDelay());
        }

        foreach (Fire fire in fireScripts)
        {
            if (fire != null)
            {
                fire.Reignite(resetIntensity);
            }
        }

        StartCoroutine(ResetTrigger());
    }

    IEnumerator ActivateTriggerAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        returnTrigger.SetActive(true);
    }

    IEnumerator ResetTrigger()
    {
        yield return new WaitForSeconds(2f);
        canTrigger = true;
    }
}