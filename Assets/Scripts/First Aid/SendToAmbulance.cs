using UnityEngine;

public class SendToAmbulance : MonoBehaviour
{
    public GameObject casualtyToAmbulance;

    public void SendCasualtyToAmbulance()
    {
        casualtyToAmbulance.SetActive(false);
    }
}
