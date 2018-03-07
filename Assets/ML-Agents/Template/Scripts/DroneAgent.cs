using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAgent: Agent {

	public BasicControl basicControl;
	public Transform Target;
	[Range(0,100)] public float Scale;

	private bool collided = false;

	public override List<float> CollectState()
	{
		List<float> state = new List<float>();
// 13 elements
		state.Add (basicControl.Computer.Gyro.Pitch / basicControl.Computer.PitchLimit);
		state.Add (basicControl.Computer.Gyro.Roll / basicControl.Computer.RollLimit);
		state.Add (basicControl.Computer.Gyro.Yaw / (180));

		state.Add (basicControl.transform.position.x);
		state.Add (basicControl.transform.position.y);
		state.Add (basicControl.transform.position.z);

		state.Add (basicControl.transform.rotation.x);
		state.Add (basicControl.transform.rotation.y);
		state.Add (basicControl.transform.rotation.z);

		state.Add (Target.transform.position.x);
		state.Add (Target.transform.position.y);
		state.Add (Target.transform.position.z);
		state.Add ((collided ? 1.0f : 0.0f));
		return state;
	}

	// 3 element input
	public override void AgentStep(float[] act)
	{
		// add in code logic for drone control
		basicControl.Controller.InputAction(0, act[0], act[1], act[2]);

		reward += RewardFunction();
	}

	public override void AgentReset()
	{
		basicControl.Controller.InputAction(0, 0, 0, 0);
	}

	public override void AgentOnDone()
	{

	}

	// super basic reward function
	float RewardFunction(){
		if (collided) {
			collided = false;
			done = true;
			return -1000.0f;
		} else {
			//euclidean horizontal plane distance
			float dist = Mathf.Pow(Target.position.x - basicControl.transform.position.x, 2) + Mathf.Pow(Target.position.z - basicControl.transform.position.z, 2);
			dist = Scale * dist;
			return 1.0f / dist;
		}
			
	}

	void OnTriggerEnter(Collider other)
	{
		print ("COLLISION");
		collided = true;
	}
}
