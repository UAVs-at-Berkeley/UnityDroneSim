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

	public CubeControl cc; // linked externally

	public void GetState() {
		float Pitch = cc.transform.eulerAngles.x;
		Pitch = (Pitch > 180) ? Pitch - 360 : Pitch;
		Pitch = Pitch / 180.0f * 3.1416f; // Convert to radians

		float Roll = cc.transform.eulerAngles.z;
		Roll = (Roll > 180.0f) ? Roll - 360.0f : Roll;
		Roll = Roll / 180.0f * 3.1416f; // Convert to radians

		float Yaw = cc.transform.eulerAngles.y;
		Yaw = (Yaw > 180.0f) ? Yaw - 360.0f : Yaw;
		Yaw = Yaw / 180.0f * 3.1416f; // Convert to radians

		Altitude = cc.transform.position.y;

		Angles = new Vector3 (Pitch, Yaw, Roll);
		VelocityVector = cc.transform.GetComponent<Rigidbody> ().velocity;
		AngularVelocityVector = cc.transform.GetComponent<Rigidbody> ().angularVelocity;

		Inertia = cc.transform.GetComponent<Rigidbody> ().inertiaTensor;
		Mass = cc.transform.GetComponent<Rigidbody> ().mass;
	}
}
