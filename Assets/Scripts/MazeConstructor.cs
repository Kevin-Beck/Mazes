﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeConstructor : MonoBehaviour
{
    public GameObject MazeNodePrefab;
    public FloatReference MazeSize;
   // private int nodeSize = 4;

    public GameEvent MazeNodesInitialized;

    public void InitializeMazeNodes()
    {
        for(int i = 0; i < MazeSize.Value; i++)
        {
            for(int j = 0; j < MazeSize.Value; j++)
            {
                GameObject go = Instantiate(MazeNodePrefab);
                go.transform.position = new Vector3(i * 4, 0, j * 4);
                go.transform.parent = transform;

                if(i == 0 && j == 0)
                {
                    go.GetComponent<MazeNode>().startingNode = true;
                }else if(i==MazeSize.Value-1 && j == MazeSize.Value - 1)
                {
                    go.GetComponent<MazeNode>().endingNode = true;
                }
            }
        }

        Invoke("FinishedInitialization", .25f);
    }
    public void FinishedInitialization()
    {
        MazeNodesInitialized.Raise();
    }
    public void DeleteAllMazeNodes()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
