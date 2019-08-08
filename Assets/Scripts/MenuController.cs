using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public GameObject CubePrefab;
    public Node[,] grid;
    public Vector2 gridSize;
    public float nodeRadius;
    int gridSizeX, gridSizeY;
    float nodeDiameter;

    // Use this for initialization
    void Start () {
        //adventurer = GameObject.Find("Hero").GetComponent<GameObject>();
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);

        CreateGrid();
    }
	
	// Update is called once per frame
	void Update () {
		
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
                bool walkable = true;
                int difficulty = 0;
                grid[x, y] = new Node(walkable, worldPoint, x, y, difficulty);
                Instantiate(CubePrefab, grid[x, y].thisLocation, Quaternion.identity);
            }
        }
    }
}
