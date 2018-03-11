using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BasicControl : MonoBehaviour {

	[Header("Control")]
	public Controller Controller;
	public float ThrottleIncrease;
	
	[Header("Motors")]
	public Motor[] Motors;
	public float ThrottleValue;

    [Header("Internal")]
    public ComputerModule Computer;

	void FixedUpdate() {
        Computer.UpdateComputer(Controller.Pitch, Controller.Roll, Controller.Throttle * ThrottleIncrease);
        ThrottleValue = Computer.HeightCorrection;
//		Debug.Log (Computer.PitchCorrection);
        ComputeMotors();
        if (Computer != null)
            Computer.UpdateGyro();
        ComputeMotorSpeeds();
	}

    private void ComputeMotors()
    {
        float yaw = 0.0f;
        Rigidbody rb = GetComponent<Rigidbody>();
        int i = 0;
        foreach (Motor motor in Motors)
        {
            motor.UpdateForceValues();
            yaw += motor.SideForce;
            i++;
            Transform t = motor.GetComponent<Transform>();
//			Debug.Log (i);
//			Debug.Log (motor.UpForce);
			rb.AddForceAtPosition(transform.up * motor.UpForce, t.position, ForceMode.Impulse);
        }
        rb.AddTorque(Vector3.up * yaw, ForceMode.Force);
    }

    private void ComputeMotorSpeeds()
    {
        foreach (Motor motor in Motors)
        {
            if (Computer.Gyro.Altitude < 0.1)
                motor.UpdatePropeller(0.0f);
            else
                motor.UpdatePropeller(1200.0f);
        }
    }
}