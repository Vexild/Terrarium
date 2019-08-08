using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_chaseTarget : MonoBehaviour {

    CharacterManager target;
    Vector2 velocity;
    private float directionToTarget;
    private float speed = 1.0f;
    private float startTime;
    private float journeyLength;
    private float dist = 40f;
    private float distance;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        target = GameObject.Find("TargetDummy").GetComponent<CharacterManager>();
	}
	
	// Update is called once per frame
	void Update () {
        directionToTarget = Vector2.Angle(transform.position, target.transform.position);
        distance = Vector3.Distance(transform.position, target.transform.position);
        
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;


        CharacterManager closest = null;

        //Debug.Log("Distance between Tracer and Target: " + distance);

        if (closest == null || distance < dist)
        {
            closest = target;
            Debug.Log("Closest unit is: " + closest.ToString());
            dist = distance;
            //velocity = Vector3.Lerp(transform.position, target.transform.position, fracJourney);

        }
        if (closest == null)
        {
            return;
            Debug.Log("No close targets");
        }
        // when the target is in chase range
        // chase it
        //float directionToTarget = Vector2.Angle(transform.position, target.transform.position);
        //this.transform.position = Vector3.Lerp(transform.position, target.transform.position, fracJourney);

    }


       

        

    
}
