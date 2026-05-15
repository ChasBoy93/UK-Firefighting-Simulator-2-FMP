using UnityEngine;

public class ToggleCutters : MonoBehaviour
{
    public GameObject cutters;
    bool isCuttersOn;
    public void ToggleTheCutters()
    {
        if(isCuttersOn)
        {
            cutters.SetActive(false);
            isCuttersOn = false;
        }
        else
        {
            cutters.SetActive(true);
            isCuttersOn = true;
        }   
    }
}
