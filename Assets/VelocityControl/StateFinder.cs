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

	public VelocityControl vc; // linked externally

	public void GetState() {
		// relative to object

		float Pitch = vc.transform.eulerAngles.x;
		Pitch = (Pitch > 180) ? Pitch - 360 : Pitch;
		Pitch = Pitch / 180.0f * 3.1416f; // Convert to radians

		float Roll = vc.transform.eulerAngles.z;
		Roll = (Roll > 180.0f) ? Roll - 360.0f : Roll;
		Roll = Roll / 180.0f * 3.1416f; // Convert to radians

		float Yaw = vc.transform.eulerAngles.y;
		Yaw = (Yaw > 180.0f) ? Yaw - 360.0f : Yaw;
		Yaw = Yaw / 180.0f * 3.1416f; // Convert to radians

		Altitude = vc.transform.position.y;

		Angles = new Vector3 (Pitch, Yaw, Roll);
		VelocityVector = vc.transform.GetComponent<Rigidbody> ().velocity;
		AngularVelocityVector = vc.transform.GetComponent<Rigidbody> ().angularVelocity;

		Inertia = vc.transform.GetComponent<Rigidbody> ().inertiaTensor;
		Mass = vc.transform.GetComponent<Rigidbody> ().mass;

	}
}
