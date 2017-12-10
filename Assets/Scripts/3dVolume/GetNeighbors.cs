using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetNeighbors : MonoBehaviour {

	public GameObject surface;																//initalizations
	public GameObject forward;
	public GameObject back;
	public GameObject up;
	public GameObject down;
	public GameObject right;
	public GameObject left;

	float minTime = 40;
	float maxTime = 60;

	GameObject[] neighbors = new GameObject[6];

	//initalize directions						forward				back			up			down		right			left
	Vector3[] directions = new Vector3[]{Vector3.forward, -Vector3.forward, Vector3.up, -Vector3.up, Vector3.right, -Vector3.right};

	float currentTime;
	float waitTime;

	// Use this for initialization
	void Start () {
		currentTime = Time.time;															//gets current wait time

		waitTime = Random.Range (5,10);														//sets initial wait time
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Time.time > currentTime + waitTime) {
			for (int i = 0; i < directions.Length; i++) {
				RaycastHit hit;
				if(Physics.Raycast(transform.position, directions[i], out hit, 1, -1, QueryTriggerInteraction.Collide)){		//only checks 1 unity in all directions 
					if (hit.transform.tag == "Fill" || hit.transform.tag == "Surface") {
						neighbors [i] = hit.transform.gameObject;							//if hit, set that node
					}
				}
			}

			forward = 	neighbors [0];
			back = 		neighbors [1];
			up = 		neighbors [2];
			down = 		neighbors [3];
			right = 	neighbors [4];
			left = 		neighbors [5];

			if(transform.tag == "Fill")
				gameObject.name = "Fill - Updated: " + Time.time;							//updates name/ time
			if(transform.tag == "Surface")
				gameObject.name = "Surface - Updated: " + Time.time;

			if (down == null && transform.tag != "Surface") {								//if there is no node below
				Instantiate (surface, transform.position, Quaternion.identity);				//creates surface node
				Destroy (gameObject);														//deletes current node
			}

			currentTime = Time.time;
			waitTime = Random.Range (minTime,maxTime);										//sets next random update time
		}
		
	}
}
