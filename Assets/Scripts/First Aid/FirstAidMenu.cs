using UnityEngine;
using System.Collections;
using TMPro;

public class FirstAidMenu : MonoBehaviour
{
    public GameObject menuPanel;

    public GameObject inspectPopup;
    public TMP_Text inspectText;

    private CasualtyMedicalData currentCasualty;

    public void OpenMenu(CasualtyMedicalData casualty)
    {
        currentCasualty = casualty;

        menuPanel.SetActive(true);
    }

    public void CloseMenu()
    {
        menuPanel.SetActive(false);
    }

    public float popupDuration = 6f;

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

        StopAllCoroutines();
        StartCoroutine(HidePopupAfterTime());
    }

    IEnumerator HidePopupAfterTime()
    {
        yield return new WaitForSeconds(popupDuration);

        inspectPopup.SetActive(false);
    }

    public void CloseInspect()
    {
        inspectPopup.SetActive(false);
    }
}