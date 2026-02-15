using System.Collections;
using UnityEngine;

public class PumpControl : MonoBehaviour
{
    [Header("Pump On/Off")]
    public GameObject pumpSelectNormal;
    public GameObject pumpSelected;
    public GameObject pumpImpellerIcon;

    //public AudioSource audioSource;
    //public AudioClip pumpOnWarningSound;
    //public AudioClip pumpOffWarningSound;

    private bool isPumpOn = false;

    [Header("4 Bar L/P")]
    public GameObject fourBarLPNormal;
    public GameObject fourBarLPSelected;

    private bool isFourBarOn = false;

    [Header("7 bar L/P")]
    public GameObject sevenBarLPNormal;
    public GameObject sevenBarLPSelected;

    private bool isSevenBarOn = false;

    [Header("28 bar H/P")]
    public GameObject twentyEightBarLPNormal;
    public GameObject twentyEightBarLPSelected;

    private bool isTwentyEightBarOn = false;

    [Header("RPM UP")]
    public GameObject RpmUpNormal;
    public GameObject RpmUpSelected;

    [Header("RPM DOWN")]
    public GameObject RpmDownNormal;
    public GameObject RpmDownSelected;

    [Header("RPM IDLE")]
    public GameObject RpmIdleNormal;
    public GameObject RpmIdleSelected;

    [Header("HTT")]
    public GameObject httNormal;
    public GameObject httSelected;

    private bool isHttOn = false;

    [Header("Other Graphical Assets")]
    public GameObject manualPressureControlText;
    public GameObject greenLpIcon;


    public void TogglePump()
    {
        isPumpOn = !isPumpOn;

        if (isPumpOn)
        {
            pumpSelectNormal.SetActive(false);
            pumpSelected.SetActive(true);
            pumpImpellerIcon.SetActive(true);
            manualPressureControlText.SetActive(false);

            //audioSource.PlayOneShot(pumpOnWarningSound);
        }
        else
        {
            pumpSelectNormal.SetActive(true);
            pumpSelected.SetActive(false);
            pumpImpellerIcon.SetActive(false);
            greenLpIcon.SetActive(false);

           //audioSource.PlayOneShot(pumpOffWarningSound);
        }
    }

    public void ToggleFourBarPressure()
    {
        isFourBarOn= !isFourBarOn;

        if (isFourBarOn)
        {
            fourBarLPNormal.SetActive(false);
            fourBarLPSelected.SetActive(true);
            manualPressureControlText.SetActive(false);
            greenLpIcon.SetActive(true);

            //Other Pressures Deactivate

            // 7 bar
            sevenBarLPNormal.SetActive(true);
            sevenBarLPSelected.SetActive(false);

            // 28 bar
            twentyEightBarLPNormal.SetActive(true);
            twentyEightBarLPSelected.SetActive(false);
        }
        else
        {
            fourBarLPNormal.SetActive(true);
            fourBarLPSelected.SetActive(false);
        }

    }

    public void ToggleSevenBarPressure()
    {
        isSevenBarOn= !isSevenBarOn;

        if (isSevenBarOn)
        {
            sevenBarLPNormal.SetActive(false);
            sevenBarLPSelected.SetActive(true);
            manualPressureControlText.SetActive(false);

            //Other Pressures Deactivate

            // 4 Bar
            fourBarLPNormal.SetActive(true);
            fourBarLPSelected.SetActive(false);

            // 28 Bar
            twentyEightBarLPNormal.SetActive(true);
            twentyEightBarLPSelected.SetActive(false);
        }
        else
        {
            sevenBarLPNormal.SetActive(true);
            sevenBarLPSelected.SetActive(false);
        }
    }

    public void ToggleTwentyEightBarPressure()
    {
        isTwentyEightBarOn = !isTwentyEightBarOn;

        if (isTwentyEightBarOn)
        {
            twentyEightBarLPNormal.SetActive(false);
            twentyEightBarLPSelected.SetActive(true);
            manualPressureControlText.SetActive(false);
            greenLpIcon.SetActive(false);

            //Other Pressures Deactivate

            // 4 bar
            fourBarLPNormal.SetActive(true);
            fourBarLPSelected.SetActive(false);

            // 7 bar
            sevenBarLPNormal.SetActive(true);
            sevenBarLPSelected.SetActive(false);
        }
        else
        {
            twentyEightBarLPNormal.SetActive(true);
            twentyEightBarLPSelected.SetActive(false);
        }
    }

    public void RpmUpButtonDown()
    {
        RpmUpNormal.SetActive(false);
        RpmUpSelected.SetActive(true);
        manualPressureControlText.SetActive(true);
        greenLpIcon.SetActive(false);

        //Other Pressures Deactivate

        // 4 bar
        fourBarLPNormal.SetActive(true);
        fourBarLPSelected.SetActive(false);

        // 7 bar
        sevenBarLPNormal.SetActive(true);
        sevenBarLPSelected.SetActive(false);

        // 28 Bar
        twentyEightBarLPNormal.SetActive(true);
        twentyEightBarLPSelected.SetActive(false);
    }

    public void RpmUpButtonUp()

    {
        RpmUpNormal.SetActive(true);
        RpmUpSelected.SetActive(false);
    }

    public void RpmDownButtonDown()
    {
        RpmDownNormal.SetActive(false);
        RpmDownSelected.SetActive(true);
        manualPressureControlText.SetActive(true);
        greenLpIcon.SetActive(false);

        //Other Pressures Deactivate

        // 4 bar
        fourBarLPNormal.SetActive(true);
        fourBarLPSelected.SetActive(false);

        // 7 bar
        sevenBarLPNormal.SetActive(true);
        sevenBarLPSelected.SetActive(false);

        // 28 Bar
        twentyEightBarLPNormal.SetActive(true);
        twentyEightBarLPSelected.SetActive(false);
    }

    public void RpmDownButtonUp()

    {
        RpmDownNormal.SetActive(true);
        RpmDownSelected.SetActive(false);
    }

    public void RpmIdle()
    {
        RpmIdleNormal.SetActive(false);
        RpmIdleSelected.SetActive(true);
        manualPressureControlText.SetActive(true);
        greenLpIcon.SetActive(false);

        //Other Pressures Deactivate

        // 4 bar
        fourBarLPNormal.SetActive(true);
        fourBarLPSelected.SetActive(false);

        // 7 bar
        sevenBarLPNormal.SetActive(true);
        sevenBarLPSelected.SetActive(false);

        // 28 Bar
        twentyEightBarLPNormal.SetActive(true);
        twentyEightBarLPSelected.SetActive(false);
        StartCoroutine(RpmIdleButton());
    }

    IEnumerator RpmIdleButton()
    {
        yield return new WaitForSeconds(1);
        RpmIdleNormal.SetActive(true);
        RpmIdleSelected.SetActive(false);
    }

    public void ToggleHydrantToTank()
    {
        isHttOn = !isHttOn;

        if (isHttOn)
        {
            httNormal.SetActive(false);
            httSelected.SetActive(true);

        }
        else
        {
            httNormal.SetActive(true);
            httSelected.SetActive(false);
        }
    }
}
