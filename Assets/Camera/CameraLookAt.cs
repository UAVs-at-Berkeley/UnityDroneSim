using UnityEngine;
using System.Collections;
using System;

public class CameraLookAt : MonoBehaviour {

	public Transform Target;
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (Target);
	}
}
