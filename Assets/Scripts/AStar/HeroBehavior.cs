using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBehavior : MonoBehaviour {
    
    Grid grid;
    public bool releaseHero;
    public List<Node> path;

    void Start () {
        releaseHero = false;
        grid = GetComponent<Grid>();
	}
	
	// Update is called once per frame
	void Update () {
        if (releaseHero)
        {
            moveHero();
        }
	}
    
    void moveHero()
    {
            
    }
}
