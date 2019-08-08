using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    Transform transform;
    float speed = 30f;
    private bool m_cursorIsLocked = true;
    //public bool lockCursor = true;

    private void Start()
    {
        transform = GetComponent<Transform>();
        //SetCursorLock(true);

    }


    // Update is called once per frame
    void Update () {
        if (Input.GetAxis("Vertical") > 0)
        {
            //transform.localPosition += new Vector3.forward);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if(Input.GetAxis("Vertical") < 0)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");
        //transform.localPosition+= new Vector3(x, 0f, z);

        float mx = Input.mousePosition.x / speed;
        float my = Input.mousePosition.y / speed;
        float mz = Input.mousePosition.z / speed;
        transform.localEulerAngles = new Vector3(-my, mx, mz);
        //UpdateCursorLock();

        if (Input.GetKeyDown(KeyCode.C))
        {
            Cursor.visible = !Cursor.visible;
        }

    }

    

}
