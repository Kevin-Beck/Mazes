using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToEnd : MonoBehaviour
{
    public FloatReference mazeSize;
    Vector3 start;
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
        transform.position = new Vector3((mazeSize.Value) * 4, start.y, (mazeSize.Value) * 4);
        
    }
}
