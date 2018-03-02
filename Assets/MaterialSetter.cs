using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSetter : MonoBehaviour {

	public Material m;
	public Color color = Color.blue;
	public Renderer rend;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		rend.enabled = true;
		m.color = color;
		rend.sharedMaterial = m;
	}

}
