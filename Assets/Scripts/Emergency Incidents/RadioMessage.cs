using UnityEngine;

[System.Serializable]
public class RadioMessage
{
    [Header("Control Message")]
    public AudioClip controlClip;
    public string controlSubtitle;

    [Header("Crew Response (Optional)")]
    public AudioClip responseClip;
    public string responseSubtitle;

    public float responseDelay = 2f;
}