using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuCubeScript : MonoBehaviour {
    float parameter1;
    float speed;
    bool direction;
    Color color1, color2;
    private void Start()
    {
        direction = true;
        speed = 0.1f;

    }
    // Update is called once per frame
    void Update () {
        transform.Rotate(0, 3, 0 * Time.deltaTime);
        //color1 = new Color(Random.RandomRange(0f,1f), Random.RandomRange(0f, 1f), Random.RandomRange(0f, 1f));
        //color2 = new Color(Random.RandomRange(0f, 1f), Random.RandomRange(0f, 1f), Random.RandomRange(0f, 1f));
        color1 = new Color(0, 0.3f, 0);
        color2 = new Color(0.5f, 1, 1);


        //parameter3 = Random.Range(0.0f, 1f) * Time.deltaTime;
        /*  parameter4 = Random.Range(0.0f, 1f) * Time.deltaTime;
          parameter5 = Random.Range(0.0f, 1f) * Time.deltaTime;
          parameter6 = Random.Range(0.0f, 1f) * Time.deltaTime; */
        //GetComponent<Renderer>().material.color = Random.ColorHSV(0, 1f, 0, 1f, 0, 1f);
        //GetComponent<Renderer>().material.color = Color.HSVToRGB(parameter1, 1, 1);
        GetComponent<Renderer>().material.color = Color.Lerp(color1, color2, Mathf.PingPong(Time.time, 1));
        //GetComponent<Renderer>().material.color = Color.Lerp(Random.ColorHSV(0, 0.5f, 0, 0.5f, 0, 0.5f), Random.ColorHSV(1f, 1f, 1f, 1f, 1f, 1f), Mathf.PingPong(Time.time, 4));
        //Debug.Log(parameter1 + " " + parameter2 + " " + parameter3 + " " + parameter4 + " " + parameter5 + " " + parameter6);
        Debug.Log(parameter1);
    }

}
