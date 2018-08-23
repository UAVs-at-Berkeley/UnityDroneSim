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
        ClampingSpeedValues();
        Swerve();

        drone.AddRelativeForce(Vector3.up*upForce);
        drone.rotation = Quaternion.Euler(
            new Vector3(tiltAmountForward, currentYRotation, tiltAmountSideways)
        );
    }

    public float upForce;
    void MovementUpDown(){
        if(Mathf.Abs(Input.GetAxis("Vertical")) > .2f || Mathf.Abs(Input.GetAxis("Horizontal")) > .2f) {
            if(Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.K)){
                drone.velocity = drone.velocity;
            }
            if(!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.J) && !Input.GetKey(KeyCode.L)) {
                drone.velocity = new Vector3(drone.velocity.x, Mathf.Lerp(drone.velocity.y, 0, Time.deltaTime * 5), drone.velocity.z);
                upForce = 281;
            }
            if (!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L)){
                drone.velocity = new Vector3(drone.velocity.x, Mathf.Lerp(drone.velocity.y, 0, Time.deltaTime * 5), drone.velocity.z);
                upForce = 110;
            }
            if(Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L)) {
                upForce = 410;
            }
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) < .2f && Mathf.Abs(Input.GetAxis("Horizontal")) > .2f){
            upForce = 135; 
        }


        if(Input.GetKey(KeyCode.I)){
            upForce = 450;
            if(Mathf.Abs(Input.GetAxis("Horizontal")) > .2f) {
                upForce = 500; 
            }
        } else if (Input.GetKey(KeyCode.K)) {
            upForce = -200;
        } else if (!Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.I) && Mathf.Abs(Input.GetAxis("Vertical")) < .2f && Mathf.Abs(Input.GetAxis("Horizontal")) < .2f) {
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
    public float currentYRotation;
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

    private Vector3 velocityToSmoothDampingToZero;
    void ClampingSpeedValues(){
        if(Mathf.Abs(Input.GetAxis("Vertical")) > .2f && Mathf.Abs(Input.GetAxis("Horizontal")) > .2f) {
            drone.velocity = Vector3.ClampMagnitude(drone.velocity, Mathf.Lerp(drone.velocity.magnitude, 10.0f, Time.deltaTime * 5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > .2f && Mathf.Abs(Input.GetAxis("Horizontal")) < .2f){
            drone.velocity = Vector3.ClampMagnitude(drone.velocity, Mathf.Lerp(drone.velocity.magnitude, 10.0f, Time.deltaTime * 5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) < .2f && Mathf.Abs(Input.GetAxis("Horizontal")) > .2f){
            drone.velocity = Vector3.ClampMagnitude(drone.velocity, Mathf.Lerp(drone.velocity.magnitude, 10.0f, Time.deltaTime * 5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) < .2f && Mathf.Abs(Input.GetAxis("Horizontal")) < .2f) {
            drone.velocity = Vector3.SmoothDamp(drone.velocity, Vector3.zero, ref velocityToSmoothDampingToZero, .95f);
        }
    }

    private float sideMovementAmount = 300.0f;
    private float tiltAmountSideways;
    private float tiltAmountVelocity;
    void Swerve() {
        if(Mathf.Abs(Input.GetAxis("Horizontal")) > .2f){
            drone.AddRelativeForce(Vector3.right*Input.GetAxis("Horizontal")*sideMovementAmount);
            tiltAmountSideways = Mathf.SmoothDamp(tiltAmountSideways, -20 * Input.GetAxis("Horizontal"), ref tiltAmountVelocity, .1f);
        } else {
            tiltAmountSideways = Mathf.SmoothDamp(tiltAmountSideways, 0, ref tiltAmountVelocity, .1f);
        }

    }
}

