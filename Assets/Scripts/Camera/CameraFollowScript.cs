using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {

    private Transform drone;
    void Awake(){
        drone = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    private Vector3 velocityCameraFollow;
    public Vector3 behindPosition = new Vector3(0,2,-4);
    public float angle;
    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, drone.transform.TransformPoint(behindPosition) + Vector3.up * Input.GetAxis("Vertical"), ref velocityCameraFollow, .1f);
        transform.rotation = Quaternion.Euler(new Vector3(angle, drone.GetComponent<DroneMovementScript>().currentYRotation, 0));
    }
}
