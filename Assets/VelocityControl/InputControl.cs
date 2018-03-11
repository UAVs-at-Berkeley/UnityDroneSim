using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour {

	public VelocityControl vc;

	private float abs_height = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		vc.desired_vx = Input.GetAxisRaw ("Pitch")*2.0f;
//		print(Input.GetAxisRaw("Vertical"));
		vc.desired_vy = Input.GetAxisRaw ("Roll")*2.0f;
		vc.desired_yaw = Input.GetAxisRaw ("Yaw");
		abs_height += Input.GetAxisRaw("Throttle") * 0.01f;

		Debug.Log (vc.desired_yaw);

		vc.desired_height = abs_height;
	}
}
