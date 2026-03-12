using UnityEngine;

public class TruckSounds : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;

    public float minPitch;
    public float maxPitch;

    private float currentSpeed;
    private float pitchFromTruck;

    private Rigidbody truckRb;
    private AudioSource truckAudio;
    private TruckController controller;

    void Start()
    {
        truckAudio = GetComponent<AudioSource>();
        truckRb = GetComponent<Rigidbody>();
        controller = GetComponent<TruckController>();
    }

    void Update()
    {
        EngineSound();
    }

    void EngineSound()
    {
        if (!controller.engineRunning)
        {
            return;
        }

        currentSpeed = truckRb.linearVelocity.magnitude;
        pitchFromTruck = truckRb.linearVelocity.magnitude / 50f;

        if (currentSpeed < minSpeed)
        {
            truckAudio.pitch = minPitch;
        }
        else if (currentSpeed > maxSpeed)
        {
            truckAudio.pitch = maxPitch;
        }
        else
        {
            truckAudio.pitch =
                Mathf.Lerp(minPitch, maxPitch, pitchFromTruck);
        }
    }
}