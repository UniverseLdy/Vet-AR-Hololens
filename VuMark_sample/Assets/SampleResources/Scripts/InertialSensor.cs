using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

using LpmsCSharpWrapper;
public class InertialSensor : MonoBehaviour
{
    Vector3 error;
    Vector3 last_sensor;
    Vector3 current_sensor;
    string lpmsSensor = "00:04:3E:9B:A3:7C";
    public GameObject datamanager;
    private Vector3 default_rot;
    private bool first_time;
    public GameObject created;
    public GameObject cube;
    // Start is called before the first frame update
    void Start()

    {
        //default_rot = new Vector3((float)0.01, (float)0.01, (float)0.01);
        //transform.rotation = Quaternion.Euler(default_rot);
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
        if (LpSensorManager.getConnectionStatus(lpmsSensor) == 1)
        {
            Debug.Log("Sensor connected");
        }

        // Sets sensor offset
        LpSensorManager.setOrientationOffset(lpmsSensor, LpSensorManager.LPMS_OFFSET_MODE_HEADING);
        first_time = true;
        last_sensor = new Vector3(0, 0, 0);
        current_sensor = last_sensor;
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
            this.transform.rotation = new Quaternion(sd.qx, sd.qz, sd.qy, sd.qw);
            if (first_time)
            {
                created = (GameObject)Instantiate(cube);
                created.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                //created.GetComponent<MeshRenderer>().enabled = false;
                created.transform.parent = this.transform;
                last_sensor = created.transform.rotation.eulerAngles;
                first_time = !first_time;
            }
            //Debug.Log("Timestamp: " + sd.timeStamp + " q: " + sd.qw + " " + sd.qx + " " + sd.qy + " " + sd.qz);
            //Quaternion q = new Quaternion(sd.qx - offset.x, sd.qz - offset.y, sd.qy - offset.z, sd.qw - offset.w);
            current_sensor = created.transform.rotation.eulerAngles;
            error = current_sensor - last_sensor;
            datamanager.GetComponent<DataManager>().set_Iniertial(this.transform.localRotation);
            last_sensor = current_sensor;
        }
    }
    public void Reset_Created()
    {
        created.transform.parent = null;
        created.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        created.transform.parent = this.transform;
        Reset_Initial();
    }
    public void Reset_Initial()
    {
        //LpSensorManager.resetOrientationOffset(lpmsSensor);
    }
}
