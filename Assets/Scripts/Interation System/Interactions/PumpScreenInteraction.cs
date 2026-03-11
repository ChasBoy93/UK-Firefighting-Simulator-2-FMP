using UnityEngine;

public class PumpScreenInteraction : MonoBehaviour, IInteractable
{
    public GameObject pumpScreenUi;

    public void Interact()
    {
        OpenScreen();
    }

    void OpenScreen()
    {
        pumpScreenUi.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
    }

}
