using UnityEngine;

public class ControlsToggle : MonoBehaviour
{
    public GameObject truckControlsUi;
    bool isControlsOn = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isControlsOn = !isControlsOn;
            truckControlsUi.SetActive(isControlsOn);
        }
    }
}