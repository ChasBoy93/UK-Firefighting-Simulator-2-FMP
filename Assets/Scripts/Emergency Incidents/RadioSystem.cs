using UnityEngine;
using TMPro;

public class RadioSystem : MonoBehaviour
{
    public AudioSource radioSource;

    public TMP_Text subtitleText;

    public void PlayRadio(AudioClip clip, string subtitle)
    {
        radioSource.clip = clip;
        radioSource.Play();

        subtitleText.text = subtitle;

        CancelInvoke();
        Invoke("ClearSubtitle", clip.length);
    }

    void ClearSubtitle()
    {
        subtitleText.text = "";
    }
}