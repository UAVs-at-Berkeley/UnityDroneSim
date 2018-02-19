using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovementScript : MonoBehaviour {

    Rigidbody drone;

    void Awake(){
        drone = GetComponent<Rigidbody>();
    }

    private void FixedUpdate(){
        MovementUpDown();
        MovementForward();
        Rotation();
        drone.AddRelativeForce(Vector3.up*upForce);
        drone.rotation = Quaternion.Euler(
            new Vector3(tiltAmountForward, currentYRotation, drone.rotation.z)
        );
    }

    public float upForce;
    void MovementUpDown(){
        if(Input.GetKey(KeyCode.I)){
            upForce = 450;
        } else if (Input.GetKey(KeyCode.K)) {
            upForce = -200;
        } else if (!Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.I)) {
            upForce = 98.1f;
        }
    }
    private float movementForwardSpeed = 500.0f;
    private float tiltAmountForward = 0;
    private float tiltVelocityForward;
    void MovementForward(){
        if(Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0) {
            drone.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementForwardSpeed);
            tiltAmountForward = Mathf.SmoothDamp(tiltAmountForward, 20 * Input.GetAxis("Vertical"), ref tiltVelocityForward, 0.1f);
        }
    }

    private float wantedYRotation;
    private float currentYRotation;
    private float rotateAmountByKeys = 2.5f;
    private float rotationYVelocity;
    void Rotation(){
        if(Input.GetKey(KeyCode.J)){
            wantedYRotation -= rotateAmountByKeys;
        }
        if(Input.GetKey(KeyCode.L)){
            wantedYRotation += rotateAmountByKeys;
        }
        currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, .25f);
    }
}
