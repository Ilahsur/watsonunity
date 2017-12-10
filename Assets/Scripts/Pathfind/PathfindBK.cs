using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindBK : MonoBehaviour
{

    public GameObject Start;
    public GameObject Goal;

    List<Vector3> BestPath;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed");
            GetAStarPath(Start, Goal);
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            GameObject[] Nodes = GameObject.FindGameObjectsWithTag("Fill");
            Start = Nodes[Random.Range(0, Nodes.Length)];
            Goal = Nodes[Random.Range(0, Nodes.Length)];
        }
    }

    void GetAStarPath(GameObject start, GameObject goal)
    {
        if (!start || !goal)
        {
            Debug.Log("Null start or goal node");
            return;
        }
        if(start == goal)
        {
            Debug.Log("Start and Goal were the same");
            return;
        }
        Debug.Log("Start = " + start.transform.position);
        Debug.Log("Goal = " + goal.transform.position);

        //Set of nodes already evaluated
        List<GameObject> closedSet = new List<GameObject>();

        //Nodes to be evaluated
        List<GameObject> openSet = new List<GameObject>();
        openSet.Add(start);

        //Map containing most effecient path that led to this node.
        start.GetComponent<AStar>().cameFrom = null;

        // the score to get from start to this node
        start.GetComponent<AStar>().gScore = 0;

        //Cost of getting from start node to goal along this path
        //Partially known, partially determined by hueristic
        start.GetComponent<AStar>().fScore = Vector3.Distance(start.transform.position, goal.transform.position);

        List<GameObject> neighbors = new List<GameObject>();

        // while (openSet.Count > 0)
        int cnt = 0;
      while(cnt<500)
        {
            neighbors.Clear();

            cnt++;
            GameObject current = start;
            foreach (GameObject node in openSet)
            {
                //Get lowest fScore node in openSet;
                if (node.GetComponent<AStar>().fScore < current.GetComponent<AStar>().fScore)
                {
                    current = node;
                }
            }
            if (current == goal)
            {
                Debug.Log("Found Path after " + cnt + " cycles");
                
                ConstructPath(current);
                return;
            }

            //  Debug.Log("Current = " + current.transform.position);

            openSet.Remove(current);
            closedSet.Add(current);

            //For each neighbor
            neighbors.Add(current.GetComponent<GetNeighbors>().up);
            neighbors.Add(current.GetComponent<GetNeighbors>().down);
            neighbors.Add(current.GetComponent<GetNeighbors>().right);
            neighbors.Add(current.GetComponent<GetNeighbors>().left);
            neighbors.Add(current.GetComponent<GetNeighbors>().forward);
            neighbors.Add(current.GetComponent<GetNeighbors>().back);
            foreach (GameObject neighbor in neighbors)
            {
                if (neighbor)
                {
                    if (closedSet.Contains(neighbor))
                    {
                        continue; //ignore node's already checked
                    }

                    float tentative_gScore = current.GetComponent<AStar>().gScore
                        + Vector3.Distance(current.transform.position, neighbor.transform.position);

                    //if openset does not contain neighbor
                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }

                    else if (tentative_gScore >= neighbor.GetComponent<AStar>().gScore)
                    {
                        //Path using this  neighbor is worse
                        continue;
                    }

                    //This is the next best node, record this
                    neighbor.GetComponent<AStar>().cameFrom = current;
                    neighbor.GetComponent<AStar>().gScore = tentative_gScore;
                    neighbor.GetComponent<AStar>().fScore = neighbor.GetComponent<AStar>().gScore
                        + Vector3.Distance(neighbor.transform.position, goal.transform.position);

                }
            }
        }

        Debug.Log("Failed to find path");
        return;
       
    }


    private List<Vector3> ConstructPath(GameObject current)
    {
        List<Vector3> path = new List<Vector3>();
        path.Add(current.transform.position);

        GameObject cameFrom = current.GetComponent<AStar>().cameFrom;

        while (cameFrom != null)
        {
            path.Add(cameFrom.transform.position);
            cameFrom = cameFrom.GetComponent<AStar>().cameFrom;
        }

        foreach(Vector3 v3 in path)
        {
            Debug.Log("Path is... " + v3);
        }
        return path;
    }
}
