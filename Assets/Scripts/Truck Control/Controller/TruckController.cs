using System;
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

    private Rigidbody truckRb;
    private TruckLights truckLights;

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
    }

    void Update()
    {
        GetInputs();
        AnimateWheels();
    }

    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }

    void GetInputs()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }

    void Move()
    {
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * maxAcceleration * Time.deltaTime * 1500f;
        }
    }

    void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                float targetAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, targetAngle, 0.6f);
            }
        }
    }

    void Brake()
    {

        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.brakeTorque = 0f;
        }

        bool isBraking = false;


        if (moveInput < 0 && RigidbodyVelocity > 2f)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = brakeAcceleration * 1000f * Time.deltaTime;
            }
            isBraking = true;
        }


        if (Input.GetKey(KeyCode.Space))
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = brakeAcceleration * 2500f * Time.deltaTime;
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