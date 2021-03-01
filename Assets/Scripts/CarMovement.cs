using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentBreakForce;
    private bool isBreaking;

    [SerializeField] private float speedForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteeringAngle;

    [SerializeField] private WheelCollider frontWheel1Collider;
    [SerializeField] private WheelCollider frontWheel2Collider;
    [SerializeField] private WheelCollider backWheel1Collider;
    [SerializeField] private WheelCollider backWheel2Collider;

    [SerializeField] private Transform frontWheel1Transform;
    [SerializeField] private Transform frontWheel2Transform;
    [SerializeField] private Transform backWheel1Transform;
    [SerializeField] private Transform backWheel2Transform;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }
    private void HandleMotor()
    {
        frontWheel1Collider.motorTorque = verticalInput * speedForce;
        frontWheel2Collider.motorTorque = verticalInput * speedForce;
        currentBreakForce = isBreaking ? breakForce : 0f;
        if (isBreaking)
        {
            ApplyBreaking();
        }
    }

    private void ApplyBreaking()
    {
        frontWheel1Collider.brakeTorque = currentBreakForce;
        frontWheel2Collider.brakeTorque = currentBreakForce;
        backWheel1Collider.brakeTorque = currentBreakForce;
        backWheel2Collider.brakeTorque = currentBreakForce;
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }
    private void HandleSteering()
    {
        currentSteerAngle = maxSteeringAngle * horizontalInput;
        frontWheel1Collider.steerAngle = currentSteerAngle;
        frontWheel2Collider.steerAngle = currentSteerAngle;
    }
    private void UpdateWheels()
    {
        UpdateSingleWheel(frontWheel1Collider, frontWheel1Transform);
        UpdateSingleWheel(frontWheel2Collider, frontWheel2Transform);
        UpdateSingleWheel(backWheel1Collider, backWheel1Transform);
        UpdateSingleWheel(backWheel2Collider, backWheel2Transform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;

    }
}
