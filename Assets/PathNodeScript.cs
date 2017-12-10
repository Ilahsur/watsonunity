using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathNodeScript : MonoBehaviour
{
    static int numberOfNodes;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Start");
        numberOfNodes++;
 
    }

    public void saveVar(string key, Func<object> getter, Action<object> setter)
    {
    }


    // Update is called once per frame
    void Update()
    {

    }

    void Bloom()
    {
        GameObject pathNode = Resources.Load("Astar/PathNode") as GameObject;
        GameObject Up = GameObject.Instantiate(pathNode);
        Up.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);

        GameObject Down = GameObject.Instantiate(pathNode);
        Down.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 1, this.transform.position.z);

        GameObject Left = GameObject.Instantiate(pathNode);
        Left.transform.position = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z);

        GameObject Right = GameObject.Instantiate(pathNode);
        Right.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z);

        GameObject Front = GameObject.Instantiate(pathNode);
        Front.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1);

        GameObject Back = GameObject.Instantiate(pathNode);
        Back.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1);


        saveVar("Up", () => Up, v => { Up = (GameObject)v; });
        saveVar("Down", () => Up, v => { Up = (GameObject)v; });
        saveVar("Left", () => Up, v => { Up = (GameObject)v; });
        saveVar("Right", () => Up, v => { Up = (GameObject)v; });
        saveVar("Front", () => Up, v => { Up = (GameObject)v; });
        saveVar("Back", () => Up, v => { Up = (GameObject)v; });
    }
}
