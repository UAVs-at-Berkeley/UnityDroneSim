using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAgent: Agent {

	public BasicControl basicControl;
	[Range(0,100)] public float Scale;

	public GameObject startRegion;
	public GameObject endRegion;
	private Bounds endBounds;

	private bool collided = false;

	private bool wait = false;

	private Vector3 initialPos;

	private float maxX;
	private float minX;
	private float maxZ;
	private float minZ;

	private System.Random rand;

	public override void InitializeAgent() {

		Debug.Log ("Start BOUNDS");
		Renderer rend = startRegion.GetComponent<Renderer>();
		Debug.Log(rend.bounds.max);
		Debug.Log(rend.bounds.min);

		maxX = rend.bounds.max.x;
		minX = rend.bounds.min.x;

		maxZ = rend.bounds.max.z;
		minZ = rend.bounds.min.z;

		rand = new System.Random ();

		initialPos = transform.position;

		endBounds = endRegion.GetComponent<Renderer> ().bounds;

		// randomness
		float startX = ((float) rand.NextDouble()) * (maxX - minX) + minX;
		float startZ = ((float) rand.NextDouble()) * (maxZ - minZ) + minZ;

		transform.position = new Vector3 (startX, initialPos.y, startZ);


		wait = false;

	}

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

		state.Add (endRegion.transform.position.x);
		state.Add (endRegion.transform.position.y);
		state.Add (endRegion.transform.position.z);
		state.Add ((collided ? 1.0f : 0.0f));
		return state;
	}

	// 3 element input
	public override void AgentStep(float[] act)
	{
		//only wait initially if we are a non external player
		if (wait && brain.brainType == BrainType.Player) {
			return;
		}
		// add in code logic for drone control
		basicControl.Controller.InputAction(0, act[0], act[1], act[2]);

		reward += RewardFunction();

		// done checking
		Vector3 currPos = new Vector3 (transform.position.x, endBounds.center.y, transform.position.z);
//		Debug.Log (currPos);
//		Debug.Log (endBounds);
		done = done || endBounds.Contains (currPos);

	}

	public override void AgentReset()
	{
		basicControl.Controller.InputAction(0, 0, 0, 0);

		// randomness
		float startX = ((float) rand.NextDouble()) * (maxX - minX) + minX;
		float startZ = ((float) rand.NextDouble()) * (maxZ - minZ) + minZ;

		transform.position = new Vector3 (startX, initialPos.y, startZ);
		transform.rotation = Quaternion.Euler (Vector3.forward);

		basicControl.Reset ();

		StartCoroutine (Waiting (0.5f));
	}

	IEnumerator Waiting(float time) {
		wait = true;
		yield return new WaitForSeconds(time);
		wait = false;
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
			float dist = Mathf.Pow(endRegion.transform.position.x - basicControl.transform.position.x, 2) + Mathf.Pow(endRegion.transform.position.z - basicControl.transform.position.z, 2);
			dist = Scale * dist;
			return 1.0f / dist;
		}
			
	}

	void OnCollisionEnter(Collision other)
	{
		Debug.LogWarning ("-- COLLISION --");
		collided = true;
	}
}
