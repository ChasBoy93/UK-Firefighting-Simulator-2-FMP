using System.Collections;
using UnityEngine;
using TMPro;

public class PumpControl : MonoBehaviour
{
    [Header("Pump")]
    public GameObject pumpSelectNormal;
    public GameObject pumpSelected;
    public GameObject pumpImpellerIcon;

    public AudioSource pumpOnWarningSound;
    public AudioSource pumpOffWarningSound;

    public bool isPumpOn = false;

    [Header("Low Pressure Buttons")]
    public GameObject fourBarLPNormal;
    public GameObject fourBarLPSelected;
    public GameObject sevenBarLPNormal;
    public GameObject sevenBarLPSelected;

    [Header("High Pressure Button")]
    public GameObject twentyEightBarNormal;
    public GameObject twentyEightBarSelected;

    [Header("Gauge Text")]
    public TextMeshProUGUI highPressureText;
    public TextMeshProUGUI lowPressureText;

    [Header("Gauge Colours")]
    public GameObject hpGaugeBlue;
    public GameObject hpGaugeGreen;
    public GameObject lpGaugeBlue;
    public GameObject lpGaugeGreen;

    [Header("Mode Icons")]
    public GameObject hpGreenIcon;
    public GameObject lpGreenIcon;

    private int hpTarget = 0;
    private int currentHighPressure = 0;

    private float lpTarget = 0f;
    private float currentLowPressure = 0f;

    private bool isHpMode = false;
    private bool isLpMode = false;

    private Coroutine hpRampRoutine;
    private Coroutine lpRampRoutine;

    private Coroutine hpFlickerRoutine;
    private Coroutine lpFlickerRoutine;

    private const int idleHP = 3;
    private const float idleLP = 2.5f;

    public GameObject autoPressureModeText;
    public GameObject manualPressureModeText;


    [Header("PTO Audio")]
    public AudioSource startPTOAudio;
    public AudioSource loopingPTOAudio;
    public AudioSource stopPTOAudio;

    [Header("Other")]
    public GameObject fireHoseParticle;

    public void TogglePump()
    {
        isPumpOn = !isPumpOn;

        if (isPumpOn)
        {
            // Update pump UI
            pumpSelectNormal.SetActive(false);
            pumpSelected.SetActive(true);
            pumpImpellerIcon.SetActive(true);

            // Start the audio sequence
            StartCoroutine(PlayPumpStartAudioSequence());

            // Initialize pressures
            currentHighPressure = idleHP;
            currentLowPressure = idleLP;

            autoPressureModeText.SetActive(true);
            manualPressureModeText.SetActive(false);

            // Start flicker routines
            hpFlickerRoutine = StartCoroutine(HighPressureFlicker());
            lpFlickerRoutine = StartCoroutine(LowPressureFlicker());

            fireHoseParticle.SetActive(true);
        }
        else
        {
            fireHoseParticle.SetActive(false);

            // Reset pump UI
            pumpSelectNormal.SetActive(true);
            pumpSelected.SetActive(false);
            pumpImpellerIcon.SetActive(false);

            // Stop all coroutines (flickers & ramps)
            StopAllCoroutines();

            // Stop the looping PTO audio if it's playing
            if (loopingPTOAudio.isPlaying)
                loopingPTOAudio.Stop();

            // Play stop audio and warning
            stopPTOAudio.Play();
            pumpOffWarningSound.Play();

            // Reset pressures
            currentHighPressure = 0;
            currentLowPressure = 0f;

            highPressureText.text = "0";
            lowPressureText.text = "0.0";

            ResetGauges();
            DeselectAllPressures();

        }
    }

    // Coroutine to play start audio sequence
    private IEnumerator PlayPumpStartAudioSequence()
    {
        // Play start PTO audio
        startPTOAudio.Play();

        // Play warning sound simultaneously (optional, can also yield if you want it after start)
        pumpOnWarningSound.Play();

        // Wait until start audio finishes
        yield return new WaitForSeconds(startPTOAudio.clip.length);

        // Start looping PTO audio
        loopingPTOAudio.Play();
    }


    public void ToggleFourBarPressure()
    {
        if (!isPumpOn) return;

        SelectLowPressure(4.0f, 6);
    }

    public void ToggleSevenBarPressure()
    {
        if (!isPumpOn) return;

        SelectLowPressure(7.0f, 10);
    }

    void SelectLowPressure(float newLP, int newHP)
    {
        if (lpTarget == newLP && isLpMode)
        {
            DeselectAllPressures();
            return;
        }

        isLpMode = true;
        isHpMode = false;

        lpTarget = newLP;
        hpTarget = newHP;

        SetAutoModeUI();
        UpdateButtons();
        UpdateModeIcons();

        StartLowPressureRamp(lpTarget);
        StartHighPressureRamp(hpTarget);
    }


    public void ToggleTwentyEightBarPressure()
    {
        if (!isPumpOn) return;

        if (hpTarget == 28 && isHpMode)
        {
            DeselectAllPressures();
            return;
        }

        isHpMode = true;
        isLpMode = false;

        hpTarget = 28;
        lpTarget = 24.0f;

        SetAutoModeUI();
        UpdateButtons();
        UpdateModeIcons();

        StartHighPressureRamp(hpTarget);
        StartLowPressureRamp(lpTarget);
    }


    void StartHighPressureRamp(int target)
    {
        if (hpRampRoutine != null)
        {
            StopCoroutine(hpRampRoutine);
        }

        hpRampRoutine = StartCoroutine(RampHighPressure(target));
    }

    void StartLowPressureRamp(float target)
    {
        if (lpRampRoutine != null)
        {
            StopCoroutine(lpRampRoutine);
        }

        lpRampRoutine = StartCoroutine(RampLowPressure(target));
    }

    IEnumerator RampHighPressure(int target)
    {
        while (currentHighPressure != target)
        {
            if (currentHighPressure < target)
            {
                currentHighPressure++;
            }
            else
            {
                currentHighPressure--;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator RampLowPressure(float target)
    {
        while (Mathf.Abs(currentLowPressure - target) > 0.05f)
        {
            if (currentLowPressure < target)
                currentLowPressure += 0.2f;
            else
                currentLowPressure -= 0.2f;

            yield return new WaitForSeconds(0.03f);
        }

        currentLowPressure = target;
    }


    IEnumerator HighPressureFlicker()
    {
        while (true)
        {
            int flicker = Random.Range(currentHighPressure - 1, currentHighPressure + 2);
            highPressureText.text = flicker.ToString();

            bool locked = isHpMode && Mathf.Abs(flicker - hpTarget) <= 1;

            hpGaugeBlue.SetActive(!locked);
            hpGaugeGreen.SetActive(locked);

            yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
        }
    }

    IEnumerator LowPressureFlicker()
    {
        while (true)
        {
            float flicker = Random.Range(currentLowPressure - 1f, currentLowPressure + 1f);
            lowPressureText.text = flicker.ToString("F1");

            bool locked = isLpMode && Mathf.Abs(flicker - lpTarget) <= 1f;

            lpGaugeBlue.SetActive(!locked);
            lpGaugeGreen.SetActive(locked);

            yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
        }
    }


    void UpdateButtons()
    {
        fourBarLPNormal.SetActive(lpTarget != 4f);
        fourBarLPSelected.SetActive(lpTarget == 4f);

        sevenBarLPNormal.SetActive(lpTarget != 7f);
        sevenBarLPSelected.SetActive(lpTarget == 7f);

        twentyEightBarNormal.SetActive(hpTarget != 28);
        twentyEightBarSelected.SetActive(hpTarget == 28);
    }

    void UpdateModeIcons()
    {
        hpGreenIcon.SetActive(isHpMode);
        lpGreenIcon.SetActive(isLpMode);
    }

    void ResetGauges()
    {
        hpGaugeBlue.SetActive(true);
        hpGaugeGreen.SetActive(false);
        lpGaugeBlue.SetActive(true);
        lpGaugeGreen.SetActive(false);

        hpGreenIcon.SetActive(false);
        lpGreenIcon.SetActive(false);
    }

    public void DeselectAllPressures()
    {
        isHpMode = false;
        isLpMode = false;

        hpTarget = idleHP;
        lpTarget = idleLP;

        UpdateButtons();
        UpdateModeIcons();

        StartHighPressureRamp(idleHP);
        StartLowPressureRamp(idleLP);

        SetManualModeUI();
    }

    void SetAutoModeUI()
    {
        manualPressureModeText.SetActive(false);
        autoPressureModeText.SetActive(true);
    }

    void SetManualModeUI()
    {
        manualPressureModeText.SetActive(true);
        autoPressureModeText.SetActive(false);
    }
}