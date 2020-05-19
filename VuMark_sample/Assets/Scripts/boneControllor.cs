using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

public class boneControllor : MonoBehaviour,IInputClickHandler {
    GameObject cursor;
    string boneAnchorName; //give anchor a name
    protected SpatialMappingManager spatialMappingManager;
    WorldAnchorManager wAnchorManager;
    static bool front_isAnchored = false;
    static bool back_isAnchored = false;
    static bool right_isAnchored = false;
    static bool left_isAnchored = false; //probably we need static
    bool isAnchored;
    bool appear = true;
    int flag; //used to determine which isAnchored we will change
    // Use this for initialization
    void Start () {
        boneAnchorName = gameObject.name;
        wAnchorManager = WorldAnchorManager.Instance; //instantiate the w anchor manager here
        spatialMappingManager = SpatialMappingManager.Instance;
        cursor = GameObject.Find("BasicCursor");
        if (spatialMappingManager == null)
        {
            Debug.LogError("This script expects that you have a SpatialMappingManager component in your scene.");
        }

        if (wAnchorManager != null && spatialMappingManager != null)
        {
            //only if these two managers online we set the flag
            if (boneAnchorName == "bone_front")
                flag = 1;
            else if (boneAnchorName == "bone_back")
                flag = 2;
            else if (boneAnchorName == "bone_right")
                flag = 3;
            else if (boneAnchorName == "bone_left")
                flag = 4;
            else
                flag = -1; //it is invalid
        }
        else
        {
            //Destroy(this); //"this" is a reference to the specific mono behavior attached to the current game object
            Debug.Log("WorldanchorManager fails to load");
        }
        //initialize the world anchor
        wAnchorManager.AttachAnchor(gameObject, boneAnchorName); //attach anchor on bone model with name we defined above
        //spatialMappingManager.DrawVisualMeshes = false; //when bone shows up, we turn mesh drawing off
    }

    // Update is called once per frame
    void Update () {
		
	}

    void IInputClickHandler.OnInputClicked(InputClickedEventData eventData) {
        Debug.Log("I clicked one time!");
        //bool valid = true;
        //when we clicked the bone, the bone and fracture plane will disappear
        if (appear)
        {
            appear = !appear;
            for (int index = 0; index < transform.childCount; index++)
            {
                //only the last child is active
                if (transform.GetChild(index).gameObject.name == "smallBone:Mesh" || transform.GetChild(index).gameObject.name == "smallPlane(Clone)")
                {
                    transform.GetChild(index).gameObject.SetActive(false);
                }
            }

            //deactive the cursor
            cursor.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        }
        else {
            appear = !appear;
            for (int index = 0; index < transform.childCount; index++)
            {
                //only the last child is active
                if (transform.GetChild(index).gameObject.name == "smallBone:Mesh" || transform.GetChild(index).gameObject.name == "smallPlane(Clone)")
                {
                    transform.GetChild(index).gameObject.SetActive(true);
                }
            }
            cursor.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        }

        /*
        GameObject contrast;
        contrast = GameObject.Find("Contrast(Clone)");
        if (contrast != null)
        {
            if (contrast.activeSelf)
                contrast.SetActive(false);
            else
                contrast.SetActive(true);
        }
        else
            gameObject.SetActive(false); //if we cannot find the contrast plane, we set itself false to make it disappear
       */

        //logic here need to be modified, since we actually have four models, we need to keep track of each of them, not to mess thing up
        /*
        switch (flag) {
            case 1:
                front_isAnchored = !front_isAnchored;
                isAnchored = front_isAnchored;
                break;
            case 2:
                back_isAnchored = !back_isAnchored;
                isAnchored = back_isAnchored;
                break;
            case 3:
                right_isAnchored = !right_isAnchored;
                isAnchored = right_isAnchored;
                break;
            case 4:
                left_isAnchored = !left_isAnchored;
                isAnchored = left_isAnchored;
                break;
            default:
                Debug.Log("the bone name has some problems, check it");
                valid = false;
                break;

        }
        //Debug.Log("is being place true");
        // If the user is in placing mode, display the spatial mapping mesh.
        if (isAnchored && valid)
        {
            spatialMappingManager.DrawVisualMeshes = true;
            wAnchorManager.RemoveAnchor(gameObject); //since we need to move object, we need to remove anchor first
        }
        // If the user is not in placing mode, hide the spatial mapping mesh.
        else if (isAnchored == false && valid)
        {
            spatialMappingManager.DrawVisualMeshes = false;
            wAnchorManager.AttachAnchor(gameObject, boneAnchorName);
        }
        else {
            Debug.Log("bugs on setting the flag");
        }
        */
        
    }
}
