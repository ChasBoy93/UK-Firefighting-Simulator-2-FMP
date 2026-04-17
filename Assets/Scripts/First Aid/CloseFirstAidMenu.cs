using Artemis;
using UnityEngine;

public class CloseFirstAidMenu : MonoBehaviour
{
    public GameObject firstAidMenu;
    public GameObject playerCameraStop;
    public void CloseMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        firstAidMenu.SetActive(false);
        playerCameraStop.GetComponent<FPController>().enabled = true;
    }
}
