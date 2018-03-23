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

	public CubeControl cc; // linked externally

	public void GetState() {

		Vector3 worldDown = cc.transform.InverseTransformDirection (Vector3.down);
		float Pitch = worldDown.z; // Small angle approximation
		float Roll = -worldDown.x; // Small angle approximation
		float Yaw = cc.transform.eulerAngles.y;

		Angles = new Vector3 (Pitch, Yaw, Roll);

		Altitude = cc.transform.position.y;

		VelocityVector = cc.transform.GetComponent<Rigidbody> ().velocity;
		VelocityVector = cc.transform.InverseTransformDirection (VelocityVector);

		AngularVelocityVector = cc.transform.GetComponent<Rigidbody> ().angularVelocity;
		AngularVelocityVector = cc.transform.InverseTransformDirection (AngularVelocityVector);

		if (flag) {
			Inertia = cc.transform.GetComponent<Rigidbody> ().inertiaTensor;
			Mass = cc.transform.GetComponent<Rigidbody> ().mass;
			flag = false;
		}

	}
}
