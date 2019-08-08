using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameObject adventurer;
    public GameObject startinglocation;
    public GameObject map1, map2;
    public Text timeSpent;
    public Text speed;
    Grid grid;
    Unit unit;
    pathfinding pathFind;
    
    public Slider speedSlider;
    float unitGeneralSpeed;
    int mapIndex;
    float timeOnFinding;

	// Use this for initialization
	void Start () {
        grid = FindObjectOfType<Grid>();
        unit = FindObjectOfType<Unit>();
        pathFind = FindObjectOfType<pathfinding>();
        map1.SetActive(true);
        //map2.SetActive(false);
        mapIndex = 1;
        grid.CreateGrid();
    }
	
	// Update is called once per frame
	void Update () {
        SliderForSpeed();
        if(mapIndex == 1)
        {
            map1.SetActive(true);
            map2.SetActive(false);
        }
        if(mapIndex == 2)
        {
            map1.SetActive(false);
            map2.SetActive(true);
        }
    }
    void startPathfinding()
    {
        Destroy(GameObject.Find("Adventurer(Clone)"));
        Destroy(GameObject.Find("Treasure(Clone)"));
        GameObject[] dots = GameObject.FindGameObjectsWithTag("RedDot");
        for (int i = 0; i < dots.Length; i++)
        {
            Destroy(dots[i]);
        }
        grid.SpawnTreasure();
        StartCoroutine(spawnAdventurer());
        StartCoroutine(getTimeSpent());
    }

    IEnumerator spawnAdventurer()
    {
        yield return new WaitForSeconds(.1f);
        Instantiate(adventurer, startinglocation.transform.position, Quaternion.identity);
    }
    IEnumerator newGrid()
    {
        yield return new WaitForSeconds(.1f);
        grid.CreateGrid();
    }
    IEnumerator getTimeSpent()
    {
        timeSpent.text = "Waiting..";
        yield return new WaitForSeconds(.3f);
        timeSpent.text = pathFind.returnTimeSpent().ToString() + " ms";
    }
    
    public void SliderForSpeed()
    {
        unitGeneralSpeed = speedSlider.value;
        speed.text = speedSlider.value.ToString();
    }
    public float GetGeneralSpeed()
    {
        return unitGeneralSpeed;
    }
    public void chanceMap()
    {
        if(mapIndex == 1)
        {
            mapIndex++;
        }
        else
        {
            mapIndex = 1;
        }
        StartCoroutine(newGrid());
        Debug.Log("New grid created");
    }
    public void RewalkThePath()
    {
        Vector3 resetLocation = grid.returnTreasureLocation();  // Difference is that we save the previous location, reset it and spawn it there again

        Destroy(GameObject.Find("Adventurer(Clone)"));
        Destroy(GameObject.Find("Treasure(Clone)"));
        GameObject[] dots = GameObject.FindGameObjectsWithTag("RedDot");
        for (int i = 0; i < dots.Length; i++)
        {
            Destroy(dots[i]);
        }
        grid.ResetTreasure(resetLocation);
        StartCoroutine(spawnAdventurer());
        StartCoroutine(getTimeSpent());
    }

}

