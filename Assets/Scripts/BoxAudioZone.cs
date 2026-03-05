using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider))]
public class BoxAudioZone : MonoBehaviour
{
    public float fadeSpeed = 3f;

    private AudioSource audioSource;
    private BoxCollider box;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        box = GetComponent<BoxCollider>();
        box.isTrigger = true;
        audioSource.spatialBlend = 0;
        audioSource.volume = 0f;
    }

    void Update()
    {
        AudioListener listenerComponent = Object.FindFirstObjectByType<AudioListener>();
        if (listenerComponent == null) return;

        Vector3 listenerPos = listenerComponent.transform.position;

        if (box.bounds.Contains(listenerPos))
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, 1f, fadeSpeed * Time.deltaTime);

            Vector3 localPos = box.transform.InverseTransformPoint(listenerPos);
            float pan = Mathf.Clamp(localPos.x / (box.size.x / 2f), -1f, 1f);
            audioSource.panStereo = pan;
        }
        else
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, 0f, fadeSpeed * Time.deltaTime);
        }
    }
}