using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtCamera : MonoBehaviour {

    public Transform cameraToLookAt;
    float rotation = 3.15f;
	void Start () {
        //camera = GameObject.Find("Main Camera").GetComponent<Transform>();
        cameraToLookAt = GameObject.Find("Main Camera").GetComponent<Transform>();
        //transform.Rotate(Vector3();
        

    }

    // Update is called once per frame
    void Update() {
        //Vector3 direction = cameraToLookAt.transform.position - transform.position;
        //Vector3 targetDir = Camera.main.transform.position - transform.position;
        //Vector3 dir = Vector3.RotateTowards(transform.forward, targetDir, 10 * Time.deltaTime, 0.0f);
        //transform.rotation = Quaternion.LookRotation(dir);
        //transform.RotateAround(Vector3.forward, (gameObject.transform.position - Camera.main.transform.position));
        transform.LookAt(cameraToLookAt.transform.position);
        transform.RotateAroundLocal(Vector3.up, rotation);
        //transform.LookAt(cameraToLookAt.position);
        //transform.rotation.SetLookRotation(Camera.main.transform.position);

    }
}
