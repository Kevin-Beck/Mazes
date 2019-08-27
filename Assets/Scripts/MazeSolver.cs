﻿using System.Collections.Generic;
using UnityEngine;

public class MazeSolver : MonoBehaviour
{
    Stack<Vector3> Path;
    public bool[,] visited;
    int NodeWidth = 4;
    public Vector3 StartPos;
    Vector3 FinishPos;
    public bool isSecondSolver;
    private int curX;
    private int curZ;

    public FloatReference timeFactor;

    public FloatReference mazeSize;
    public GameEvent MazeSolved;
    public GameObject PathTile;
    public Vector3 DropInHeight;

    private int noWayCounter = 0;
    
    public void SolveMaze()
    {
        if (isSecondSolver)
        {
            FinishPos = new Vector3(0, 2.1f, (mazeSize.Value - 1) * NodeWidth);
            StartPos= new Vector3((mazeSize.Value - 1) * NodeWidth, 2.1f, 0);
            transform.position = StartPos;
        }
        else
            FinishPos = new Vector3((mazeSize.Value-1)*NodeWidth, 2.1f, (mazeSize.Value-1)*NodeWidth);

        Path = new Stack<Vector3>();

        curX = Mathf.RoundToInt(StartPos.x)/NodeWidth;
        curZ = Mathf.RoundToInt(StartPos.z)/NodeWidth;

        visited = new bool[Mathf.RoundToInt(mazeSize.Value), Mathf.RoundToInt(mazeSize.Value)];
        for(int i = 0; i < Mathf.RoundToInt(mazeSize.Value); i++)
        {
            for(int j = 0; j < Mathf.RoundToInt(mazeSize.Value); j++)
            {
                visited[i,j] = false;
            }
        }
        
        SolveStep();
    }
    public void SolveStep()
    {
        if(CheckIfWeAreCloseEnough())
        {
            Path.Push(transform.position);
            CancelInvoke("SolveStep");
            GenerateRoad();
        }
        else
        {
            // Save the current path we are on
            Path.Push(transform.position);

            curX = Mathf.RoundToInt(transform.position.x) / NodeWidth;
            curZ = Mathf.RoundToInt(transform.position.z) / NodeWidth;
            // Mark our current spot as explored
            visited[curX, curZ] = true;

            // if we cannot move to a valid location false is returned and we need to move back a spot.
            if (!MoveToValidPosition())
            {
                Path.Pop();
                if (noWayCounter > 3)
                    Debug.Log("NoWayOut");
                if (Path.Count == 0)
                    noWayCounter++;
                else
                    transform.position = Path.Pop();
            }            
            Invoke("SolveStep", .05f/timeFactor.Value);
        }
        
    }


    private bool CheckIfWeAreCloseEnough()
    {
        return Vector3.Distance(transform.position, FinishPos) < 1f;
    }
    private bool MoveToValidPosition()
    {
        bool successfulMove = false;
        RaycastHit hit;
        LayerMask lm = ~0;
        // Check North
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 3f, lm) || curZ+1 >= Mathf.RoundToInt(mazeSize.Value) || visited[curX, curZ+1])
        {
            // if we hit a wall north, or north is off the charts, or north has been visited -- move on we cant move there
        }
        else
        {
            successfulMove = true;
            curZ += 1;
            transform.position += new Vector3(0, 0, NodeWidth);
            return successfulMove;
        }
        
        // Check East
        if (Physics.Raycast(transform.position, Vector3.right, out hit, 3f, lm) || curX+1 >= Mathf.RoundToInt(mazeSize.Value) || visited[curX+1, curZ])
        {
        }
        else
        {
            successfulMove = true;
            curX += 1;
            transform.position += new Vector3(NodeWidth, 0, 0);
            return successfulMove;
        }

        // Check South
        if (Physics.Raycast(transform.position, Vector3.forward * -1, out hit, 3f, lm) || curZ-1 < 0 || visited[curX, curZ-1])
        {
            // if we hit a wall north, or north is off the charts, or north has been visited -- move on
        }
        else
        {
            successfulMove = true;
            curZ -= 1;
            transform.position -= new Vector3(0, 0, NodeWidth);
            return successfulMove;
        }

        // Check West
        if (Physics.Raycast(transform.position, Vector3.right * -1, out hit, 3f, lm) || curX-1 < 0 || visited[curX-1, curZ])
        {
            // if we hit a wall north, or north is off the charts, or north has been visited -- move on
        }
        else
        {
            successfulMove = true;
            curX -= 1;
            transform.position -= new Vector3(NodeWidth, 0, 0);
            return successfulMove;
        }
        
        return successfulMove;
    }
    public void GenerateRoad()
    {
        if (Path.Count == 0)
        {
            MazeSolved.Raise();
        }
        else
        {
            GameObject go = Instantiate(PathTile, Path.Pop() + DropInHeight, Quaternion.identity);
            go.transform.parent = transform;
            Invoke("GenerateRoad", .07f / (timeFactor.Value * 2));
        }
    }
    public void Reset()
    {
        transform.position = StartPos;
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }
}
