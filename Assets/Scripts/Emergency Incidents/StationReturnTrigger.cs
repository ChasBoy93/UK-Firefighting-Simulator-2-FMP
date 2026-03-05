using UnityEngine;

public class StationReturnTrigger : MonoBehaviour
{
    public CallManager callManager;

    public RadioSystem radio;

    public AudioClip clip;

    public string subtitle;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("FireEngine")) return;

        if (radio != null)
            radio.PlayRadio(clip, subtitle);

        callManager.ReturnToStation();
    }
}