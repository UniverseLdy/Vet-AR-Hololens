using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorManager : MonoBehaviour
{
    public GameObject Inertial;
    public GameObject VuMark;
    public GameObject TrackIR;
    public GameObject RealSense;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Reset()
    {
        //VuMark.GetComponent<VumarkSensor1>().Reset_Created();
        TrackIR.GetComponent<TrackIRComponent>().Reset_Created();
        //Inertial.GetComponent<InertialSensor>().Reset_Created();
        RealSense.GetComponent<RealSense>().Reset_RealSense();
    }
}
