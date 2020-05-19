using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public Vector3 Inertia_Rotation;
    public Vector3 TrackIR_Rotation;
    public Vector3 TrackIR_Position;
    public Vector3 RealSense_Rotation;
    public Vector3 RealSense_Position;
    public Vector3 VuMark_Rotation;
    public Vector3 VuMark_Position;
    // Start is called before the first frame update
    void Start()
    {
        TrackIR_Rotation = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void set_TrackIR(Vector3 Rotation,Vector3 Position)
    {

        TrackIR_Rotation = Rotation;
        TrackIR_Position = Position;
    }
}
