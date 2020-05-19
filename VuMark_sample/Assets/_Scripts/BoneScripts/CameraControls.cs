using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// code from https://forum.unity.com/threads/maya-like-camera.41860/

public class CameraControls : MonoBehaviour {

    public float panSpeed;
    public float zoomSpeed;
    public float xSpeed;
    public float ySpeed;

    private Vector3 targetPos;

    // Use this for initialization
    void Start () {
        targetPos = GameObject.Find("Bone").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        
        //Orbit = ALT + LEFT MOUSE BUTTON
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0)) {
            transform.RotateAround(targetPos, transform.right, Input.GetAxis("Mouse Y") * -ySpeed * .02f);
            transform.RotateAround(targetPos, transform.up, Input.GetAxis("Mouse X") * xSpeed * .02f);
        }
        
        //Pan = ALT + RIGHT MOUSE BUTTON
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(1)) {
            Camera.main.transform.Translate(transform.right * -Input.GetAxis("Mouse X") * panSpeed, Space.World);
            Camera.main.transform.Translate(transform.up * -Input.GetAxis("Mouse Y") * panSpeed, Space.World);
        }
       
        //Zoom = ALT + MIDDLE MOUSE BUTTON
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(2)) {
            Camera.main.transform.Translate(transform.forward * -Input.GetAxis("Mouse X") * zoomSpeed, Space.World);
            transform.Translate(transform.forward * -Input.GetAxis("Mouse X") * zoomSpeed, Space.World);
        }
    }
}
