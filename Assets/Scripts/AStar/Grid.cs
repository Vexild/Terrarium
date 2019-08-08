using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    public GameObject treasurePrefab;
    public Vector2 gridSize;
    public float nodeRadius;
    public LayerMask unwalkableMask;
    public Node[,] grid;
    public TerrainType[] walkableTerrain;
    LayerMask walkableMask;

    Dictionary<int, int> walkableTerrainDictionary = new Dictionary<int, int>();

    bool showGizmosOnGrid;
    int gridSizeX, gridSizeY;
    float nodeDiameter;
    Vector3 treasureCurrentLocation;


    private void Start()
    {
        //adventurer = GameObject.Find("Hero").GetComponent<GameObject>();
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);

        foreach(TerrainType region in walkableTerrain)
        {
            walkableMask.value |= region.terrainMask.value;
            walkableTerrainDictionary.Add((int)Mathf.Log(region.terrainMask.value, 2), region.terrainDifficulty);
        }

        CreateGrid();

    }
    private void FixedUpdate()
    {
        //playerLocation(adventurer.transform.position);
    }

    public void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridSize.x / 2 - Vector3.forward * gridSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                int difficulty = 0;
                if (walkable)
                {                           // we need a raycast to identify terrains difficulty, aka weights
                    Ray ray = new Ray(worldPoint + Vector3.up * 50, Vector3.down);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, walkableMask))
                    {
                        walkableTerrainDictionary.TryGetValue(hit.collider.gameObject.layer, out difficulty);
                    }; 
                }
                grid[x, y] = new Node(walkable, worldPoint,x,y, difficulty);
            }
        }
    }
    // player location in grid
    public Node playerLocation(Vector3 location)  // THIS IS ALSO USED FOR THE GOAL NODE
    {
        int x = Mathf.RoundToInt((location.x + gridSize.x / 2 - nodeRadius) / nodeDiameter);  // NOTE: instead of y axis, we need to use the Z axis since it acts
        int y = Mathf.RoundToInt((location.z + gridSize.y / 2 - nodeRadius) / nodeDiameter);  // as the Y axis in unity 3D space

        x = Mathf.Clamp(x, 0, gridSizeX - 1);
        y = Mathf.Clamp(y, 0, gridSizeY - 1);

        return grid[x, y];
    }

    //neighbour node scanning
    public List<Node> getNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for(int x = -1; x <=1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if(x == 0 && y == 0)
                {
                    continue;
                }
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }
    


    [System.Serializable]
    public class TerrainType
    {
        public LayerMask terrainMask;
        public int terrainDifficulty;
    }

   public void SpawnTreasure()
    {
        //Vector3 TreasurePosition = new Vector3(Random.Range(gridSizeX, (gridSizeX - gridSizeX)), 0, Random.Range(gridSizeY, (gridSizeY - gridSizeY)));
        int x = Random.Range(0, gridSizeX);
        int y = Random.Range(0, gridSizeY);
        Node TreasureNode = grid[x,y];
        Debug.Log("Treasure location: " + TreasureNode.thisLocation);
        if (!TreasureNode.isThisWalkable || TreasureNode.thisLocation == GameObject.Find("StartingLocation").transform.position)
        {
            Debug.Log("Node: "+ TreasureNode.thisLocation + "  is Unwalkable. Creating new node.");
            SpawnTreasure();
        }
        else
        {
            //Debug.Log("Unwalkable");
            Instantiate(treasurePrefab, TreasureNode.thisLocation, Quaternion.identity);
            treasureCurrentLocation = TreasureNode.thisLocation;
            Debug.Log("Node saved: " + treasureCurrentLocation);
        }
    }

    public void ResetTreasure(Vector3 location)
    {
            Instantiate(treasurePrefab, location, Quaternion.identity); 
    }

    public Vector3 returnTreasureLocation()
    {
        return treasureCurrentLocation;
    }


    void OnDrawGizmos()
    {
        if (grid != null)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.isThisWalkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.thisLocation, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}
