using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class VumarkSensor1 : MonoBehaviour
{
    public bool isTracking;
    public bool Target_Detected;
    public bool first_time;
    public GameObject CoreCube;
    public GameObject VuMark;
    public Vector3 rotation;
    private VuMarkManager m_Manager;
    private VuMarkTarget Target;
    private Vector3 data;
    private Vector3 offset;
    private Vector3 last_pos, current_pos,last_rot,current_rot;
    public GameObject Cube;
    public bool Cube_Exist;
    private GameObject created;
    public GameObject DataManager;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 50;
        Target_Detected = false;
        first_time = true;
        m_Manager = TrackerManager.Instance.GetStateManager().GetVuMarkManager();
        m_Manager.RegisterVuMarkDetectedCallback(OnVuMarkDetected);
        m_Manager.RegisterVuMarkLostCallback(OnVuMarkLost);
        last_rot=last_pos = new Vector3(0, 0, 0);
        current_pos = current_rot = new Vector3(0, 0, 0);
    }
    void OnDestroy()
    {
        // unregister callbacks from VuMark Manager
        m_Manager.UnregisterVuMarkDetectedCallback(OnVuMarkDetected);
        m_Manager.UnregisterVuMarkLostCallback(OnVuMarkLost); //here we can probably remove the anchor, but I am not sure that is what we want.

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 current_rot = transform.rotation.eulerAngles;
        data = VuMark.transform.rotation.eulerAngles;
        if (Target_Detected)
        {
            if (first_time)
            {

                first_time = !first_time;
                ////foreach(var bhvr in m_Manager.GetActiveBehaviours())
                ////{
                ////    last_sensor = bhvr.transform.rotation.eulerAngles;
                ////}
                created = (GameObject)Instantiate(Cube);
                created.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                //created.GetComponent<MeshRenderer>().enabled = false;
                created.transform.parent = VuMark.transform;
                last_pos = created.transform.position;
                last_rot = created.transform.rotation.eulerAngles;
            }
            rotation = created.transform.rotation.eulerAngles;
            //foreach (var bhvr in m_Manager.GetActiveBehaviours())
            //{
            //    current_sensor = bhvr.transform.rotation.eulerAngles;
            //}
            //offset = current_sensor - last_sensor;
            //if (offset.y < 0.1)
            //{
            //    offset.y = 0;
            //}
            //current_rot = created.transform.rotation.eulerAngles;
            //current_pos = created.transform.position;
            //Vector3 error_rot = current_rot - last_rot;
            //Vector3 error_pos = current_pos - last_pos;
            //DataManager.GetComponent<DataManager>().set_VuMark(error_rot, error_pos);
            DataManager.GetComponent<DataManager>().set_VuMark(created.transform.rotation, created.transform.position);
            //transform.rotation = Quaternion.Euler(data - offset + makeup);
            //transform.rotation = Quaternion.Euler(current_rot + new Vector3 (0, offset.y,0));
            //last_sensor = current_sensor;
            last_pos = current_pos;
            last_rot = current_rot;
            //transform.rotation = created.transform.rotation;
        }

    }
    public void OnVuMarkDetected(VuMarkTarget target)
    {
        Debug.Log("Detected!!!!!!!!!!!!!!!");
        Target_Detected = true;
        Target = target;
        if (!Cube_Exist)
        {

        }
    }
    public void OnVuMarkLost(VuMarkTarget target)
    {
        Debug.Log("Lost!!!!!!!!!!!!!!!");
        Target_Detected = false;
    }
    public void Reset_Created()
    {
        if (created != null)
        {
            created.transform.parent = null;
            created.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            created.transform.parent = VuMark.transform;
        }
    }
}
