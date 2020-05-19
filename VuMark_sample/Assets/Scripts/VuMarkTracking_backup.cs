using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System.IO;
using System.Text;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Windows;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

#if WINDOWS_UWP
using Windows.Storage;
using System.Threading.Tasks;
using System;
#endif


public class VuMarkTracking_backup : MonoBehaviour {
    VuMarkManager m_VuMarkManager;
    VuMarkTarget m_ClosestVuMark;
    VuMarkTarget m_CurrentVuMark;
    public GameObject cone;
    public GameObject plane;
    public GameObject bone_model;
    public GameObject contrast_plane;
    public GameObject drill;
    public GameObject drill_par;
    //two data to keep track of the VuMark data
    Vector3 mark_pos = Vector3.zero;
    Vector3 mark_rot = Vector3.zero;
    string path;
    bool isBone = true; //different test flag
    bool isHolo = true; //test when we in unity or in hololens
    private Vector3 wrong_data = new Vector3(-7, -7, -7);
    WorldAnchorManager wAnchorManager;
    //a counter that used to count the frame
    long counter = 0;
    //an update flag to keep track of whether we need to update the bone status
    bool update_flag = false;
    // Use this for initialization
    void Start()
    {
        // register callbacks to VuMark Manager
        m_VuMarkManager = TrackerManager.Instance.GetStateManager().GetVuMarkManager();
        m_VuMarkManager.RegisterVuMarkDetectedCallback(OnVuMarkDetected);
        m_VuMarkManager.RegisterVuMarkLostCallback(OnVuMarkLost);

        //register the world anchor manager
        wAnchorManager = WorldAnchorManager.Instance;
        //determine we use test.txt or test_bone.txt
        if (!isHolo)
        {
            if (isBone)
            {
                path = @"D:\Mark\Data\BoneData.txt".Replace('\\', System.IO.Path.DirectorySeparatorChar);
            }
            else
                path = @"D:\Mark\Data\Test.txt".Replace('\\', System.IO.Path.DirectorySeparatorChar);
        }
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateClosestTarget();
    }

    void OnDestroy()
    {
        // unregister callbacks from VuMark Manager
        m_VuMarkManager.UnregisterVuMarkDetectedCallback(OnVuMarkDetected);
        m_VuMarkManager.UnregisterVuMarkLostCallback(OnVuMarkLost); //here we can probably remove the anchor, but I am not sure that is what we want.

    }

    //two helper functions to get the type or id of a VuMark
    string GetVuMarkDataType(VuMarkTarget vumark)
    {
        switch (vumark.InstanceId.DataType)
        {
            case InstanceIdType.BYTES:
                return "Bytes";
            case InstanceIdType.STRING:
                return "String";
            case InstanceIdType.NUMERIC:
                return "Numeric";
        }
        return string.Empty;
    }

    string GetVuMarkId(VuMarkTarget vumark)
    {
        switch (vumark.InstanceId.DataType)
        {
            case InstanceIdType.BYTES:
                return vumark.InstanceId.HexStringValue;
            case InstanceIdType.STRING:
                return vumark.InstanceId.StringValue;
            case InstanceIdType.NUMERIC:
                return vumark.InstanceId.NumericValue.ToString();
        }
        return string.Empty;
    }

    #region PUBLIC_METHODS

    /// <summary>
    /// This method will be called whenever a new VuMark is detected
    /// </summary>
    public void OnVuMarkDetected(VuMarkTarget target)
    {
        Debug.Log("New VuMark: " + GetVuMarkId(target));
    }

    /// <summary>
    /// This method will be called whenever a tracked VuMark is lost
    /// </summary>
    /// add a feature to detect and remove the world anchor when we remove it
    public void OnVuMarkLost(VuMarkTarget target)
    {

        //Debug.Log("Lost VuMark: " + GetVuMarkId(target));
        //first remove the anchor of the bone, which is a child of the VuMark gameobject
        string vu_name = GetVuMarkId(target);
        GameObject bone;
        if (vu_name.Equals("Front"))
        {
            bone = GameObject.Find("bone_front");
        }
        else if (vu_name.Equals("Back_"))
        {
            bone = GameObject.Find("bone_back");
        }
        else if (vu_name.Equals("Left_"))//works
        {
            bone = GameObject.Find("bone_left");
        }
        else if (vu_name.Equals("Right"))
        {
            bone = GameObject.Find("bone_right");
        }
        else
        {
            Debug.Log("vu_name is not correct");
            bone = null;
        }
        if (bone != null)
        {
            wAnchorManager.RemoveAnchor(bone); //this function runs ascyn so we need to wait it for a second
            //try to directly remove the world anchor component
            Debug.Log("remove " + bone.name + " anchor here");
        }
    }
    public GameObject RemoveAnchor(VuMarkTarget target)
    {
        string vu_name = GetVuMarkId(target);
        GameObject bone;
        if (vu_name.Equals("Front"))
        {
            bone = GameObject.Find("bone_front");
        }
        else if (vu_name.Equals("Back_"))
        {
            bone = GameObject.Find("bone_back");
        }
        else if (vu_name.Equals("Left_"))//works
        {
            bone = GameObject.Find("bone_left");
        }
        else if (vu_name.Equals("Right"))
        {
            bone = GameObject.Find("bone_right");
        }
        else
        {
            Debug.Log("vu_name is not correct");
            bone = null;
        }
        if (bone != null)
        {
            wAnchorManager.RemoveAnchor(bone); //this function runs ascyn so we need to wait it for a second
            //try to directly remove the world anchor component
            Debug.Log("remove " + bone.name + " anchor here");
            return bone;
        }
        return null;
    }

    public void AttachAnchor(VuMarkTarget target)
    {
        string vu_name = GetVuMarkId(target);
        GameObject bone;
        if (vu_name.Equals("Front"))
        {
            bone = GameObject.Find("bone_front");
        }
        else if (vu_name.Equals("Back_"))
        {
            bone = GameObject.Find("bone_back");
        }
        else if (vu_name.Equals("Left_"))//works
        {
            bone = GameObject.Find("bone_left");
        }
        else if (vu_name.Equals("Right"))
        {
            bone = GameObject.Find("bone_right");
        }
        else
        {
            Debug.Log("vu_name is not correct");
            bone = null;
        }
        if (bone != null)
        {
            wAnchorManager.AttachAnchor(bone, bone.name);
            //try to directly remove the world anchor component
            Debug.Log("attach " + bone.name + " anchor here");
        }
    }
    #endregion // PUBLIC_METHODS

    //used to update the status of VuMark
    void UpdateClosestTarget()
    {
        int time_lag = 5;
        Camera cam = DigitalEyewearARController.Instance.PrimaryCamera ?? Camera.main;
        float closestDistance = Mathf.Infinity;
        foreach (var bhvr in m_VuMarkManager.GetActiveBehaviours())
        {
            Vector3 worldPosition = bhvr.transform.position;
            Vector3 camPosition = cam.transform.InverseTransformPoint(worldPosition);

            float distance = Vector3.Distance(Vector2.zero, camPosition);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                m_ClosestVuMark = bhvr.VuMarkTarget;
            }
        }

        //Mark modified from here
        //update the deviation only when the current Vumark is the closest one
        if (m_CurrentVuMark != null && m_CurrentVuMark == m_ClosestVuMark && counter % 60 == 0)
        {
            //Debug.Log(counter);
            foreach (var bhvr in m_VuMarkManager.GetActiveBehaviours())
            {
                if (bhvr.VuMarkTarget == m_CurrentVuMark)
                {
                    //Debug.Log("name of current vumark is " + bhvr.name);
                    //compute the distance and update the VuMark data
                    //Debug.Log("previous VuMark's position is (" + mark_pos[0] + ", " + mark_pos[1] + ", " + mark_pos[2] + ")");
                    //Debug.Log("current VuMark's position data is ( " + bhvr.transform.position.x + ", " + bhvr.transform.position.y + ", " + bhvr.transform.position.z + ")");
                    //Debug.Log("previous VuMark's rotation is (" + mark_rot.x + ", " + mark_rot.y + ", " + mark_rot.z + ")");
                    //Debug.Log("current VuMark's rotation data is ( " + bhvr.transform.rotation.eulerAngles.x + ", " + bhvr.transform.rotation.eulerAngles.y + ", " + bhvr.transform.rotation.eulerAngles.z + ")");
                    float Vu_distance = Vector3.Distance(mark_pos, bhvr.transform.position);
                    if (Vu_distance > 0.05)
                    {
                        //if the distance pass the threshold, we remove the world anchor and create a new object for it
                        GameObject bone = RemoveAnchor(m_CurrentVuMark); //remove the Vumark
                        update_flag = true;
                        //attach the new one to world anchor manager
                        //Debug.Log("the distance is " + Vu_distance);
                    }
                    if (update_flag != true)
                    {
                        //don't know why but the y and z angles are changing ridiculously large
                        //the second condition is to prevent minor change across 360 degrees
                        if (Mathf.Abs(mark_rot.y - bhvr.transform.rotation.eulerAngles.y) > 20 && Mathf.Abs(mark_rot.y - bhvr.transform.rotation.eulerAngles.y - 360) > 20)
                        {
                            GameObject bone = RemoveAnchor(m_CurrentVuMark); //remove the Vumark
                            update_flag = true;
                            Debug.Log("The difference of two rotations is: " + Mathf.Abs(mark_rot.x - bhvr.transform.rotation.eulerAngles.x) + ", " + Mathf.Abs(mark_rot.y - bhvr.transform.rotation.eulerAngles.y) + ", " + Mathf.Abs(mark_rot.z - bhvr.transform.rotation.eulerAngles.z));
                        }
                    }
                    //update the mark_pos
                    //mark_pos = bhvr.transform.position;
                    //mark_rot = bhvr.transform.rotation.eulerAngles;
                }
            }
        }

        //wait some times to let the world anchor manager remove the world anchor
        //then we create a new bone at the new position

        if (counter % 60 == time_lag && update_flag == true)
        {
            Debug.Log("Getting in Attach part");
            var temp = GetStringVuMarkDescription(m_CurrentVuMark); // Create a new one
            AttachAnchor(m_CurrentVuMark);
            update_flag = false;
        }



        //Mark modify done

        if (m_ClosestVuMark != null &&
            m_CurrentVuMark != m_ClosestVuMark)
        {
            var vuMarkId = GetVuMarkId(m_ClosestVuMark);
            var vuMarkDataType = GetVuMarkDataType(m_ClosestVuMark);
            var vuMarkReact = GetStringVuMarkDescription(m_ClosestVuMark);
            m_CurrentVuMark = m_ClosestVuMark;
        }
        if (counter < long.MaxValue)
            counter++;
        else
            counter = 0;
    }

    string GetStringVuMarkDescription(VuMarkTarget vumark)
    {
        string vuMarkId = GetVuMarkId(vumark);
        //Debug.Log("The VuMark we looks at is " + vuMarkId);
        //perfectly fine to use this method, but later we need to make only one model show up
        //only create the first one
        foreach (var VuMark_object in m_VuMarkManager.GetActiveBehaviours(vumark))
        {
            //update the mark_pos and mark_rot when we create the bone
            mark_pos = VuMark_object.transform.position;
            mark_rot = VuMark_object.transform.rotation.eulerAngles;
            //this is for testing in cylinder
            //GameObject created = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            //this is for testing in bone mode
            //we should move created and check whether the necessary data exist, if not, we don't create the object
            //the behaviour of current vumark
            //IEnumerable<VuMarkBehaviour> behavior = VuMarkManager.GetActiveBehaviours(vumark);
            if (VuMark_object != null)
            {
                string model_data;// the data of bone model, now is the cylinder.
                string plane_data;//the data of plane on the bone
                string[] cones_data;
                GameObject created;
                //if the file read in is successful
                if (LoadTextFile(vuMarkId, out model_data, out plane_data, out cones_data))
                {
                    if (isBone)
                    {
                        created = (GameObject)Instantiate(bone_model);
                    }
                    else
                    {
                        created = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    }
                    //assign to VuMark object
                    created.transform.parent = VuMark_object.transform;
                    //Debug.Log("the model data we have is " + model_data);
                    //Debug.Log("the plane data we have is " + plane_data);
                    Vector3 model_pos;
                    Vector3 model_rot;
                    Vector3 model_scale;
                    ParseRawData(model_data, out model_pos, out model_rot, out model_scale);
                    //for unknown reason, we need to add a 180 degree offset for y-aixs rotation of bone, and modify the y-axis also
                    //model_rot.y -= 180.0f;
                    //model_pos.z = model_pos.z * -1;
                    created.transform.localPosition = model_pos;
                    created.transform.localEulerAngles = model_rot;
                    created.transform.localScale = model_scale;
                    //set name for bone
                    if (vuMarkId.Equals("Front"))
                    {
                        created.name = "bone_front";
                    }
                    else if (vuMarkId.Equals("Back_"))
                    {
                        created.name = "bone_back";
                    }
                    else if (vuMarkId.Equals("Left_"))//works
                    {
                        created.name = "bone_left";
                    }
                    else if (vuMarkId.Equals("Right"))
                    {
                        created.name = "bone_right";
                    }
                    CreatePlane(plane_data, created);
                    CreateCones(cones_data, created);
                    //CreateContrast(VuMark_object);
                    //now we create a contrast plane

                    GameObject contrast = (GameObject)Instantiate(contrast_plane);
                    contrast.transform.parent = VuMark_object.transform;
                    contrast.transform.position = contrast.transform.parent.position;
                    Vector3 contrast_rot = new Vector3(0, 180, 0);
                    Vector3 contrast_scale = new Vector3(0.1f, 0.1f, 0.1f);
                    contrast.transform.localEulerAngles = contrast_rot;
                    contrast.transform.localScale = contrast_scale;
                    contrast.transform.localPosition = new Vector3(0, 0, 0);

                    //set only one child to be active
                    if (VuMark_object.transform.childCount > 0)
                    {
                        for (int index = 0; index < VuMark_object.transform.childCount - 2; index++)
                        {
                            //only the last child is active
                            if (VuMark_object.transform.GetChild(index).gameObject.active)
                            {
                                // VuMark_object.transform.GetChild(index).gameObject.SetActive(false);
                                //instead of set inactive we destroy it
                                if (VuMark_object.transform.GetChild(index).gameObject.transform.name == "drill")
                                {
                                    VuMark_object.transform.GetChild(index).gameObject.SetActive(true);
                                    continue;
                                }
                                Destroy(VuMark_object.transform.GetChild(index).gameObject);
                            }
                        }
                    }
                    //Debug.Log("created's Parent: " + created.transform.parent.name);
                }
                else
                {
                    Debug.Log("Fail to load the text");
                }
            }
            else
            {
                Debug.Log("Didn't find VuMark in Scene, The VuMark object is null");
            }
        }

        return string.Empty; // if VuMark DataType is byte or string
    }

    //this function only for test
    //the final version, take the vuMark ID, parse it and return appropriate data for using in modify model
    bool LoadTextFile(string vu_name, out string model_data, out string plane_data, out string[] cones_data)
    {
        //Debug.Log("We enter load Text file");
        //string file_name =@"D:\Data.txt"; //name should be same all the time
        //string path = 
        //int plane_index = 4; //currently, I place plane data after the four model offsets data, so its index is 4
        //int cones_index = plane_index + 1;
        //the path should be stored in class highest level.
        //string path = @"D:\Mark\Data\Test.txt".Replace('\\', System.IO.Path.DirectorySeparatorChar);
        string text;
        if (!isHolo)
        {
            text = System.IO.File.ReadAllText(path);
        }
        else
        {
            //we are in Hololens, use read data
            string filename;
            if (isBone)
            {
                filename = "BoneData.txt";
            }
            else
                filename = "Test.txt";

            //text = ReadString(filename);
            //new approach
            //text = OpenDataForRead(filename);\
            //another approach using Unityengine.Windows
            text = GetData(filename);
            //Debug.Log("The return text is " + text);
            //path = @"D:\Mark\Data\Test.txt".Replace('\\', System.IO.Path.DirectorySeparatorChar);
            //text = System.IO.File.ReadAllText(path);
        }
        //debug printing
        //Debug.Log(text);
        //if the file is not correct
        if (text == "")
        {
            model_data = "";
            plane_data = "";
            cones_data = null;
            return false;
        }
        //these three to parse the data from text file
        //string[] raw_data_list;
        //int[] data = new int[] { 0, 0, 0 }; //the length of array is fixed
        string[] lines = text.Split('\n');
        //first we need to know how many VuMark we have, assume the first line is mark_num
        int mark_num = Int32.Parse(lines[0]);
        //Debug.Log("mark num is " + mark_num);
        //check vu_name to assign appropriate true string value
        int vu_index;
        if (vu_name.Equals("Front"))
        {
            vu_index = 1;
        }
        else if (vu_name.Equals("Back_"))
        {
            vu_index = 2;
        }
        else if (vu_name.Equals("Left_"))//works
        {
            vu_index = 3;
        }
        else if (vu_name.Equals("Right"))
        {
            vu_index = 4;
        }
        else
            vu_index = -1;

        //If the vu_index larger than mark_num, it means that we don't want that Vumark, result as a load fail
        if (vu_index > mark_num)
            vu_index = -1;
        //model_data = "test";
        //plane_data = "test2";
        if (vu_index != -1)
            Debug.Log("the model data we will return is " + lines[vu_index]);
        else
        {
            model_data = "";
            plane_data = "";
            cones_data = null;
            Debug.Log("vuName is invalid");
            return false;
        }
        model_data = lines[vu_index];
        int plane_index = mark_num + 1; //the plane_index is right after the last model offset data lines
        plane_data = lines[plane_index];
        //Debug.Log("the plane data we will return is " + plane_data);
        //assume only the cone data left
        //int num_cone = lines.Length - cones_index;//not very flexible, backup
        int num_cone = Int32.Parse(lines[plane_index + 1]); // assume the next line is for number of cones
        //Debug.Log("We have " + num_cone + " cones");
        cones_data = new string[num_cone];
        int cones_index = plane_index + 2;
        for (int i = 0; i < num_cone; i++)
        {
            cones_data[i] = lines[cones_index + i];
            //Debug.Log("Cone " + i + " data is " + cones_data[i]);
        }
        return true;

    }

    void ParseRawData(string raw_data, out Vector3 pos, out Vector3 rot, out Vector3 scale)
    {
        int pos_index = 0, rot_index = 1, scale_index = 2;
        //Debug.Log("We are printing " + raw_data);
        string[] raw_data_type = raw_data.Split(';'); //seperate position, rotation and scale
        string position_raw = raw_data_type[pos_index];
        string rotation_raw = raw_data_type[rot_index];
        string scale_raw = raw_data_type[scale_index];
        float[] pos_data = new float[3];
        float[] rot_data = new float[3];
        float[] scale_data = new float[3];

        //put the value to each double array
        for (int i = pos_index; i <= scale_index; i++)
        {
            //Debug.Log("parse " + raw_data_type[i] + " now!");
            string[] temp = raw_data_type[i].Split(',');
            if (i == pos_index)
            {
                for (int j = 0; j < 3; j++)
                {
                    pos_data[j] = Single.Parse(temp[j]);
                }
                //Debug.Log("pos data is " + pos_data[0] + " " + pos_data[1] + " " + pos_data[2]);
            }
            else if (i == rot_index)
            {
                for (int j = 0; j < 3; j++)
                {
                    rot_data[j] = Single.Parse(temp[j]);
                }
                //Debug.Log("rot data is " + rot_data[0] + " " + rot_data[1] + " " + rot_data[2]);
            }
            else
            {
                for (int j = 0; j < 3; j++)
                {
                    scale_data[j] = Single.Parse(temp[j]);
                }
                //Debug.Log("scale data is " + scale_data[0] + " " + scale_data[1] + " " + scale_data[2]);
            }
        }
        pos = new Vector3(pos_data[0], pos_data[1], pos_data[2]);
        rot = new Vector3(rot_data[0], rot_data[1], rot_data[2]);
        //since the scale of VuMark is small, we automatically enlarge it by size of 2 -- can adjust here
        //scale = new Vector3(scale_data[0]*2, scale_data[1]*2, scale_data[2]*2);
        scale = new Vector3(scale_data[0], scale_data[1], scale_data[2]);
    }

    void CreatePlane(string plane_data, GameObject par)
    {
        GameObject cut_plane = (GameObject)Instantiate(plane);
        //assign to VuMark object
        cut_plane.transform.parent = par.transform;
        cut_plane.transform.position = cut_plane.transform.parent.position;
        Vector3 plane_pos;
        Vector3 plane_rot;
        Vector3 plane_scale;
        ParseRawData(plane_data, out plane_pos, out plane_rot, out plane_scale);
        cut_plane.transform.localPosition = plane_pos;
        cut_plane.transform.localEulerAngles = plane_rot;
        cut_plane.transform.localScale = plane_scale;
    }

    void CreateCones(string[] cones_data, GameObject par)
    {
        int counter = 1;
        foreach (var cone_data in cones_data)
        {
            GameObject this_cone = (GameObject)Instantiate(cone);
            this_cone.transform.parent = par.transform;
            this_cone.transform.position = this_cone.transform.parent.position;
            Vector3 cone_pos;
            Vector3 cone_rot;
            Vector3 cone_scale;
            ParseRawData(cone_data, out cone_pos, out cone_rot, out cone_scale);
            this_cone.transform.localPosition = cone_pos;
            this_cone.transform.localEulerAngles = cone_rot;
            this_cone.transform.localScale = cone_scale;
            this_cone.name = "Cone" + counter;
            if (counter == 1)
            {
                //GameObject cone1 = GameObject.Find("Cone1");
                //not the best solution, it will affect the performance after using the program for a long time
                //the extra bone model will be kept instead of deleting
                drill.transform.parent = this_cone.transform;
                drill.transform.localPosition = new Vector3(0, 0.8f, 0);
                drill.transform.localEulerAngles = new Vector3(0, 0, 0);
                //this_cone.GetComponent<coneControl>().find_drill = true;
                //this_cone.tag = "has_drill";
            }
            counter++;
        }
    }

    //if in hololens, we use a fixed path: LocalAppData\SOMEAPP\RoamingState
    //using Async not very appropriate, using GetData instead

    //work!!!!!!!!
    private string GetData(string fileName)
    {
        string text = "";
        string foldername = UnityEngine.Windows.Directory.roamingFolder;
        string file_path = foldername + "\\" + fileName;
        //Debug.Log("file path is " + file_path);
        byte[] data = System.IO.File.ReadAllBytes(file_path);
        text = Encoding.ASCII.GetString(data);
        return text;
    }


    private IEnumerator delete_bone(VuMarkTarget target)
    {
        //Debug.Log("Deleteing bone object here");

        yield return null;


        //Debug.Log("wake up here!");
        if (target == m_CurrentVuMark)
        {
            foreach (Transform child in transform)
            {
                //first remove the anchor of the bone, which is a child of the VuMark gameobject
                //Debug.Log("name is " + child.name);
                GameObject.Destroy(child.gameObject);
            }
        }
        //Debug.Log("job done!");
    }

}
