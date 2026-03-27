using UnityEngine;
using System.Collections;

public class BlueLightController : MonoBehaviour
{
    public bool blueLightsOn;
    public GameObject blueLights;
    public GameObject sirens;

    public AudioSource hornSound;
    public AudioSource sirenSound1;
    public AudioSource sirenSound2;
    public AudioSource sirenSound3;

    private AudioSource[] sirenArray;
    private int sirenIndex = 0;
    private AudioSource currentSiren;

    public float doubleTapTime = 0.4f;
    private float lastHPressTime = 0f;

    public float sirenBlipDuration = 0.5f;
    private Coroutine blipCoroutine;

    public float startupBlipChance = 0.5f;
    public float startupBlipDuration = 0.3f;
    private bool hasDoneStartupBlip = false;

    private bool sirenIsActive = false;

    private Coroutine tapCoroutine;

    void Start()
    {
        sirenArray = new AudioSource[] { sirenSound1, sirenSound2, sirenSound3 };
    }

    void Update()
    {
        AllBlues();
        SirenControl();
    }

    void AllBlues()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (blueLightsOn)
            {
                blueLights.SetActive(false);
                sirens.SetActive(false);
                StopSiren();
                blueLightsOn = false;
            }
            else
            {
                blueLights.SetActive(true);
                sirens.SetActive(true);
                blueLightsOn = true;
                sirenIndex = 0;
            }
        }
    }

    void SirenControl()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            hornSound.Play();

            if (!blueLightsOn)
            {
                return;
            }

                float timeSinceLastPress = Time.time - lastHPressTime;

            if (timeSinceLastPress <= doubleTapTime && sirenIsActive)
            {
                lastHPressTime = 0f;

                if (tapCoroutine != null)
                {
                    StopCoroutine(tapCoroutine);
                }
                DoubleTapStop();
            }
            else
            {
                lastHPressTime = Time.time;

                if (tapCoroutine != null)
                {
                    StopCoroutine(tapCoroutine);
                }

                tapCoroutine = StartCoroutine(SingleTapDelayed());
            }
        }
    }

    IEnumerator SingleTapDelayed()
    {
        yield return new WaitForSeconds(doubleTapTime);


        if (!sirenIsActive && !hasDoneStartupBlip)
        {
            if (Random.value <= startupBlipChance)
            {
                hasDoneStartupBlip = true;
                yield return StartCoroutine(StartupBlip());
                yield break;
            }
        }

        ToggleSiren();
    }

    IEnumerator StartupBlip()
    {
        AudioSource wail = sirenArray[0];

        if (wail != null)
        {
            wail.Play();
            yield return new WaitForSeconds(startupBlipDuration);
            wail.Stop();
        }

        sirenIsActive = false;
        currentSiren = null;
    }

    void ToggleSiren()
    {
        StopSiren();

        currentSiren = sirenArray[sirenIndex];

        if (currentSiren != null)
        {
            currentSiren.Play();
        }
           
        sirenIsActive = true;

        sirenIndex = (sirenIndex + 1) % sirenArray.Length;
    }

    void DoubleTapStop()
    {
        StopSiren();

        AudioSource nextSiren = sirenArray[sirenIndex];

        if (blipCoroutine != null)
        {
            StopCoroutine(blipCoroutine);
        }
            

        blipCoroutine = StartCoroutine(SirenBlip(nextSiren));

        sirenIndex = 0;
        sirenIsActive = false;
        hasDoneStartupBlip = false;
    }

    IEnumerator SirenBlip(AudioSource siren)
    {
        if (siren != null)
        {
            siren.Play();
            yield return new WaitForSeconds(sirenBlipDuration);
            siren.Stop();
        }

        currentSiren = null;
    }

    void StopSiren()
    {
        if (currentSiren != null)
        {
            currentSiren.Stop();
        }
            

        currentSiren = null;
        sirenIsActive = false;
        hasDoneStartupBlip = false;
    }
}