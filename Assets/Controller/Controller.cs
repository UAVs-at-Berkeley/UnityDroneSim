using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public float Throttle = 0.0f;
	public float Yaw = 0.0f;
	public float Pitch = 0.0f;
	public float Roll = 0.0f;

    public enum ThrottleMode { None, LockHeight};

	[Header("Throttle command")]
	public string ThrottleCommand = "Throttle";
	public bool InvertThrottle = true;

    [Header("Yaw Command")]
	public string YawCommand = "Yaw";
	public bool InvertYaw = false;

	[Header("Pitch Command")]
	public string PitchCommand = "Pitch";
	public bool InvertPitch = true;

	[Header("Roll Command")]
	public string RollCommand = "Roll";
	public bool InvertRoll = true;

	void Update() {
        Throttle = Input.GetAxisRaw(ThrottleCommand) * (InvertThrottle ? -1 : 1);
        Yaw = Input.GetAxisRaw(YawCommand) * (InvertYaw ? -1 : 1);
        Pitch = Input.GetAxisRaw(PitchCommand) * (InvertPitch ? -1 : 1);
        Roll = Input.GetAxisRaw(RollCommand) * (InvertRoll ? -1 : 1);
	}

}
