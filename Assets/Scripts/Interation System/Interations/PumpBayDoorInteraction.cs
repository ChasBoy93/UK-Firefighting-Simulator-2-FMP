using UnityEngine;

public class PumpBayDoorInteraction : MonoBehaviour, IInteractable
{

    [SerializeField] private Animator pumpBayDoors;

    private bool _isOpen = false;
    public bool CanInteract()
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {
       if(_isOpen)
        {
            pumpBayDoors.Play("Pump Bay Door Open");
        }
       else
        {
            pumpBayDoors.Play("Pump Bay Door Close");
        }
        _isOpen = !_isOpen;

        return true;
    }

}
