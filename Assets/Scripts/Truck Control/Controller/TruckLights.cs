using UnityEngine;
using System.Collections.Generic;

public class TruckLights : MonoBehaviour
{
    public enum Side
    {
        Back
    }

    [System.Serializable]
    public struct Light
    {
        public GameObject lightObj;
        public Side side;
    }

    public List<Light> lights;

    private bool isBraking = false;

    public void SetBraking(bool braking)
    {
        isBraking = braking;
        UpdateBrakeLights();
    }

    private void UpdateBrakeLights()
    {
        foreach (var light in lights)
        {
            if (light.side == Side.Back)
            {
                light.lightObj.SetActive(isBraking);
            }
        }
    }
}