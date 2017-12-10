using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillVolume2 : MonoBehaviour {


	public GameObject fill;

	bool scanned = false;
//											forward				back			up			down		right			left
	Vector3[] directions = new Vector3[]{Vector3.forward, -Vector3.forward, Vector3.up, -Vector3.up, Vector3.right, -Vector3.right};

	void Update () {
		if (!scanned) {
			for (int j = 0; j < directions.Length; j++) {
				RaycastHit hit;
				if (Physics.Raycast (transform.position, directions [j], out hit, 50, -1, QueryTriggerInteraction.Collide)) {
					if (hit.transform.tag != "Fill") {
						for (int i = 1; i < (int)hit.distance; i++) {
							GameObject newFill = Instantiate (fill, transform.position + directions [j] * i, Quaternion.identity);
							newFill.name = "Fill";
						}
					}
				}
				scanned = true;
			}
		}
	}
}