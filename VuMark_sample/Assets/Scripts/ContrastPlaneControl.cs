using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HoloToolkit.Unity.InputModule;
public class ContrastPlaneControl : MonoBehaviour, IInputClickHandler{
    // Use this for initialization
    bool status = true;
    public GameObject cursor;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void OnInputClicked(InputClickedEventData eventData)
    {
        //Debug.Log("contrast plane has name: " + gameObject.name);
        gameObject.SetActive(false);
        cursor = GameObject.Find("BasicCursor");
        Debug.Log("The position of contrast plane is ( " + gameObject.transform.position.x + ", " + gameObject.transform.position.y + ", " + gameObject.transform.position.z + ")");
        Debug.Log("The position of the cursor is ( " + cursor.transform.position.x + ", " + cursor.transform.position.y + ", " + cursor.transform.position.z + ")");
    }

    //for using in UI
    public void ChangeStatus() {
        status = !status;
        gameObject.SetActive(status);
    }
}

