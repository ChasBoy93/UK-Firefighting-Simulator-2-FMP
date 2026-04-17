using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float currentIntensity = 1.0f;
    private float[] startIntensities = new float[0];
    float timeLastWatered = 0;
    [SerializeField] private float regenDelay = 2.5f;
    [SerializeField] private float regenRate = 0.1f;

    [SerializeField] private ParticleSystem []firePartcleSystems = new ParticleSystem[0];

    private bool isLit = true;

    private void Start()
    {
        startIntensities = new float[firePartcleSystems.Length];

        for(int i = 0; i < firePartcleSystems.Length; i++)
        {
            startIntensities[i] = firePartcleSystems[i].emission.rateOverTime.constant;
        }
    }



    private void Update()
    {
        if(isLit && currentIntensity < 1.0f && Time.time - timeLastWatered >= regenDelay)
        {
            currentIntensity += regenRate * Time.deltaTime;
            ChangeIntensity();
        }
    }

    public bool TryExtinguish(float amount)
    {
        timeLastWatered = Time.time;

        currentIntensity -= amount;

        if(currentIntensity < 0f)
        {
            isLit = false;
            return true;
        }

        ChangeIntensity();

        return false;
    }

    private void ChangeIntensity()
    {
        for (int i = 0; i < firePartcleSystems.Length; i++)
        {
            var emission = firePartcleSystems[i].emission;
            emission.rateOverTime = currentIntensity * startIntensities[i];
        }
    }

    public void Reignite()
    {
        currentIntensity = 1f;
        isLit = true;
        timeLastWatered = 0f;

        ChangeIntensity();

        for (int i = 0; i < firePartcleSystems.Length; i++)
        {
            var ps = firePartcleSystems[i];
            if (ps == null) continue;

            if (!ps.isPlaying)
                ps.Play();
        }
    }
}
