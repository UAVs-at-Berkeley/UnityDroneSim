using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateFinder : MonoBehaviour {
//	public float Pitch; // The current pitch for the given transform in radians
//	public float Roll; // The current roll for the given transform in radians
//	public float Yaw; // The current Yaw for the given transform in radians
	public float Altitude; // The current altitude from the zero position
	public Vector3 Angles;
	public Vector3 VelocityVector; // Velocity vector
	public Vector3 AngularVelocityVector; // Angular Velocity
	public Vector3 Inertia;
	public float Mass;

	private bool flag = true; // Only get mass and inertia once 

	public VelocityControl vc; // linked externally

	public void GetState() {

		Vector3 worldDown = vc.transform.InverseTransformDirection (Vector3.down);
		float Pitch = worldDown.z; // Small angle approximation
		float Roll = -worldDown.x; // Small angle approximation
		float Yaw = vc.transform.eulerAngles.y;

//		float Pitch = cc.transform.eulerAngles.x;
//		Pitch = (Pitch > 180) ? Pitch - 360 : Pitch;
//		Pitch = Pitch / 180.0f * 3.1416f; // Convert to radians
//
//		float Roll = cc.transform.eulerAngles.z;
//		Roll = (Roll > 180.0f) ? Roll - 360.0f : Roll;
//		Roll = Roll / 180.0f * 3.1416f; // Convert to radians
//
//		float Yaw = cc.transform.eulerAngles.y;
//		Yaw = (Yaw > 180.0f) ? Yaw - 360.0f : Yaw;
//		Yaw = Yaw / 180.0f * 3.1416f; // Convert to radians

//		Altitude = cc.transform.position.y;
//
		Angles = new Vector3 (Pitch, Yaw, Roll);

		Altitude = vc.transform.position.y;

		VelocityVector = vc.transform.GetComponent<Rigidbody> ().velocity;
		VelocityVector = vc.transform.InverseTransformDirection (VelocityVector);

		AngularVelocityVector = vc.transform.GetComponent<Rigidbody> ().angularVelocity;
		AngularVelocityVector = vc.transform.InverseTransformDirection (AngularVelocityVector);

		if (flag) {
			Inertia = vc.transform.GetComponent<Rigidbody> ().inertiaTensor;
			Mass = vc.transform.GetComponent<Rigidbody> ().mass;
			flag = false;
		}

	}

	public void Reset() {
		flag = true;
		VelocityVector = Vector3.zero;
		AngularVelocityVector = Vector3.zero;
		Angles = Vector3.zero;
		Altitude = 0.0f;

		enabled = true;
	}
}
