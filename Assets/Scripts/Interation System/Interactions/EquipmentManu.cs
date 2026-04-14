using UnityEngine;
using Artemis;

public class EquipmentManu : MonoBehaviour, IInteractable
{
    public GameObject equipmentMenu;
    public GameObject playerCameraStop;

    public void Interact()
    {
        OpenIncidentMenu();
    }

    void OpenIncidentMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        equipmentMenu.SetActive(true);
        playerCameraStop.GetComponent<FPController>().enabled = false;
    }

    public void CloseIncidentMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        equipmentMenu.SetActive(false);
        playerCameraStop.GetComponent<FPController>().enabled = true;
    }

    public string GetInteractionText()
    {
        return "Open Equipment Menu";
    }
}
