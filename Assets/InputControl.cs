using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour {

	public CubeControl cc;

	private float abs_yaw;
	private float abs_height = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		cc.desired_vx = Input.GetAxisRaw ("Vertical");
		cc.desired_vy = Input.GetAxisRaw ("Horizontal");
		abs_yaw += Input.GetAxisRaw ("Yaw") * 0.01f;
		abs_yaw = abs_yaw % 360.0f;
		abs_height += Input.GetAxisRaw("Throttle") * 0.01f;

		cc.desired_yaw = abs_yaw;
		cc.desired_height = abs_height;
	}
}
