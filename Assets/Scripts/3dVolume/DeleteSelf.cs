using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSelf : MonoBehaviour {

	float startTime;
	float waitTime = 3f;

	// Use this for initialization
	void Start () {
		startTime = Time.time;

		float x = Random.Range (.01f, .3f);
		transform.localScale = new Vector3(x, x, x);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > startTime + waitTime) {
			Destroy (gameObject);
		}
	}
}
