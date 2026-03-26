using TMPro;
using UnityEngine;

public class MDTController : MonoBehaviour
{
    public GameObject mdtScreen;

    public TMP_Text incidentText;

    public CallManager callManager;

    bool callActive = false;
    bool playerInVehicle = false;

    string incidentName;

    void Start()
    {
        mdtScreen.SetActive(false);
    }

    public void ReceiveCall(string name)
    {
        incidentName = name;

        incidentText.text = "MOBILISATION MESSAGE\nIncident: " + incidentName;

        callActive = true;

        CheckDisplay();
    }

    public void SetPlayerInVehicle(bool state)
    {
        playerInVehicle = state;

        CheckDisplay();
    }

    void CheckDisplay()
    {
        if (callActive && playerInVehicle)
        {
            mdtScreen.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
    }

    public void AcknowledgeCall()
    {
        if (callManager != null)
        {
            callManager.AcknowledgeCall();
        }

        callActive = false;

        mdtScreen.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void ResetMDT()
    {
        callActive = false;
        mdtScreen.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}