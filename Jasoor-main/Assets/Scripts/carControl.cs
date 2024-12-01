using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.XR.Content.Interaction;


public class carControl : MonoBehaviour
{

    public WheelCollider[] wheels = new WheelCollider[4];
    public InputActionReference rightTrigger;
    public InputActionReference leftTrigger;
    public XRKnob knob;
    public bool isPressed = false;
    public float motorTorque;
    public float breakTorque;
    public float steeringMax;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rightTrigger.action.WasPressedThisFrame())
        {
            for(int i=0; i<wheels.Length; i++)
            {
                wheels[i].brakeTorque = 0;
                wheels[i].motorTorque = motorTorque;

            }
        }
        if (rightTrigger.action.WasReleasedThisFrame())
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].brakeTorque = breakTorque;

            }
        }
        if (leftTrigger.action.WasPressedThisFrame())
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = 0;
                wheels[i].brakeTorque = breakTorque;

            }
        }
        if (leftTrigger.action.WasReleasedThisFrame())
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = motorTorque;

            }
        }
        
        // Apply steering to front wheels only
        wheels[0].steerAngle = (knob.value - 0.5f) * steeringMax;
        wheels[1].steerAngle = (knob.value - 0.5f) * steeringMax;

    }
}
