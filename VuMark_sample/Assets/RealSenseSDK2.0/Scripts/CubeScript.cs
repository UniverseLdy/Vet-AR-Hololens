using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Intel.RealSense;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public GameObject cam;
    RsPoseStreamTransformer.RsPose pose = new RsPoseStreamTransformer.RsPose();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (RsPoseStreamTransformer.q != null)
        {
            PoseFrame frame;
            if (RsPoseStreamTransformer.q.PollForFrame<PoseFrame>(out frame))
                using (frame)
                {
                    frame.CopyTo(pose);

                    var t = pose.translation;
                    t.Set(t.x, t.y, -t.z);

                    var e = pose.rotation.eulerAngles;
                    var r = Quaternion.Euler(-e.x, -e.y, e.z);

                    transform.localRotation = r;
                    transform.localPosition = t;
                }
        }
    }
}
