using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using UnityEngine;

public class pathfinding : MonoBehaviour {

    PathRequestManager pathManager;
    Grid grid;
    LineRenderer lineRenderer;
    public GameObject adventurer, treasure;
    public GameObject pathDot;
    float timeSpent;
    private void Awake()
    {
        pathManager = GetComponent<PathRequestManager>();
        grid = GetComponent<Grid>();
        lineRenderer = GetComponent<LineRenderer>();
        //adventurer = FindObjectOfType<GameObject>();            // ADDED
        //treasure = FindObjectOfType<GameObject>();              // ADDED
    }

    private void Update()
    {
        //Node startNode = grid.playerLocation(adventurer.transform.position);         // ADDED
        //Node goalNode = grid.playerLocation(treasure.transform.position);           // ADDED
    }

    public void startCalculatingPath(Vector3 startPosition, Vector3 goalPosition)
    {
        StartCoroutine(findPath(startPosition, goalPosition));
        print("Start position: " + startPosition + " and goal position: " + goalPosition);
    }

    IEnumerator findPath(Vector3 start, Vector3 goal)
    {
        Stopwatch clock = new Stopwatch();
        clock.Start();

        Vector3[] pathSteps = new Vector3[0];
        bool pathsuccess = false;

        //Node startNode = grid.playerLocation(GameObject.Find("Adventurer(Clone)").transform.position);
        //Node goalNode = grid.playerLocation(GameObject.Find("Treasure(Clone)").transform.position);
        Node startNode = grid.playerLocation(start);
        Node goalNode = grid.playerLocation(goal);

        if (startNode.isThisWalkable && goalNode.isThisWalkable){
        List<Node> unExploredNodes = new List<Node>();
        HashSet<Node> exploredNodes = new HashSet<Node>();

        unExploredNodes.Add(startNode);

            while (unExploredNodes.Count > 0 && !exploredNodes.Contains(goalNode))
            {
                Node currentNode = unExploredNodes[0]; //Hero's location on the grid
                for (int i = 1; i < unExploredNodes.Count; i++)
                {
                    if (unExploredNodes[i].estimatedCost() < currentNode.estimatedCost() || unExploredNodes[i].estimatedCost() == currentNode.estimatedCost() && unExploredNodes[i].distanceCost < currentNode.distanceCost)
                    {
                        currentNode = unExploredNodes[i];
                    }
                }
                unExploredNodes.Remove(currentNode);
                exploredNodes.Add(currentNode);
                //Debug.Log("added current node to explored list");

                if (currentNode == goalNode)  // goal found
                {
                    clock.Stop();
                    timeSpent = clock.ElapsedMilliseconds;
                    print("Time spent on finding path: " + clock.ElapsedMilliseconds + " ms");
                    pathsuccess = true;
                    break;
                }
                foreach (Node neighbour in grid.getNeighbours(currentNode))
                {
                    if (!neighbour.isThisWalkable || exploredNodes.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.distanceCost + calculateDistance(currentNode, neighbour) + neighbour.difficulty;
                    if (newMovementCostToNeighbour < neighbour.distanceCost || !unExploredNodes.Contains(neighbour))
                    {
                        neighbour.distanceCost = newMovementCostToNeighbour;
                        neighbour.heuristicCost = calculateDistance(neighbour, goalNode);
                        neighbour.parent = currentNode;

                        if (!unExploredNodes.Contains(neighbour))
                        {
                            unExploredNodes.Add(neighbour);
                        }
                       
                    }
                }
            }
            unExploredNodes.Clear();
            exploredNodes.Clear();
        }
        yield return null;
        if (pathsuccess)
        {
            pathSteps = BackTrackPath(startNode, goalNode);
        }
        pathManager.FinishedProcessingPath(pathSteps, pathsuccess);

    }

    Vector3[] BackTrackPath(Node startingNode, Node endingNode)  // track the path from starting node to ending node
    {
        List<Node> path = new List<Node>();             // new list for the path
        Node currentNode = endingNode;
        while (currentNode != startingNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] pathSteps = SimplifyPath(path);
        Array.Reverse(pathSteps);
        return pathSteps;
    }

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> pathSteps = new List<Vector3>();
        Vector2 oldDirection = Vector2.zero;
        pathSteps.Add(path[0].thisLocation);  // Optim. 
        for (int i = 1; i < path.Count; i++){
           
            Vector2 newDirection = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (newDirection != oldDirection)
            {
                pathSteps.Add(path[i].thisLocation);
                Instantiate(pathDot, path[i].thisLocation, Quaternion.identity);
            }
            oldDirection = newDirection;
        }
        //pathSteps.Add(path[path.Count - 1].thisLocation);  // we add locations to the PathSteps array
        GameObject[] pathDots = GameObject.FindGameObjectsWithTag("RedDot");
        DrawLines(pathDots);
        return pathSteps.ToArray();
    }

    int calculateDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX); // derivate
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if(distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
    void DrawLines(GameObject[] dots)
    {
       
        for(int i = 0; i < dots.Length; i++)
        {
             
            //Instantiate(pathDot, dots[i].transform.position, Quaternion.identity);
            /*Vector3 A = dots[i].transform.position;
            Vector3 B = dots[i+1].transform.position;

            Vector3 AtoB = Vector3.Normalize(B - A) + A;
            // DRAW LINE BETWEEN DOTS
            
            //lineRenderer.SetWidth(.5f, 0.5f);
            //lineRenderer.SetColors(Color.red, Color.black);
            lineRenderer.SetPosition(0, AtoB);
            */
            //line = dots[i].transform.position
            //Gizmos.DrawLine(dots[i].transform.position, dots[i + 1].transform.position);
        }
    }
    public float returnTimeSpent()
    {
        return timeSpent;
    }
}
