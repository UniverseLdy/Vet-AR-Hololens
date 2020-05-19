using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealSense : MonoBehaviour
{
    public GameObject DataManager;
    public GameObject Pose;
    public bool First_Time;
    public Quaternion offset;
    // Start is called before the first frame update
    void Start()
    {
        First_Time = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Pose.GetComponent<RsPoseStreamTransformer>().detected)
        {

            if (First_Time)
            {
                offset = Pose.transform.rotation;
                First_Time = !First_Time;
            }
            this.transform.localPosition = Pose.transform.localPosition;
            this.transform.rotation = Pose.transform.rotation * Quaternion.Inverse(offset);
            DataManager.GetComponent<DataManager>().set_RealSense(this.transform.rotation, this.transform.localPosition);
        }
    }
    public void Reset_RealSense()
    {
        offset = Pose.transform.rotation;
    }
}
