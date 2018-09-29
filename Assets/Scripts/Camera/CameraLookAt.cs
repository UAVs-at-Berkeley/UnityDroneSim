using UnityEngine;
using System.Collections;
using System;

public class CameraLookAt : MonoBehaviour {

//	public Transform Target;
	public GameObject Drone;
    private Quaternion original_rotation;

	public Vector3 offset;
//	private Vector3 rotation_off;

	void Start() {
        //		offset = transform.position - Drone.transform.position;
        //		rotation_off = transform.rotation - Drone.transform.rotation;
        original_rotation = transform.rotation;
	}
	// Update is called once per frame
	void Update () {
        //		transform.LookAt (Target);
        float yaw = Drone.transform.localEulerAngles.y;
        Vector3 relative_offset = Quaternion.AngleAxis(yaw, Vector3.up) * offset;
        transform.position = Drone.transform.position + relative_offset;

        transform.rotation = Quaternion.AngleAxis(yaw, Vector3.up) * original_rotation;
	}
}
