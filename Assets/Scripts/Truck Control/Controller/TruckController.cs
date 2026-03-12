using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public Axel axel;
    }

    [Header("Truck Settings")]
    public float maxAcceleration = 1000f;
    public float brakeAcceleration = 50f;
    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;
    public Vector3 _centerOfMass;
    public List<Wheel> wheels;

    [HideInInspector] public float moveInput;
    [HideInInspector] public float steerInput;
    [HideInInspector] public bool canDrive = false;

    [Header("Engine")]
    public bool engineRunning = false;
    public GameObject markerLights;

    [Header("Engine Audio")]
    public AudioClip engineStartClip;
    public AudioClip engineIdleClip;
    public AudioClip engineStopClip;
    Coroutine engineStartRoutine;

    private Rigidbody truckRb;
    private TruckLights truckLights;
    private AudioSource engineAudio;

    [Obsolete]
    public float RigidbodyVelocity => truckRb.velocity.magnitude;

    void Awake()
    {
        truckRb = GetComponent<Rigidbody>();
        truckRb.centerOfMass = _centerOfMass;
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        truckLights = GetComponent<TruckLights>();
        engineAudio = GetComponent<AudioSource>();

        if (markerLights != null)
        {
            markerLights.SetActive(false);
        }
    }

    void Update()
    {
        HandleEngine();
        GetInputs();
        AnimateWheels();
    }

    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }

    void HandleEngine()
    {
        if (!canDrive)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (engineRunning == false)
            {
                StartEngine();
            }
            else
            {
                StopEngine();
            }
        }
    }

    void StartEngine()
    {
        engineRunning = true;

        if (engineAudio != null)
        {
            engineAudio.loop = false;
            engineAudio.clip = engineStartClip;
            engineAudio.Play();

            engineStartRoutine = StartCoroutine(StartIdleAfterStart());
        }

        if (markerLights != null)
        {
            markerLights.SetActive(true);
        }
    }

    void StopEngine()
    {
        engineRunning = false;

        if (engineStartRoutine != null)
        {
            StopCoroutine(engineStartRoutine);
        }

        if (engineAudio != null)
        {
            engineAudio.loop = false;
            engineAudio.clip = engineStopClip;
            engineAudio.Play();
        }

        if (markerLights != null)
        {
            markerLights.SetActive(false);
        }
    }

    IEnumerator StartIdleAfterStart()
    {
        yield return new WaitForSeconds(engineStartClip.length);

        if (!engineRunning)
            yield break;

        engineAudio.clip = engineIdleClip;
        engineAudio.loop = true;
        engineAudio.Play();
    }

    void GetInputs()
    {
        if (!canDrive || !engineRunning)
        {
            moveInput = 0f;
            steerInput = 0f;
            return;
        }

        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }

    void Move()
    {
        if (!engineRunning)
        {
            return;
        }

        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * maxAcceleration * Time.deltaTime * 1500f;
        }
    }

    void Steer()
    {
        if (!engineRunning)
        {
            return;
        }

        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                float targetAngle = steerInput * turnSensitivity * maxSteerAngle;

                wheel.wheelCollider.steerAngle =
                    Mathf.Lerp(wheel.wheelCollider.steerAngle, targetAngle, 0.6f);
            }
        }
    }

    void Brake()
    {
        if (!canDrive || !engineRunning)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 5000f;
            }

            if (truckLights != null)
            {
                truckLights.SetBraking(false);
            }

            return;
        }

        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.brakeTorque = 0f;
        }

        bool isBraking = false;

        if (moveInput < 0 && RigidbodyVelocity > 2f)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque =
                    brakeAcceleration * 1000f * Time.deltaTime;
            }

            isBraking = true;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque =
                    brakeAcceleration * 2500f * Time.deltaTime;
            }

            isBraking = true;
        }

        if (truckLights != null)
        {
            truckLights.SetBraking(isBraking);
        }
    }

    void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);

            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
        }
    }
}