using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

    int hp = 100;
    
    // Use this for initialization
    void Start () { 
	}
	
	// Update is called once per frame
	void Update () {
		if(hp < 0)
        {
            GameObject.Destroy(this);
        }
        if(hp > 0)
        {
            return;
        }
	}
}
