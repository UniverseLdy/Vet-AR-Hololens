using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

using LpmsCSharpWrapper;

public class LpmsTest : MonoBehaviour {

    public Button Cali;
    public Button objectResetButton;
    string lpmsSensor = "00:04:3E:9B:A3:7C";
    Quaternion offset;
    Vector3 offsetEuler;
    // Use this for initialization
    void Start()
    {
        offset = new Quaternion(0, 0, 0, 0);
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
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Doing The Calibration");
            offset = transform.rotation;
            offsetEuler = offset.eulerAngles;
            Debug.Log("OFFSET: X:" + offsetEuler.x + "Y:" + offsetEuler.z + "Z:" + offsetEuler.y);
        }
        if(LpSensorManager.getConnectionStatus(lpmsSensor) == LpSensorManager.SENSOR_CONNECTION_CONNECTED)
        {
            SensorData sd;
            unsafe
            {
                sd = *((SensorData*)LpSensorManager.getSensorData(lpmsSensor));
            }
            //Debug.Log("Timestamp: " + sd.timeStamp + " q: " + sd.qw + " " + sd.qx + " " + sd.qy + " " + sd.qz);
            //Quaternion q = new Quaternion(sd.qx - offset.x, sd.qz - offset.y, sd.qy - offset.z, sd.qw - offset.w);
            Quaternion q = new Quaternion(sd.qx, sd.qz, sd.qy, sd.qw);
        }
        
    }

    void OnDestroy()
    {
        Debug.Log("PrintOnDestroy");
        LpSensorManager.disconnectLpms(lpmsSensor);
        // Destroy sensor manager and free up memory
        LpSensorManager.deinitSensorManager();
    }
}
