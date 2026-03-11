using UnityEngine;

public class PumpBayDoorInteraction : MonoBehaviour, IInteractable
{
    public Animator pumpBayDoorAnimator;

    public string openAnimation = "Pump Bay Door Open";
    public string closeAnimation = "Pump Bay Door Close";

    public bool isOpen = false;

    public void Interact()
    {
        ToggleDoors();
    }

    void ToggleDoors()
    {
        if (isOpen)
        {
            pumpBayDoorAnimator.Play(closeAnimation);
        }
        else
        {
            pumpBayDoorAnimator.Play(openAnimation);
        }

        isOpen = !isOpen;
    }

    public string GetInteractionText()
    {
        if (isOpen)
            return "Close Pump Bay Door";

        return "Open Pump Bay Door";
    }
}