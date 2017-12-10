using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FillVolume : MonoBehaviour {

    public float waitTime = .1f;

    static int numberOfNodes; //Keep track of number of nodes
    int debugNumberOfNodes;
    public int maxNodes;

    public GameObject fill;
    float thisTime;
    bool scanned = false;

    Vector3[] directions = new Vector3[] { Vector3.forward, -Vector3.forward, Vector3.up, -Vector3.up, Vector3.right, -Vector3.right };

    // Use this for initialization
    void Awake()
    {
        thisTime = Time.time;

        numberOfNodes++; //inc node counter
        debugNumberOfNodes = numberOfNodes;
    }

    // Update is called once per frame    
    void FixedUpdate()
    {
         Grow();
    }

    public void GetNeighbors()
    {

    }

    public void Grow()
    {
        if (Time.time > thisTime + waitTime && !scanned)
        {
            for (int j = 0; j < directions.Length; j++)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directions[j], out hit))
                {
                    if (hit.transform.tag != "Fill")
                    {
                        for (int i = 1; i < (int)hit.distance; i++)
                        {
                            if (numberOfNodes < maxNodes)
                            {

                                GameObject newFill = Instantiate(fill, transform.position + directions[j] * i, Quaternion.identity);
                                newFill.GetComponent<FillVolume>().waitTime = j + i / 50;

                                //Add reference to object to neighbors array
                                newFill.name = "Fill";
                            }
                        }
                    }
                }
                scanned = true;
            }
        }
    }
}
