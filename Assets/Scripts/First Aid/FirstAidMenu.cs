using Artemis;
using System.Collections;
using TMPro;
using UnityEngine;

public class FirstAidMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject menuPanel;

    public GameObject inspectPopup;

    public TMP_Text inspectText;

    public TMP_Text treatmentStatusText;

    [Header("Player")]
    public GameObject playerCameraStop;

    [Header("Settings")]
    public float treatmentTime = 3f;

    private CasualtyMedicalData currentCasualty;

    // MENU

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

    // INSPECT

    public void InspectCasualty()
    {
        if (currentCasualty == null)
            return;

        inspectPopup.SetActive(true);

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

        StartCoroutine(HideInspectPopup());
    }

    IEnumerator HideInspectPopup()
    {
        yield return new WaitForSeconds(6f);

        inspectPopup.SetActive(false);
    }

    // CPR

    public void CPRButton()
    {
        StartCoroutine(PerformTreatment("Performing CPR...", currentCasualty.ApplyCPR(),"Breathing restored", "CPR not required"));
    }

    // BLEEDING

    public void BleedingButton()
    {
        StartCoroutine(PerformTreatment("Controlling bleeding...", currentCasualty.ControlBleeding(),"Bleeding controlled", "No severe bleeding found"));
    }

    // OXYGEN

    public void OxygenButton()
    {
        StartCoroutine(PerformTreatment("Administering oxygen...", currentCasualty.GiveOxygen(), "Breathing improved", "Oxygen not required"));
    }

    // REASSURE

    public void ReassureButton()
    {
        StartCoroutine(PerformTreatment(
            "Reassuring casualty...", currentCasualty.ReassureCasualty(), "Casualty calmed", "Casualty already calm" ));
    }

    // IMMOBILISE

    public void ImmobiliseButton()
    {
        StartCoroutine(PerformTreatment("Immobilising casualty...", currentCasualty.Immobilise(), "Casualty immobilised", "Immobilisation not required" ));
    }

    // UNIVERSAL TREATMENT

    IEnumerator PerformTreatment(
        string startMessage,
        bool success,
        string successMessage,
        string failMessage)
    {
        treatmentStatusText.gameObject.SetActive(true);

        treatmentStatusText.text = startMessage;

        yield return new WaitForSeconds(treatmentTime);

        if (success)
        {
            treatmentStatusText.text = successMessage;
        }
        else
        {
            treatmentStatusText.text = failMessage;
        }

        yield return new WaitForSeconds(2f);

        treatmentStatusText.gameObject.SetActive(false);
    }
}