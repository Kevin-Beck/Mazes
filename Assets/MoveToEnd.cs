using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToEnd : MonoBehaviour
{
    public FloatReference mazeSize;
    Vector3 start;
    public bool isSecondFinish;
    public bool isSecondStart;
    private void Start()
    {
        start = transform.position;
    }
    public void Reset()
    {
        transform.position = start;
    }
    public void Move()
    {
        if (isSecondFinish)
            transform.position = new Vector3(0, start.y, (mazeSize.Value) * 4);
        else
            transform.position = new Vector3((mazeSize.Value) * 4, start.y, (mazeSize.Value) * 4);
        if (isSecondStart)
            transform.position = new Vector3((mazeSize.Value) * 4, start.y, 0);
    }
}
