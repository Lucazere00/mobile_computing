using UnityEngine;
using System;
using System.Collections.Generic;

public class CarController : MonoBehaviour
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
        public GameObject wheelEffectObj;
        
        public Axel axel;
    }

    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;
    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;


    public List<Wheel> wheels;
    float moveInput;
    float steerInput;
    private Rigidbody carRb;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();


    }


    void Update()
    {
        GetInputs();
        AnimateWheels();
        WheelEffects();
    }

    void FixedUpdate()
    {
        Move();
        Steer();
        Brake();
    }


    public void MoveInput(float input)
    {
        moveInput = input;
    }

    public void SteerInput(float input)
    {
        steerInput = input;
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
            wheel.wheelCollider.motorTorque = moveInput * maxAcceleration;
        }
    }

    void Steer()
    {
        foreach (var wheel in wheels)
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
        if (Input.GetKey(KeyCode.Space) || moveInput == 0)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime;
            }


        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }

        }

    }
    void AnimateWheels()
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



    void WheelEffects()
{
    foreach (var wheel in wheels)
    {
        var trail = wheel.wheelEffectObj?.GetComponentInChildren<TrailRenderer>();
        if (trail == null) continue;

        bool braking = Input.GetKey(KeyCode.Space);
        bool grounded = wheel.wheelCollider.isGrounded;
        bool fastEnough = carRb.linearVelocity.magnitude >= 5f;

        if (braking && wheel.axel == Axel.Rear && grounded && fastEnough)
        {
            trail.emitting = true;
            Debug.Log("Scia ON su: " + wheel.wheelModel.name);
        }
        else
        {
            trail.emitting = false;
        }
    }
}
}