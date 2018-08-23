using UnityEngine;
using System.Collections;

// Basic gyroscope simulator.  Uses the zero and identity to calculate.  This one suffre from gimball lock effect
[System.Serializable]
public class BasicGyro {


	public float Pitch; // The current pitch for the given transform
	public float Roll; // The current roll for the given transform
	public float Yaw; // The current Yaw for the given transform
    public float Altitude; // The current altitude from the zero position
    public Vector3 VelocityVector; // Velocity vector
    public float VelocityScalar; // Velocity scalar value

    public void UpdateGyro(Transform transform) {
		Pitch = transform.eulerAngles.x;
		Pitch = (Pitch > 180) ? Pitch - 360 : Pitch;
		
		Roll = transform.eulerAngles.z;
		Roll = (Roll > 180) ? Roll - 360 : Roll;

		Yaw = transform.eulerAngles.y;
		Yaw = (Yaw > 180) ? Yaw - 360 : Yaw;

        Altitude = transform.position.y;

        VelocityVector = transform.GetComponent<Rigidbody>().velocity;
        VelocityScalar = VelocityVector.magnitude;
	}
}
