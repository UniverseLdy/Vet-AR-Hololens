using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera ARCamera;
    // Start is called before the first frame update
    void Start()
    {
        ARCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("tab"))
        {
            ARCamera.enabled = !ARCamera.enabled;
        }
    }
}
