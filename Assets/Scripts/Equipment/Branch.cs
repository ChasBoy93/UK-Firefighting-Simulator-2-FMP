using UnityEngine;

public class Branch : MonoBehaviour
{
    public GameObject theBranch;
    public GameObject theBranchParticle;

    bool isBranchOn = false;

    private bool hoseOn;
    public AudioSource hoseSound;

    public PumpControl pumpScreen;


    void Update()
    {
        UseHose();
    }


    public void UseBranch()
    {
        isBranchOn = !isBranchOn;

        if(isBranchOn)
        {
            theBranch.SetActive(true);
        }
        else
        {
            theBranch.SetActive(false);
        }
    }

    void UseHose()
    {
        if (!pumpScreen.isPumpOn)
        {
            hoseOn = false;
            theBranchParticle.SetActive(false);

            if (hoseSound.isPlaying)
            {
                hoseSound.Stop();
            }

            return;
        }

        if (Input.GetMouseButton(0))
        {
            hoseOn = true;
            theBranchParticle.SetActive(true);

            if (!hoseSound.isPlaying)
            {
                hoseSound.Play();
            }
        }
        else
        {
            hoseOn = false;
            theBranchParticle.SetActive(false);

            if (hoseSound.isPlaying)
            {
                hoseSound.Stop();
            }
        }
    }
}
