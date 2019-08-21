using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSolver : MonoBehaviour
{
    Stack<Vector3> Path;
    List<Vector3> PositionsVisited;
    public Vector3 Start;
    public Vector3 Finish;

    GameEvent MazeSolved;

    public void SolveMaze()
    {
        Vector3 approxPosition = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
        transform.position = approxPosition;
        Path.Push(approxPosition);
        PositionsVisited.Add(approxPosition);

        if (Vector3.Distance(transform.position, Finish) < 1f)
        {
            MazeSolved.Raise();
            GenerateRoad();
        }
        else
        {
            MoveToValidPosition();
        }
    }
    private void MoveToValidPosition()
    {
        // Check if there is a wall north, if there isnt then check if it has been visited, move to north
        // Check if there if a wall east, if there isnt then check if it has been visited, move to east
        // check if there is a wall south, if there isnt then check if it has been visited, move to south
        // check if there is a wall west, if there isnt then check if it has been visited, move to west
    }
    public void GenerateRoad()
    {
        // Create tiles and waypoints at each position in path
    }
}
