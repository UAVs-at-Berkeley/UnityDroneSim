using UnityEngine;

using System.Collections;

// Module reuniting computing parts of a drone.  Used by a BasicControl.
public class ComputerModule : MonoBehaviour
{
    [Header("Settings")]
    [Range(0, 90)] public float PitchLimit;
    [Range(0, 90)] public float RollLimit;

    [Header("Parts")]
    public PID PidThrottle;
    public PID PidPitch;
    public PID PidRoll;
    public BasicGyro Gyro;


    [Header("Values")]
    public float PitchCorrection;
    public float RollCorrection;
    public float HeightCorrection;

    public void UpdateComputer(float ControlPitch, float ControlRoll, float ControlHeight)
    {
//		string cp = ControlPitch.ToString();
//		int ip = int.Parse (cp);
//		string cr = ControlRoll.ToString();
//		int ir = int.Parse (cr);
        UpdateGyro();
		PitchCorrection = PidPitch.Update(ControlPitch * PitchLimit, Gyro.Pitch, Time.deltaTime);
		RollCorrection = PidRoll.Update(Gyro.Roll, ControlRoll * RollLimit, Time.deltaTime);
        HeightCorrection = PidThrottle.Update(ControlHeight, Gyro.VelocityVector.y, Time.deltaTime);

    }

    public void UpdateGyro()
    {
        Gyro.UpdateGyro(transform);
    }
}
