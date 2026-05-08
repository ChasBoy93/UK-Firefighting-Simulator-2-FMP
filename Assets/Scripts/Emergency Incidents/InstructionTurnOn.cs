using UnityEngine;

public class InstructionTurnOn : MonoBehaviour
{
    public GameObject incidentInstructions;
    public GameObject waypointIndicator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire Appliance"))
        {
            incidentInstructions.SetActive(true);
            waypointIndicator.SetActive(false);
        }
    }
}
