using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeNode : MonoBehaviour
{
    public VectorVariable curPositionOfActiveNode;
    public GameObject previousNode;
    public bool thisNodeIsCurrentNode = false;
    public bool startingNode;
    public bool endingNode;

    public GameObject posXWall;
    public GameObject negXWall;
    public GameObject posZWall;
    public GameObject negZWall;
    public GameObject unVisitedSphere;
    public GameObject visitedSphere;
    public GameObject marker;

    private bool checkedn = false;
    private bool checkede = false;
    private bool checkeds = false;
    private bool checkedw = false;

    public GameEvent MazeGenComplete;
    public FloatReference timeFactor;

    // Start is called before the first frame update
    void Awake()
    {
        visitedSphere.SetActive(false);
        marker.SetActive(false);
    }
    public void BeginMazeCreation()
    {
        if (startingNode)
        {
            ActivateThisNode();
        }
    }

    public void EvaluateNeighbor()
    {
        if (thisNodeIsCurrentNode)
        {
            if (checkedn && checkede && checkeds && checkedw)
            {
                thisNodeIsCurrentNode = false;
                marker.SetActive(false);
                if (previousNode == null)
                {
                    MazeGenComplete.Raise();
                }
                else
                    previousNode.GetComponent<MazeNode>().ActivateThisNode();
            }
            else
            {
                Vector3 direction = GetUncheckedDirection();
                RaycastHit hit;
                LayerMask lm = ~0;
                if (Physics.Raycast(transform.position + new Vector3(0, 2.75f, 0), direction, out hit, 6f, lm))
                {
                    if (hit.collider.gameObject.name == "UnVisited")
                    {
                        MazeNode mn = hit.collider.gameObject.GetComponentInParent<MazeNode>();
                        if (direction.x > 0)
                        {
                            Destroy(mn.negXWall);
                            Destroy(posXWall);
                        }
                        else if (direction.x < 0)
                        {
                            Destroy(mn.posXWall);
                            Destroy(negXWall);
                        }
                        else if (direction.z > 0)
                        {
                            Destroy(mn.negZWall);
                            Destroy(posZWall);
                        }
                        else if (direction.z < 0)
                        {
                            Destroy(mn.posZWall);
                            Destroy(negZWall);
                        }
                        thisNodeIsCurrentNode = false;
                        mn.previousNode = gameObject;
                        mn.ActivateThisNode();
                    }
                }
               // else
               //     Debug.Log("Didnt Hit Anything");
                Invoke("EvaluateNeighbor", .05f / timeFactor.Value);
            }
        }
        else
            marker.SetActive(false);
    }
    public void ActivateThisNode()
    {
        unVisitedSphere.SetActive(false);
        visitedSphere.SetActive(true);
        thisNodeIsCurrentNode = true;
        marker.SetActive(true);
        curPositionOfActiveNode.SetValue(transform.position);
        EvaluateNeighbor();
    }
    private Vector3 GetUncheckedDirection()
    {
        Vector3 validVector = Vector3.zero;
        while (validVector == Vector3.zero)
        {
            int val = Random.Range(0, 4); // Range int is exlusive of max, this returns 0-3
            if (val == 0 && !checkedn)
            {
                checkedn = true;
                validVector = Vector3.forward;
            }
            if (val == 1 && !checkede)
            {
                checkede = true;
                validVector = Vector3.right;
            }
            if (val == 2 && !checkeds)
            {
                checkeds = true;
                validVector = Vector3.forward * -1;
            }
            if (val == 3 && !checkedw)
            {
                checkedw = true;
                validVector = Vector3.right * -1;
            }
        }
        return validVector;
    }
}
