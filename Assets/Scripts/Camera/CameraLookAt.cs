using UnityEngine;
using System.Collections;
using System;

public class CameraLookAt : MonoBehaviour {

//	public Transform Target;
	public GameObject Drone;

	public Vector3 offset;
//	private Vector3 rotation_off;

	void Start() {
//		offset = transform.position - Drone.transform.position;
//		rotation_off = transform.rotation - Drone.transform.rotation;
	}
	// Update is called once per frame
	void Update () {
//		transform.LookAt (Target);
		transform.position = Drone.transform.position + offset;
	}
}
