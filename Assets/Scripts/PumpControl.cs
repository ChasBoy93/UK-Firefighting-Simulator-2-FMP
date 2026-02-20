using System.Collections;
using UnityEngine;
using TMPro;

public class PumpControl : MonoBehaviour
{
    [Header("Pump On/Off")]
    public GameObject pumpSelectNormal;
    public GameObject pumpSelected;
    public GameObject pumpImpellerIcon;

    public AudioSource pumpOnWarningSound;
    public AudioSource pumpOffWarningSound;

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
    public GameObject autoPressureControlText;
    public GameObject greenLpIcon;
    public GameObject greenHpIcon;

    [Header("High Pressure Gauge")]
    public TextMeshProUGUI highPressureText;
    private int highPressureTarget = 0;
    private Coroutine highPressureCoroutine;

    [Header("HP Gauge Visuals")]
    public GameObject hpGaugeBlue;
    public GameObject hpGaugeGreen;

    private int currentHighPressure = 0;
    private bool hpLocked = false;


    public void TogglePump()
    {
        isPumpOn = !isPumpOn;

        if (isPumpOn)
        {
            pumpSelectNormal.SetActive(false);
            pumpSelected.SetActive(true);
            pumpImpellerIcon.SetActive(true);
            manualPressureControlText.SetActive(false);
            autoPressureControlText.SetActive(true);

            pumpOnWarningSound.Play();

            highPressureTarget = 3;

            if (highPressureCoroutine != null)
            {
                StopCoroutine(highPressureCoroutine);
            }
            highPressureCoroutine = StartCoroutine(HighPressureFlicker());

        }
        else
        {
            pumpSelectNormal.SetActive(true);
            pumpSelected.SetActive(false);
            pumpImpellerIcon.SetActive(false);
            greenLpIcon.SetActive(false);
            greenHpIcon.SetActive(false);
            hpGaugeBlue.SetActive(true);
            hpGaugeGreen.SetActive(false);

            //Other Pressures Deactivate

            // 4 Bar
            fourBarLPNormal.SetActive(true);
            fourBarLPSelected.SetActive(false);

            // 7 bar
            sevenBarLPNormal.SetActive(true);
            sevenBarLPSelected.SetActive(false);

            // 28 bar
            twentyEightBarLPNormal.SetActive(true);
            twentyEightBarLPSelected.SetActive(false);

            pumpOffWarningSound.Play();

            if (highPressureCoroutine != null)
                StopCoroutine(highPressureCoroutine);

            StopAllCoroutines();

            currentHighPressure = 3;

            hpLocked = false;
            hpGaugeBlue.SetActive(true);
            hpGaugeGreen.SetActive(false);

            highPressureText.text = "0";

        }
    }

    public void ToggleFourBarPressure()
    {
        isFourBarOn = !isFourBarOn;

        if (isFourBarOn)
        {
            fourBarLPNormal.SetActive(false);
            fourBarLPSelected.SetActive(true);
            manualPressureControlText.SetActive(false);
            autoPressureControlText.SetActive(true);
            greenLpIcon.SetActive(true);
            greenHpIcon.SetActive(false);

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
        isSevenBarOn = !isSevenBarOn;

        if (isSevenBarOn)
        {
            sevenBarLPNormal.SetActive(false);
            sevenBarLPSelected.SetActive(true);
            manualPressureControlText.SetActive(false);
            autoPressureControlText.SetActive(true);
            greenLpIcon.SetActive(true);
            greenHpIcon.SetActive(false);

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
        if (!isPumpOn) return;

        isTwentyEightBarOn = !isTwentyEightBarOn;

        if (isTwentyEightBarOn)
        {
            twentyEightBarLPNormal.SetActive(false);
            twentyEightBarLPSelected.SetActive(true);
            greenLpIcon.SetActive(false);
            greenHpIcon.SetActive(true);
            StartCoroutine(RampPressure(28));
        }
        else
        {
            twentyEightBarLPNormal.SetActive(true);
            twentyEightBarLPSelected.SetActive(false);
            StartCoroutine(RampPressure(3));
        }
    }

    public void RpmUpButtonDown()
    {
        RpmUpNormal.SetActive(false);
        RpmUpSelected.SetActive(true);
        manualPressureControlText.SetActive(true);
        autoPressureControlText.SetActive(false);
        greenLpIcon.SetActive(false);
        greenHpIcon.SetActive(false);

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
        autoPressureControlText.SetActive(false);
        greenLpIcon.SetActive(false);
        greenHpIcon.SetActive(false);

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
        autoPressureControlText.SetActive(false);
        greenLpIcon.SetActive(false);
        greenHpIcon.SetActive(false);

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

    IEnumerator RampPressure(int target)
    {
        hpLocked = false;

        while (currentHighPressure != target)
        {
            if (currentHighPressure < target)
                currentHighPressure++;
            else
                currentHighPressure--;

            yield return new WaitForSeconds(0.08f);
        }
    }

    IEnumerator HighPressureFlicker()
    {
        yield return null;

        while (true)
        {
            int minValue = (currentHighPressure <= 3) ? 2 : currentHighPressure - 1;
            int maxValue = (currentHighPressure <= 3) ? 4 : currentHighPressure + 1;

            int flickerValue = Random.Range(minValue, maxValue + 1);

            highPressureText.text = flickerValue.ToString();

            if (!hpLocked && currentHighPressure == 28)
            {
                hpLocked = true;
            }

            if (hpLocked)
            {
                if (flickerValue < 27 || flickerValue > 29)
                {
                    hpLocked = false;
                }
            }

            if (hpLocked)
            {
                hpGaugeBlue.SetActive(false);
                hpGaugeGreen.SetActive(true);
            }
            else
            {
                hpGaugeBlue.SetActive(true);
                hpGaugeGreen.SetActive(false);
            }

            yield return new WaitForSeconds(Random.Range(0.05f, 0.35f));
        }
    }


}
