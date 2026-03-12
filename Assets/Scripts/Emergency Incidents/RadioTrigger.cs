using UnityEngine;

public class RadioTrigger : MonoBehaviour
{
    public RadioSystem radio;

    public AudioClip clip;

    public string subtitle;

    bool played = false;

    void OnTriggerEnter(Collider other)
    {
        if (played) return;

        if (!other.CompareTag("FireEngine")) return;

        if (radio != null)
        {
            radio.PlayRadio(clip, subtitle);
        }

        played = true;
    }
}