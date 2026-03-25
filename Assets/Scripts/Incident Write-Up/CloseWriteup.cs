using Artemis;
using UnityEngine;

public class CloseWriteup : MonoBehaviour
{
    public GameObject incidentWriteUpMenu;
    public GameObject playerCameraStop;

    public void CloseMenu()
    {
        incidentWriteUpMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerCameraStop.GetComponent<FPController>().enabled = true;
    }
}
