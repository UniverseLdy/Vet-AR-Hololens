using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    [UPyPlot.UPyPlotController.UPyProbe]
    private float Data_x;
    [UPyPlot.UPyPlotController.UPyProbe]
    private float Data_y;
    [UPyPlot.UPyPlotController.UPyProbe]
    private float Data_z;
    public int PlotMode;
    public GameObject Left_Box;
    public GameObject Right_Box;
    public GameObject Error;
    // Start is called before the first frame update
    void Start()
    {
        PlotMode = 0;
        Data_x = 0;
        Data_y = 0;
        Data_z = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlotMode == 1)
        {
            Data_x = Left_Box.transform.rotation.eulerAngles.x;
            Data_y = Left_Box.transform.rotation.eulerAngles.y;
            Data_z = Left_Box.transform.rotation.eulerAngles.z;
        }
        else if(PlotMode == 2)
        {
            Data_x = Right_Box.transform.rotation.eulerAngles.x;
            Data_y = Right_Box.transform.rotation.eulerAngles.y;
            Data_z = Right_Box.transform.rotation.eulerAngles.z;
        }
        else if(PlotMode == 3)
        {
            Data_x = Error.GetComponent<Error>().errorvector.x;
            Data_y = Error.GetComponent<Error>().errorvector.y;
            Data_z = Error.GetComponent<Error>().errorvector.z;
        }
        else
        {
            Data_x = 0;
            Data_y = 0;
            Data_z = 0;
        }
    }
    public void Set_Mode1()
    {
        PlotMode = 1;
    }
    public void Set_Mode2()
    {
        PlotMode = 2;
    }
    public void Set_Mode3()
    {
        PlotMode = 3;
    }
    public void Reset_Mode()
    {
        PlotMode = 0;
    }
}
