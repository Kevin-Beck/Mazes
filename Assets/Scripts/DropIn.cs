using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropIn : MonoBehaviour
{
    public float endY;
    bool madeIt;
    Vector3 myTargetPosition;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        myTargetPosition = new Vector3(transform.position.x, endY, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(!madeIt)
        {
            transform.position = Vector3.Lerp(transform.position, myTargetPosition, speed * Time.deltaTime);
            if (transform.position.y < endY + 0.01f)
                madeIt = true;
        }
    }
}
