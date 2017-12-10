using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTrail : MonoBehaviour {

	public GameObject trailObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Instantiate (trailObject, transform.position + new Vector3(Random.Range(-.2f, .2f), Random.Range(-.05f, .05f), Random.Range(0, .1f)), Quaternion.identity);
	}
}
