using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PathRequestManager : MonoBehaviour {

    Queue<Pathrequest> pathRequestQueue = new Queue<Pathrequest>();
    Pathrequest currentPathRequest;
    pathfinding pathfinding;

    bool processingPath;

    static PathRequestManager instance; 

    void Awake()
    {
        instance = this;   //not sure if neccessary
        pathfinding = GetComponent<pathfinding>();
    }

    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        Pathrequest newRequest = new Pathrequest(pathStart, pathEnd, callback);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.tryNextProcess();
    }

    void tryNextProcess()
    {
        if(!processingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            processingPath = true;
            pathfinding.startCalculatingPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);     // maybe this needs new locations
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        currentPathRequest.callback(path, success);
        processingPath = false;
        tryNextProcess();
    }

    struct Pathrequest
    {
        public Vector3 pathStart, pathEnd;
        public Action<Vector3[], bool> callback;

        public Pathrequest(Vector3 start, Vector3 end, Action<Vector3[], bool> _callback)
        {
            pathStart = start;
            pathEnd = end;
            callback = _callback;
        }
    }

}
