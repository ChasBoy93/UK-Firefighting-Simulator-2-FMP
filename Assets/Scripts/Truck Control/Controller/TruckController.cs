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

    public float maxAcceleration = 6000f;
    public float brakeAcceleration = 50f;

    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

    public Vector3 _centerOfMass;

    public List<Wheel> wheels;

    float moveInput;
    float steerInput;

    private Rigidbody truckRb;

    void Start()
    {
        truckRb = GetComponent<Rigidbody>();
        truckRb.centerOfMass = _centerOfMass;
    }
    void Update()
    {
        GetInputs();
        AnimatedWheels();
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
        foreach(var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * 1500 * maxAcceleration * Time.deltaTime;
        }
    }

    void Steer()
    {
        foreach(var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }

    void Brake()
    {
        float speed = truckRb.linearVelocity.magnitude;

        // If pressing reverse while still moving forward -> brake
        if (moveInput < 0 && speed > 1f)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = brakeAcceleration * 5000f * Time.deltaTime;
            }
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
        }

        // Space = handbrake
        if (Input.GetKey(KeyCode.Space))
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = brakeAcceleration * 8000f * Time.deltaTime;
            }
        }
    }

    void AnimatedWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
        }
    }

}
