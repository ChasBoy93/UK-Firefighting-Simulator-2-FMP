using UnityEngine;

public class BAToggle : MonoBehaviour
{
    public GameObject baMask;
    public AudioSource maskAudio;
    bool maskIsOn;

    public void BAMask()
    {
            if (maskIsOn)
            {
                baMask.SetActive(false);
                maskAudio.Stop();
                maskIsOn = false;
            }
            else
            {
                baMask.SetActive(true);
                maskAudio.Play();
                maskIsOn = true;
            }
    }
}
