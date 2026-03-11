using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;

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

    public bool isRearLightsOn;

    public List<Light> lights;

    void Start()
    {
        isRearLightsOn = false;
    }

    public void OperateRearLights()
    {
        if(isRearLightsOn)
        {
            // Turn Lights On
            foreach (Light light in lights)
            {
                if (light.side == Side.Back && light.lightObj.activeInHierarchy == false)
                {
                    light.lightObj.SetActive(true);
                }
            }
        }
        else
        {
            // Turn Lights Off
            foreach (Light light in lights)
            {
                if (light.side == Side.Back && light.lightObj.activeInHierarchy == true)
                {
                    light.lightObj.SetActive(false);
                }
            }
        }
    }
}
