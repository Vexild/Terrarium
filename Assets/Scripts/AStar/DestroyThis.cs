using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThis : MonoBehaviour {
    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Adventurer")
        {
            Destroy(this);
            Debug.Log("asdasd");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Adventurer")
        {
            Destroy(this);
            Debug.Log("asdasd");
        }
    }
}
