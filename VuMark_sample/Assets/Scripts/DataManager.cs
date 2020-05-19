using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public Quaternion Inertia_Rotation;
    public Quaternion TrackIR_Rotation;
    public Vector3 TrackIR_Position;
    public Vector3 TrackIR_Offset;
    public Quaternion RealSense_Rotation;
    public Vector3 RealSense_Position;
    public Quaternion VuMark_Rotation;
    public Vector3 VuMark_Position;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void set_TrackIR(Quaternion Rotation,Vector3 Position)
    {

        TrackIR_Rotation = Rotation;
        TrackIR_Position = Position;
    }
    public void set_Iniertial(Quaternion Rotation)
    {
        Inertia_Rotation = Rotation;
    }
    public void set_VuMark(Quaternion Rotation,Vector3 Position)
    {
        VuMark_Position = Position;
        VuMark_Rotation = Rotation;
    }
    public void set_RealSense(Quaternion Rotation,Vector3 Postion)
    {
        RealSense_Position = Postion;
        RealSense_Rotation = Rotation;
    }
}
