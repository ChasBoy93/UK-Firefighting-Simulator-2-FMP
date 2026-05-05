using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CutDoor : MonoBehaviour, IInteractable
{
    public GameObject doorToCut;
    public GameObject sliderIndicator;
    public GameObject personInVehicle;
    public GameObject personOnFloor;

    public void Interact()
    {
        StartCoroutine(CutVehicleDoor());
    }

    IEnumerator CutVehicleDoor()
    {
        sliderIndicator.SetActive(true);
        yield return new WaitForSeconds(2f);
        sliderIndicator.SetActive(false);
        doorToCut.SetActive(false);
        personInVehicle.SetActive(false);
        personOnFloor.SetActive(true);
    }

    public string GetInteractionText()
    {
        return "Cut Vehicle Door";
    }

}
