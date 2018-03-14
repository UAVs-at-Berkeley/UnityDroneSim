using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour {

	public CubeControl cc;

	private float abs_height = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		cc.desired_vx = Input.GetAxisRaw ("Pitch")*2.0f;
		cc.desired_vy = Input.GetAxisRaw ("Roll")*2.0f;
		cc.desired_yaw = Input.GetAxisRaw ("Yaw")*0.5f;
		abs_height += Input.GetAxisRaw("Throttle") * 0.1f;

		cc.desired_height = abs_height;
	}
}
