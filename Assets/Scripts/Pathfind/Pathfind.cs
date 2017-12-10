using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfind : MonoBehaviour
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

        if (Input.GetKeyDown(KeyCode.I))
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
            Debug.Log("Start or goal is null");
            return;
        }
        if (start == goal)
        {
            Debug.Log("Start and Goal were the same");
            return;
        }
        Debug.Log("Start = " + start.transform.position);
        Debug.Log("Goal = " + goal.transform.position);

        //Set of nodes already evaluated
        List<Vector3> closedSet = new List<Vector3>();

        //Nodes to be evaluated
        Dictionary<Vector3, GameObject> nodeHash = new Dictionary<Vector3, GameObject>(); //Hash the nodes based on there positions

        GameObject goCurrent = start;
        Vector3 posCurrent = start.transform.position;

        nodeHash.Add(posCurrent, goCurrent);

        List<Vector3> openSet = new List<Vector3>();
        openSet.Add(posCurrent);

        //Map containing most effecient path that led to this node.
        Dictionary<Vector3, Vector3> cameFrom = new Dictionary<Vector3, Vector3>(); //Maps nodes to cameFrom nodes

        // the score to get from start to this node
        Dictionary<Vector3, float> gScore = new Dictionary<Vector3, float>();  //Maps the gScore of each positions
        gScore.Add(posCurrent, 0.0f);

        //Cost of getting from start node to goal along this path
        //Partially known, partially determined by hueristic
        Dictionary<Vector3, float> fScore = new Dictionary<Vector3, float>(); //Maps the fScore of each position
        fScore.Add(posCurrent, 99999.9f);

        List<GameObject> neighbors = new List<GameObject>();
        int cnt = 0;

        //while (openSet.Count > 0)
        
        while (cnt < 1000)
        {
            neighbors.Clear();
            cnt++;

            Debug.Log("current = " + goCurrent + " ," + posCurrent);

            float res1 = 999999;
            float res2 = 999999;

            foreach (Vector3 pos in openSet)
            {
                fScore.TryGetValue(pos, out res1);
                fScore.TryGetValue(posCurrent, out res2);

                //Get lowest fScore node in openSet;

                if (res1 < res2)
                {
                    posCurrent = pos;
                    goCurrent = nodeHash[posCurrent];
                }
            }

            if (posCurrent == goal.transform.position)
            {
                Debug.Log("Found Path after " + cnt + " cycles");
                //   ConstructPath(posCurrent, cameFrom);
                return;
            }

            //For each neighbor add it to the map if it exists.
            if(goCurrent.GetComponent<GetNeighbors>().up)
             neighbors.Add(goCurrent.GetComponent<GetNeighbors>().up);

            if (goCurrent.GetComponent<GetNeighbors>().down)
                neighbors.Add(goCurrent.GetComponent<GetNeighbors>().down);

            if (goCurrent.GetComponent<GetNeighbors>().right)
                neighbors.Add(goCurrent.GetComponent<GetNeighbors>().right);

            if (goCurrent.GetComponent<GetNeighbors>().left)
                neighbors.Add(goCurrent.GetComponent<GetNeighbors>().left);

            if (goCurrent.GetComponent<GetNeighbors>().forward)
                neighbors.Add(goCurrent.GetComponent<GetNeighbors>().forward);

            if (goCurrent.GetComponent<GetNeighbors>().back)
                neighbors.Add(goCurrent.GetComponent<GetNeighbors>().back);

            foreach (GameObject neighbor in neighbors)
            {
                Vector3 neighborPos = neighbor.transform.position;
                if (!nodeHash.ContainsKey(neighborPos))
                {
                    nodeHash.Add(neighborPos, neighbor);
                    Debug.Log("Added value");
                }

                if (closedSet.Contains(neighborPos))
                {
                    continue; //ignore node's already checked
                }

                float tentative_gScore = gScore[posCurrent]
                    + Vector3.Distance(posCurrent, neighborPos);

                //if openset does not contain neighbor
                if (!openSet.Contains(neighborPos))
                {
                   // Debug.Log("Added " + neighborPos + " to open set");
                    openSet.Add(neighborPos);
                }

                else if (tentative_gScore >= gScore[neighborPos])
                {
                    //Path using this  neighbor is worse
                    continue;
                }

                //This is the next best node, record this
                cameFrom.Add(neighborPos, posCurrent);
                gScore.Add(neighborPos, tentative_gScore);
                fScore.Add(neighborPos, gScore[neighborPos]
                    + Vector3.Distance(neighborPos, goal.transform.position));

            }

            openSet.Remove(posCurrent);
            Debug.Log("Open Set" + openSet.Count);

            closedSet.Add(posCurrent);
        }


        Debug.Log("Failed to find path " + cnt);
        //ConstructPath(posCurrent, cameFrom);

        return;

    }




    private List<Vector3> ConstructPath(Vector3 current, Dictionary<Vector3, Vector3> cameFrom)
    {
        List<Vector3> path = new List<Vector3>();
        path.Add(current);

        while (cameFrom != null)
        {
            path.Add(cameFrom[current]);
            Debug.Log("Help + " + path[0]);
            current = cameFrom[current];
        }

        foreach (Vector3 v3 in path)
        {
            Debug.Log("Path is... " + v3);
        }
        return path;
    }
}
