using UnityEngine;

public class InstructionTurnOn : MonoBehaviour
{
    public GameObject incidentInstructions;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire Appliance"))
        {
            incidentInstructions.SetActive(true);
        }
    }
}
