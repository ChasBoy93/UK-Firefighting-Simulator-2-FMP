using UnityEngine;

public class TruckSounds : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    private float currentSpeed;

    private Rigidbody truckRb;
    private AudioSource truckAudio;

    public float minPitch;
    public float maxPitch;
    private float pitchFromTruck;

    void Start()
    {
        truckAudio = GetComponent<AudioSource>();
        truckRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        EngineSound();
    }

    void EngineSound()
    {
        currentSpeed = truckRb.linearVelocity.magnitude;
        pitchFromTruck = truckRb.linearVelocity.magnitude / 50f;

        if(currentSpeed < minSpeed)
        {
            truckAudio.pitch = minPitch;
        }
        else if (currentSpeed > maxSpeed)
        {
            truckAudio.pitch = maxPitch;
        }
        else
        {
            truckAudio.pitch = Mathf.Lerp(minPitch, maxPitch, pitchFromTruck);
        }
    }
}
