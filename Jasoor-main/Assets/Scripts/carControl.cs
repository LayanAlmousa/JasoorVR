using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Content.Interaction;

public class CarControl : MonoBehaviour
{
    public WheelCollider[] wheels = new WheelCollider[4];
    public InputActionReference rightTrigger; // Accelerate
    public InputActionReference leftTrigger;  // Brake
    public InputActionReference reverseButton; // Reverse
    public XRKnob knob; // Steering
    public float maxMotorTorque;
    public float maxBrakeTorque;
    public float steeringMax;

    private bool isReversing = false;

    void Update()
    {
        // Read inputs
        float rightTriggerPressure = rightTrigger.action.ReadValue<float>();
        float leftTriggerPressure = leftTrigger.action.ReadValue<float>();
        bool reversePressed = reverseButton.action.triggered;

        // Toggle reverse state when the reverse button is pressed
        if (reversePressed)
        {
            isReversing = !isReversing;
        }

        // Calculate motor and brake torque
        float motorTorque = rightTriggerPressure * maxMotorTorque * (isReversing ? -1 : 1); // Invert torque for reverse
        float brakeTorque = leftTriggerPressure * maxBrakeTorque;

        // Apply motor and brake torques
        ApplyTorque(motorTorque, brakeTorque);

        // Apply steering
        float steeringAngle = (knob.value - 0.5f) * steeringMax;
        wheels[0].steerAngle = steeringAngle;
        wheels[1].steerAngle = steeringAngle;
    }

    private void ApplyTorque(float motorTorque, float brakeTorque)
    {
        foreach (WheelCollider wheel in wheels)
        {
            wheel.motorTorque = motorTorque;
            wheel.brakeTorque = brakeTorque;
        }
    }
}
