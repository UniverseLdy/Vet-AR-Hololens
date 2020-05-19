using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Rotation : MonoBehaviour
{
    public GameObject cube;
    private Text Text;
    //[UPyPlot.UPyPlotController.UPyProbe]
    //private float Left_x;
    //[UPyPlot.UPyPlotController.UPyProbe]
    //private float Left_y;
    //[UPyPlot.UPyPlotController.UPyProbe]
    //private float Left_z;
    // Start is called before the first frame update
    void Start()
    {
        Text = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = this.name+":\n"+cube.transform.rotation.eulerAngles.ToString();
        //Left_x = cube.transform.rotation.eulerAngles.x;
        //Left_y = cube.transform.rotation.eulerAngles.y;
        //Left_z = cube.transform.rotation.eulerAngles.z;
    }
}
