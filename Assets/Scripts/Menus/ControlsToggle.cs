using UnityEngine;

public class ControlsToggle : MonoBehaviour
{
    public GameObject truckControlsUi;
    bool isControlsOn;

    public void ToggleControls()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(isControlsOn == false)
            {
                truckControlsUi.SetActive(true);
                isControlsOn = true;
            }
            else if(isControlsOn == true)
            {
                truckControlsUi.SetActive(false);
                isControlsOn = false;
            }
        }
    }
}
