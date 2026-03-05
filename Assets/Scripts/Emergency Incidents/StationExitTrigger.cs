using UnityEngine;

public class StationExitTrigger : MonoBehaviour
{
    public RadioSystem radio;

    public AudioClip clip;

    public string subtitle;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("FireEngine")) return;

        if (radio != null)
            radio.PlayRadio(clip, subtitle);
    }
}