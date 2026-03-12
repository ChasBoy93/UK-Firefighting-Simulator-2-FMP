using System.Collections.Generic;
using UnityEngine;

public class Incident : MonoBehaviour
{
    public string incidentName;

    public List<GameObject> incidentObjects;

    CallManager manager;

    public void StartIncident(CallManager m)
    {
        manager = m;

        foreach (GameObject obj in incidentObjects)
        {
            obj.SetActive(true);
        }
    }

    public void EndIncident()
    {
        foreach (GameObject obj in incidentObjects)
        {
            obj.SetActive(false);
        }
    }
}