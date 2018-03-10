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

	public void GetState(Transform transform) {
		float Pitch = transform.eulerAngles.x;
		Pitch = (Pitch > 180) ? Pitch - 360 : Pitch;
		Pitch = Pitch / 180 * 3.1416; // Convert to radians

		float Roll = transform.eulerAngles.z;
		Roll = (Roll > 180) ? Roll - 360 : Roll;
		Roll = Roll / 180 * 3.1416; // Convert to radians

		float Yaw = transform.eulerAngles.y;
		Yaw = (Yaw > 180) ? Yaw - 360 : Yaw;
		Yaw = Yaw / 180 * 3.1416; // Convert to radians

		Altitude = transform.position.y;

		Angles = Vector3 (Pitch, Yaw, Roll);
		VelocityVector = transform.GetComponent<Rigidbody> ().velocity;
		AngularVelocityVector = transform.GetComponent<Rigidbody> ().angularVelocity;

		Inertia = transform.GetComponent<Rigidbody> ().inertiaTensor;
		Mass = transform.GetComponent<Rigidbody> ().mass;
	}
}
