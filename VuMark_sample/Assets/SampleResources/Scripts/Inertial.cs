using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

using LpmsCSharpWrapper;

public class Inertial : MonoBehaviour
{
    public bool tracking;
    Vector3 offset;
    Vector3 last_sensor;
    Vector3 current_sensor;
    string lpmsSensor = "00:04:3E:9B:A3:7C";
    public GameObject virtualcube;
    // Start is called before the first frame update
    void Start()

    {
        Application.targetFrameRate = 20;
        // Initialize sensor manager
        LpSensorManager.initSensorManager();

        // connects to sensor
        LpSensorManager.connectToLpms(LpSensorManager.DEVICE_LPMS_B2, lpmsSensor);

        // Wait for establishment of sensor1 connection
        while (LpSensorManager.getConnectionStatus(lpmsSensor) != 1)
        {
            //System.Threading.Thread.Sleep(100);
        }
        Debug.Log("Sensor connected");

        // Sets sensor offset
        LpSensorManager.setOrientationOffset(lpmsSensor, LpSensorManager.LPMS_OFFSET_MODE_HEADING);
        Debug.Log("Offset set");
        tracking = false;
        if (LpSensorManager.getConnectionStatus(lpmsSensor) == LpSensorManager.SENSOR_CONNECTION_CONNECTED)
        {
            SensorData sd;
            unsafe
            {
                sd = *((SensorData*)LpSensorManager.getSensorData(lpmsSensor));
            }
            last_sensor = new Vector3(0, 0, 0);
            current_sensor = last_sensor;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (LpSensorManager.getConnectionStatus(lpmsSensor) == LpSensorManager.SENSOR_CONNECTION_CONNECTED)
        {
            SensorData sd;
            Vector3 current_rot = transform.rotation.eulerAngles;
            unsafe
            {
                sd = *((SensorData*)LpSensorManager.getSensorData(lpmsSensor));
            }
            if (Input.GetButtonDown("Fire1"))
            {
                tracking = !tracking;
                Debug.Log("Tracking:" + tracking);
                last_sensor = new Vector3(-sd.ry, -sd.rz, sd.rx);
                //transform.rotation = virtualcube.transform.rotation;
            }
            //Debug.Log("Timestamp: " + sd.timeStamp + " q: " + sd.qw + " " + sd.qx + " " + sd.qy + " " + sd.qz);
            //Quaternion q = new Quaternion(sd.qx - offset.x, sd.qz - offset.y, sd.qy - offset.z, sd.qw - offset.w);
            if (tracking)
            {
                current_sensor = new Vector3(-sd.ry, -sd.rz, sd.rx);
                //Debug.Log("OFFSET:" + offset);
            }
            offset = current_sensor - last_sensor;
            transform.rotation = Quaternion.Euler(current_rot + offset);
            last_sensor = current_sensor;
            if (Input.GetKeyDown("space"))
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
                //transform.rotation = virtualcube.transform.rotation;
            }
        }
    }
}
