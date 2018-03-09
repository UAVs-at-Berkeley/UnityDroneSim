using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPVCameraScript : MonoBehaviour {

	public Transform droneTransform;
	public Vector3 offset;
	[Range(0,1)] public float temp;
	[Range(0,1)] public float rpLimitRatio;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = droneTransform.position + droneTransform.rotation * offset;

		Vector3 euler = droneTransform.rotation.eulerAngles;
		float x = (euler.x > 180.0f ? euler.x - 360.0f : euler.x) * rpLimitRatio;
		float z = (euler.z > 180.0f ? euler.z - 360.0f : euler.z) * rpLimitRatio;

		float nx = (x > 0 ? x : 360.0f + x);
		float nz = (z > 0 ? z : 360.0f + z);

//		Debug.Log (nx);
//		Debug.Log (nz);
//	
		Vector3 newEuler = new Vector3 (nx, euler.y, nz);

//		Debug.Log (euler);
//		Debug.Log (newEuler);

		Quaternion target = Quaternion.Euler (newEuler);

//		transform.position = new Vector3 (droneTransform.position.x, droneTransform.position.y, droneTransform.position.z + 0.45f);
		transform.rotation = Quaternion.Slerp (transform.rotation, target, temp);
//			Vector3.SmoothDamp(transform.position, drone.transform.TransformPoint(behindPosition) + Vector3.up * Input.GetAxis("Vertical"), ref velocityCameraFollow, .1f);
	}
}
