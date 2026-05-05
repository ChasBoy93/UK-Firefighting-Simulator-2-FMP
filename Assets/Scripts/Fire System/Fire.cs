using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float currentIntensity = 1.0f;
    private float[] startIntensities = new float[0];

    private float timeLastWatered = 0f;
    [SerializeField] private float regenDelay = 2.5f;
    [SerializeField] private float regenRate = 0.1f;

    [SerializeField] private ParticleSystem[] firePartcleSystems = new ParticleSystem[0];

    private bool isLit = true;

    private void Start()
    {
        startIntensities = new float[firePartcleSystems.Length];

        for (int i = 0; i < firePartcleSystems.Length; i++)
        {
            if (firePartcleSystems[i] != null)
            {
                startIntensities[i] = firePartcleSystems[i].emission.rateOverTime.constant;
            }
        }
    }

    private void Update()
    {
        if (isLit && currentIntensity < 1.0f && Time.time - timeLastWatered >= regenDelay)
        {
            currentIntensity += regenRate * Time.deltaTime;
            currentIntensity = Mathf.Clamp01(currentIntensity);
            ChangeIntensity();
        }
    }

    public bool TryExtinguish(float amount)
    {
        timeLastWatered = Time.time;

        currentIntensity -= amount;

        if (currentIntensity <= 0f)
        {
            currentIntensity = 0f;
            isLit = false;

            for (int i = 0; i < firePartcleSystems.Length; i++)
            {
                var ps = firePartcleSystems[i];
                if (ps == null) continue;

                ps.Stop();
                ps.Clear();
            }

            return true;
        }

        ChangeIntensity();
        return false;
    }

    private void ChangeIntensity()
    {
        for (int i = 0; i < firePartcleSystems.Length; i++)
        {
            var ps = firePartcleSystems[i];
            if (ps == null) continue;

            var emission = ps.emission;
            emission.rateOverTime = currentIntensity * startIntensities[i];
        }
    }

    public void ResetFire()
    {
        currentIntensity = 1f;
        isLit = true;
        timeLastWatered = 0f;

        for (int i = 0; i < firePartcleSystems.Length; i++)
        {
            var ps = firePartcleSystems[i];
            if (ps == null) continue;

            ps.Clear();
            ps.Play();
        }

        ChangeIntensity();
    }
}