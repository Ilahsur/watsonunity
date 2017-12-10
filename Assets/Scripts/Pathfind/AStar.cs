using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
   public float gScore;
   public float fScore;
   public GameObject cameFrom;

    private void Start()
    {
        gScore = 9999999;
        fScore = 9999999;

        cameFrom = null;
    }
}

