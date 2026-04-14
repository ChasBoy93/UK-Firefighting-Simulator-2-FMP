using UnityEngine;

public class RadioTrigger : MonoBehaviour
{
    public RadioSystem radio;
    public RadioMessage message;

    private bool played = false;

    void OnEnable()
    {
        played = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (played) return;
        if (!other.CompareTag("Fire Appliance")) return;

        radio?.PlayRadio(message);
        played = true;
    }

    public void ResetTrigger()
    {
        played = false;
    }
}