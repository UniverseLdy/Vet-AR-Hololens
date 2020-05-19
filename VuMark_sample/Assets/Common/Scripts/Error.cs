using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class Error : MonoBehaviour
{
    private Text error;
    public bool is_recording;
    public GameObject target1;
    public GameObject target2;
    public Vector3 errorvector;
    private double time_count;
    private int count;
    string path;
    string file_name;
    // Start is called before the first frame update
    void Start()
    {
        error = this.GetComponent<Text>();
        file_name = "Error";
        path = @"D:\Dayu Li" + "/" + file_name + ".txt";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        var file = File.Create(path);
        time_count = 0;
        count = 0;
        is_recording = false;
    }

    // Update is called once per frame
    void Update()
    {
        errorvector = new Vector3(target1.transform.rotation.eulerAngles.x - target2.transform.rotation.eulerAngles.x, target1.transform.rotation.eulerAngles.y - target2.transform.rotation.eulerAngles.y, target1.transform.rotation.eulerAngles.z - target2.transform.rotation.eulerAngles.z);
        if (errorvector.x > 180) errorvector.x = 360 - errorvector.x;
        if (errorvector.y > 180) errorvector.y = 360 - errorvector.y;
        if (errorvector.z > 180) errorvector.z = 360 - errorvector.z;
        if (errorvector.x < -180) errorvector.x = -360 - errorvector.x;
        if (errorvector.y < -180) errorvector.y = -360 - errorvector.y;
        if (errorvector.z < -180) errorvector.z = -360 - errorvector.z;
        error.text = "Error:\n" + errorvector.ToString();
        //Errors_x = errorvector.x;
        //Errors_y = errorvector.y;
        //Errors_z = errorvector.z;
        if (is_recording)
        {
            time_count += Time.deltaTime;
            if(time_count >= 0.25)
            {
                count += 1;
                StreamWriter writer = new StreamWriter(path, true);
                writer.WriteLine(errorvector.ToString()+" @ " + count*0.25);
                writer.Close();
                time_count = 0;
            }
        }
    }
    public void Record_Error()
    {
        is_recording = !is_recording;
        Debug.Log("Is_Recording:" + is_recording);
        time_count = 0;
        count = 0;
        if (is_recording)
        {
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine("--------------------New Data Collected at" + Time.time + " since start up-----------------------------------");
            writer.Close();
        }
    }
}
