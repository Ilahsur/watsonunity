using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour {

	float rate = 2;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") * Time.deltaTime * rate);
	}
}
