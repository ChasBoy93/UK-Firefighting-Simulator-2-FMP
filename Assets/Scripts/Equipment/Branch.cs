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
            hoseSound.Stop();
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (hoseOn)
            {
                hoseOn = false;
                theBranchParticle.SetActive(false);
                hoseSound.Stop();
            }
            else
            {
                hoseOn = true;
                theBranchParticle.SetActive(true);
                hoseSound.Play();
            }
        }
    }
}
