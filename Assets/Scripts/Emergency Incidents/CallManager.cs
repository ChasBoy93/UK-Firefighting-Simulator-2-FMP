using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VolumeComponent;

public class CallManager : MonoBehaviour
{
    public List<Incident> incidents;

    public List<AudioSource> stationAlarms;
    public List<AudioSource> applianceAlarms;

    public List<GameObject> lightTowers;

    public float minCallTime = 60f;
    public float maxCallTime = 180f;

    public float lightTowerTime = 60f;

    bool stationAvailable = true;

    Incident activeIncident;

    void Start()
    {
        StartCoroutine(CallRoutine());
    }

    IEnumerator CallRoutine()
    {
        while (true)
        {
            if (stationAvailable)
            {
                float wait = Random.Range(minCallTime, maxCallTime);
                yield return new WaitForSeconds(wait);

                TriggerCall();
            }

            yield return null;
        }
    }

    void TriggerCall()
    {
        int index = Random.Range(0, incidents.Count);

        activeIncident = incidents[index];
        activeIncident.StartIncident(this);

        stationAvailable = false;

        foreach (AudioSource alarm in stationAlarms)
            alarm.Play();

        foreach (AudioSource alarm in applianceAlarms)
            alarm.Play();

        foreach (GameObject tower in lightTowers)
            tower.SetActive(true);

        StartCoroutine(LightTowerTimer());
    }

    IEnumerator LightTowerTimer()
    {
        yield return new WaitForSeconds(lightTowerTime);

        foreach (GameObject tower in lightTowers)
            tower.SetActive(false);
    }

    public void AcknowledgeCall()
    {
        foreach (AudioSource alarm in applianceAlarms)
            alarm.Stop();
    }

    public void ReturnToStation()
    {
        if (activeIncident != null)
            activeIncident.EndIncident();

        stationAvailable = true;
    }
}