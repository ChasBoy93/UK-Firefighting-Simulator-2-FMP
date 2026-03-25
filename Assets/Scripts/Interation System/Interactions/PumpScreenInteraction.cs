using Artemis;
using UnityEngine;

public class PumpScreenInteraction : MonoBehaviour, IInteractable
{
    public GameObject pumpScreenUi;
    public GameObject playerCameraStop;

    public void Interact()
    {
        OpenScreen();
    }

    public void OpenScreen()
    {
        pumpScreenUi.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerCameraStop.GetComponent<FPController>().enabled = false;
    }

    public string GetInteractionText()
    {
        return "Use Pump Screen";
    }

    public void ClosePumpScreen()
    {
        pumpScreenUi.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerCameraStop.GetComponent<FPController>().enabled = true;
    }
}
