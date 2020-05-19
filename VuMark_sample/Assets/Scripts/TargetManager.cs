using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public GameObject target1;
    public GameObject target2;
    public GameObject DataManager;
    private Vector3 target1_pos, target2_pos, target1_offset,target2_offset;
    private Quaternion target1_rot, target2_rot;
    public bool left_trackIR, left_realsense, left_VuMark, left_inertial, left_none;
    public bool right_trackIR, right_realsense, right_VuMark, right_inertial, right_none;
    public bool track_pos, track_rot;
    // Start is called before the first frame update
    void Start()
    {
        left_inertial = left_realsense = left_trackIR = left_VuMark = false;
        right_inertial = right_realsense = right_trackIR = right_VuMark = false;
        left_none = right_none = true;
        track_pos = false;
        track_rot = false;
        target1_offset = target1.transform.position;
        target2_offset = target2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(left_none)
        {
            target1_pos = new Vector3(0, 0, 0);
            target1_rot = new Quaternion(0, 0, 0,1);
        }
        if (left_trackIR)
        {
            target1_pos = DataManager.GetComponent<DataManager>().TrackIR_Position;
            target1_rot = DataManager.GetComponent<DataManager>().TrackIR_Rotation;
        }
        if (left_realsense)
        {
            target1_pos = DataManager.GetComponent<DataManager>().RealSense_Position;
            target1_rot = DataManager.GetComponent<DataManager>().RealSense_Rotation;
        }
        if(left_VuMark)
        {
            target1_pos = DataManager.GetComponent<DataManager>().VuMark_Position;
            target1_rot = DataManager.GetComponent<DataManager>().VuMark_Rotation;
        }
        if(left_inertial)
        {
            target1_pos = new Vector3(0, 0, 0);
            target1_rot = DataManager.GetComponent<DataManager>().Inertia_Rotation;

        }
        if (right_none)
        {
            target2_pos = new Vector3(0, 0, 0);
            target2_rot = new Quaternion(0, 0, 0,1);
        }
        if (right_trackIR)
        {
            target2_pos = DataManager.GetComponent<DataManager>().TrackIR_Position;
            target2_rot = DataManager.GetComponent<DataManager>().TrackIR_Rotation;
        }
        if (right_realsense)
        {
            target2_pos = DataManager.GetComponent<DataManager>().RealSense_Position;
            target2_rot = DataManager.GetComponent<DataManager>().RealSense_Rotation;
        }
        if (right_VuMark)
        {
            target2_pos = DataManager.GetComponent<DataManager>().VuMark_Position;
            target2_rot = DataManager.GetComponent<DataManager>().VuMark_Rotation;
        }
        if (right_inertial)
        {
            target2_pos = new Vector3(0, 0, 0);
            target2_rot = DataManager.GetComponent<DataManager>().Inertia_Rotation;

        }
        if (track_rot)
        {
           target1.transform.rotation = target1_rot;
           target2.transform.rotation = target2_rot;
        }
        if (track_pos)
        {
           target1.transform.localPosition = target1_pos+target1_offset;
           target2.transform.localPosition = target2_pos+target2_offset;
        }
    }
    public void left_None()
    {
        left_none = !left_none;
    }
    public void right_None()
    {
        right_none = !right_none;
    }
    public void set_left_TrackIR()
    {
        left_trackIR = !left_trackIR;
    }
    public void set_right_TrackIR()
    {
        right_trackIR = !right_trackIR;
    }
    public void set_left_RealSense()
    {
        left_realsense = !left_realsense;
    }
    public void set_right_RealSense()
    {
        right_realsense = !right_realsense;
    }
    public void set_left_VuMark()
    {
        left_VuMark = !left_VuMark;
    }
    public void set_right_VuMark()
    {
        right_VuMark = !right_VuMark;
    }
    public void set_left_Inertia()
    {
        left_inertial = !left_inertial;
    }
    public void set_right_Inertia()
    {
        right_inertial = !right_inertial;
    }
    public void track_position()
    {
        track_pos = !track_pos;
    }
    public void track_rotation()
    {
        track_rot = !track_rot;
    }
}
