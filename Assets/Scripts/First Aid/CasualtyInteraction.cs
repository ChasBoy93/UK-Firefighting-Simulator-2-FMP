using UnityEngine;

public class CasualtyInteraction : MonoBehaviour, IInteractable
{
    public CasualtyMedicalData medicalData;

    public FirstAidMenu firstAidMenu;

    public void Interact()
    {
        firstAidMenu.OpenMenu(medicalData);
    }

    public string GetInteractionText()
    {
        return "First Aid";
    }
}