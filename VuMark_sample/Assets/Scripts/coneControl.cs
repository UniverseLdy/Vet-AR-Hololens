using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HoloToolkit.Unity.InputModule;

public class coneControl : MonoBehaviour
{
    bool flag;
    bool touch_bound = false;
    public GameObject drill;
    private float drill_angle_x;
    private float drill_angle_z;
    int counter = 0;
    //public bool find_drill = false;
    private bool safe = true;
    //x and z threshold angle
    private float x_thre = 0.0f;
    private float z_thre = 0.0f;
    private float standard = 14.036f;
    // Use this for initialization
    void Start()
    {
        //find drill in its child
        flag = false;
        if(gameObject.transform.parent.name == "Cone1")
            drill = GameObject.Find("drill");
        //we need to calculate the threashold of angle for z and x axis
        //the 1:1:1 scale cone has half angle 0.245 rad = 14.036f degree, we use that as a standard
        x_thre = (gameObject.transform.localScale.x / gameObject.transform.localScale.y) * standard;
        z_thre = (gameObject.transform.localScale.z / gameObject.transform.localScale.y) * standard;
        //Debug.Log(gameObject.name);
        /*
        for (int index = 0; index < gameObject.transform.childCount; index++)
        {

            if (gameObject.transform.GetChild(index).gameObject.transform.name == "drill")
            {
                drill = gameObject.transform.GetChild(index).gameObject;
            }
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (drill != null) {
            drill_angle_x = drill.transform.localEulerAngles.x;
            drill_angle_z = drill.transform.localEulerAngles.z;
            var audio = gameObject.GetComponent<AudioSource>();
            if (counter % 100 == 0)
                Debug.Log("angle x is " + drill_angle_x + " angle z is " + drill_angle_z);

            if ((Mathf.Abs(drill_angle_x) < x_thre || Mathf.Abs(drill_angle_x - 360) < x_thre) && (Mathf.Abs(drill_angle_z) < z_thre || Mathf.Abs(drill_angle_z - 360) < z_thre)) {
                gameObject.GetComponent<Renderer>().material.color = new Color(Color.green.r,Color.green.g,Color.green.b,0.3f);
                audio.enabled = false;
                //safe = true;
            }
            else if ((Mathf.Abs(drill_angle_x) > x_thre || (Mathf.Abs(drill_angle_z) > z_thre)))
            {
                gameObject.GetComponent<Renderer>().material.color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.3f);
                audio.enabled = true;
                //safe = false;
            }
            counter++;
        }
    }
    /*
    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("cone has name: " + gameObject.name);
       // gameObject.SetActive(false);
       if(flag)
            gameObject.GetComponent<AudioSource>().enabled = false;
       else
            gameObject.GetComponent<AudioSource>().enabled = true;
        flag = !flag;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trig the collider");
        //change material of cone
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit the collider");
        //change material of cone
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }
    */
}