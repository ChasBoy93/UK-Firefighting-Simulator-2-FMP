using Artemis;
using UnityEngine;

public class IncidentWriteUpInteraction : MonoBehaviour, IInteractable
{
    public GameObject incidentWriteUpMenu;
    public GameObject playerCameraStop;

    public void Interact()
    {
        OpenWriteUpMenu();
    }

    void OpenWriteUpMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        incidentWriteUpMenu.SetActive(true);
        playerCameraStop.GetComponent<FPController>().enabled = false;
    }

    public string GetInteractionText()
    {
        return "Open Incident Write-Up";
    }
}
