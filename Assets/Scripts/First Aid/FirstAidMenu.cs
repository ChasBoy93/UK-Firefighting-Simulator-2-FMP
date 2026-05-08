using Artemis;
using System.Collections;
using TMPro;
using UnityEngine;

public class FirstAidMenu : MonoBehaviour
{
    public GameObject menuPanel;

    public GameObject inspectPopup;
    public TMP_Text inspectText;

    private CasualtyMedicalData currentCasualty;

    public GameObject playerCameraStop;

    public void OpenMenu(CasualtyMedicalData casualty)
    {
        currentCasualty = casualty;

        menuPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerCameraStop.GetComponent<FPController>().enabled = false;
    }

    public void CloseMenu()
    {
        menuPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerCameraStop.GetComponent<FPController>().enabled = true;
    }

    public float popupDuration = 6f;

    public void InspectCasualty()
    {
        if (currentCasualty == null)
            return;

        inspectPopup.SetActive(true);
        menuPanel.SetActive(false);

        inspectText.text =
            "<b>CASUALTY INSPECTION</b>\n\n" +

            "<b>Injury:</b> " +
            currentCasualty.injury + "\n\n" +

            "<b>Consciousness:</b> " +
            currentCasualty.consciousness + "\n" +

            "<b>Breathing:</b> " +
            currentCasualty.breathing + "\n" +

            "<b>Bleeding:</b> " +
            currentCasualty.bleeding + "\n" +

            "<b>Pain:</b> " +
            currentCasualty.pain + "\n" +

            "<b>Additional State:</b> " +
            currentCasualty.extraState;

    }


    public void CloseInspect()
    {
        inspectPopup.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerCameraStop.GetComponent<FPController>().enabled = true;
    }
}