using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelsControls : MonoBehaviour
{

[SerializeField] WheelCollider frontRight;
[SerializeField] WheelCollider frontLeft;
[SerializeField] WheelCollider backRight;
[SerializeField] WheelCollider backLeft;

[SerializeField] Transform frontRightTransform;
[SerializeField] Transform frontLeftTransform;
[SerializeField] Transform backRightTransform;
[SerializeField] Transform packLeftTransform;

public float acceleration = 1000f;
public float breakingForce = 800f;
public float maxTurnAngle = 15f;


private float currentAcceleration = 0f;
private float currentBreakingForce = 0f;
private float currentTurnAngle = 0f;

private void FixedUpdate(){


    // Get forward/reverse acceleration from the vertical axis (W and S keys)

currentAcceleration = acceleration * Input.GetAxis ("Vertical") ;

// If we're pressing space, give currentBreakingForce a value.

if (Input.GetKey(KeyCode.Space))
currentBreakingForce = breakingForce;
else
currentBreakingForce = 0f;


// Apply acceleration to front wheels.

frontRight.motorTorque=currentAcceleration;
frontLeft.motorTorque=currentAcceleration;

frontRight.brakeTorque= currentBreakingForce;
frontLeft.brakeTorque= currentBreakingForce;
backLeft. brakeTorque = currentBreakingForce;
backRight.brakeTorque= currentBreakingForce;

//Take care of the steering.
currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
frontLeft.steerAngle = currentTurnAngle;
frontRight.steerAngle = currentTurnAngle;


}


}
