using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistantAi : MonoBehaviour {

	//BUG: must layermask out the Assistant when raycasting to find neighbors


	Rigidbody rb;
	float flightSpeed = 3;
	GameObject[] volumeNodes;
	GameObject targetNode;
	float startTime;
	float waitTime = 1f;
	bool reachedWaypoint = true;
	float turnSpeed = .5f;

	// Use this for initialization
	void Start () {

		rb = gameObject.GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		if (Time.time > startTime + waitTime && reachedWaypoint) {
			volumeNodes = GameObject.FindGameObjectsWithTag ("Fill");
			do {
				targetNode = volumeNodes [Random.Range (0, volumeNodes.Length)];
			} while (CheckAttachedNodes(targetNode));
			reachedWaypoint = false;
		} 

		if (Vector3.Distance (transform.position, targetNode.transform.position) < 5) {
			reachedWaypoint = true;
		}

//		print (Vector3.Distance (transform.position, targetNode.transform.position));

		Wander ();
	}

	void Idle (){


	}

	void Wander() {
		rb.AddRelativeForce (Vector3.forward * flightSpeed);
//		transform.LookAt (targetNode.transform);
		Vector3 lookDirection = targetNode.transform.position - transform.position;
		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (lookDirection), Time.deltaTime * turnSpeed);
	}

	bool CheckAttachedNodes(GameObject node) {
		if (node.GetComponent<GetNeighbors> ().up != null & node.GetComponent<GetNeighbors> ().down != null & node.GetComponent<GetNeighbors> ().left != null & node.GetComponent<GetNeighbors> ().right != null & node.GetComponent<GetNeighbors> ().forward != null & node.GetComponent<GetNeighbors> ().back != null) {
			return true;
		} else {
			return false;
		}
	}
}
