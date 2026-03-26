using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CallManager : MonoBehaviour
{
    public List<Incident> incidents;

    public List<AudioSource> stationAlarms;
    public List<AudioSource> applianceAlarms;

    public List<GameObject> lightTowers;
    public List<GameObject> stationLights;

    public Material emissionMaterial;
    public Texture emissionTexture;

    public float minCallTime = 60f;
    public float maxCallTime = 180f;

    public float lightTowerTime = 60f;

    bool stationAvailable = true;

    public GameObject IcAlarmLightAnim;

    Incident activeIncident;

    public MDTController mdt;

    void Start()
    {
        StartCoroutine(CallRoutine());
    }

    void Update()
    {
        if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            AcknowledgeCall();
        }
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

        if (mdt != null)
        {
            mdt.ReceiveCall(activeIncident.incidentName);
        }

        stationAvailable = false;

        foreach (AudioSource alarm in stationAlarms)
        {
            alarm.Play();
        }

        foreach (AudioSource alarm in applianceAlarms)
        {
            alarm.Play();
        }

        foreach (GameObject tower in lightTowers)
        {
            tower.SetActive(true);
        }

        foreach (GameObject stnlights in stationLights)
        {
            stnlights.SetActive(true);
        }

        IcAlarmLightAnim.SetActive(true);

        SetEmission(true);

        StartCoroutine(LightTowerTimer());
    }

    IEnumerator LightTowerTimer()
    {
        yield return new WaitForSeconds(lightTowerTime);

        foreach (GameObject tower in lightTowers)
        {
            tower.SetActive(false);
        }

        foreach (GameObject stnlights in stationLights)
        {
            stnlights.SetActive(false);
        }

        SetEmission(false);
    }

    public void AcknowledgeCall()
    {
        foreach (AudioSource alarm in applianceAlarms)
        {
            alarm.Stop();
        }

        IcAlarmLightAnim.SetActive(false);
    }

    public void ReturnToStation()
    {

        if (activeIncident != null)
        {
            activeIncident.EndIncident();
            activeIncident = null;
        }


        foreach (AudioSource alarm in applianceAlarms)
        {
            alarm.Stop();
        }

        IcAlarmLightAnim.SetActive(false);


        foreach (GameObject tower in lightTowers)
        {
            tower.SetActive(false);
        }

        foreach (GameObject stnlights in stationLights)
        {
            stnlights.SetActive(false);
        }

        SetEmission(false);


        if (mdt != null)
        {
            mdt.ResetMDT();
        }

        stationAvailable = true;
    }

    void SetEmission(bool state)
    {
        if (state)
        {
            emissionMaterial.EnableKeyword("_EMISSION");
            emissionMaterial.SetTexture("_EmissionMap", emissionTexture);
        }
        else
        {
            emissionMaterial.DisableKeyword("_EMISSION");
            emissionMaterial.SetTexture("_EmissionMap", null);
        }
    }
}