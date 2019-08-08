using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Unit script is used for the objects we want to seek to target. It calls a path request from "PathRequestManager" class and gives the seekers
    // current location, targets location and a callback. Callback triggers the coroutine that moves the object through the nodes in the grid
    Transform target;
    public Vector3 currentPathStep;
    
    float speed;
    Vector3[] path;
    int targetIndex;
    const float pathUpdateTreshold = 0.5f;
    const float updateTimeMin = .2f;

    UIController ui;
    
    private void Start()
    {

        ui = FindObjectOfType<UIController>();
        target = GameObject.Find("Treasure(Clone)").GetComponent<Transform>();
        PathRequestManager.RequestPath(transform.position, target.position, whenPathFound);
    }

    private void Update()
    {
        speed = ui.GetGeneralSpeed();
    }

    public void whenPathFound(Vector3[] newpath, bool success)
    {
        if (success)
        {
            path = newpath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {

        currentPathStep = path[0];

        while (true)
        {
            if(transform.position == currentPathStep)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    yield break;
                }
                currentPathStep = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentPathStep, speed *Time.deltaTime);  // this moves the unit onwards
            yield return null;
        }

    }
    /*IEnumerator UpdatePath()
    {
        if(Time.timeSinceLevelLoad < .3f)
        {
            yield return new WaitForSeconds(.3f);
        }
        PathRequestManager.RequestPath(transform.position, target.position, whenPathFound);

        float sqrMoveTreshold = pathUpdateTreshold * pathUpdateTreshold;
        Vector3 targetOld = target.position;

        while (true)
        {
            yield return new WaitForSeconds(updateTimeMin);
            if ((target.position - targetOld).sqrMagnitude > sqrMoveTreshold)
            {
                PathRequestManager.RequestPath(transform.position, target.position, whenPathFound);
                targetOld = target.position;
            }
        }
    }*/

    public void OnDrawGizmos()
    {
        if(path != null)
        {
            for(int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if(i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}

