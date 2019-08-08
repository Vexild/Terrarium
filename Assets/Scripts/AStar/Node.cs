using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

    public bool isThisWalkable;
    public Vector3 thisLocation;

    public int heuristicCost; // heuristic in this case means distance to the goal
    public int distanceCost; // with distanceCost we mean distance to next node
    public int gridX;
    public int gridY;
    public int difficulty;
    public Node parent;

    public Node(bool walkable, Vector3 location, int coordinateX, int coordinateY, int penality)
    {
        isThisWalkable = walkable;
        thisLocation = location;
        gridX = coordinateX;
        gridY = coordinateY;
        difficulty = penality;
    }
    public int estimatedCost()
    {   // estimated cost determines the full cost of the certain path
   
            return heuristicCost + distanceCost;  
        
    }
}


