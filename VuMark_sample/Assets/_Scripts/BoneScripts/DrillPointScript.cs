using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrillPointScript : MonoBehaviour {

    public List<Transform> coneList;
    public List<Transform> markerList;
    private GameObject bone;

    public GameObject planeGenerator;

    public Dropdown placementEnabled;
    private MeshRenderer meshRenderer;
    // prefab of marker to place on bone
    public GameObject drillMarkers;
    public GameObject cones;
    // bool determining if mouse has already been down to avoid
    // placing more than one marker at a given spot
    bool alreadyClicked;
    int numPlanes;

    private Quaternion coneRotation;

	// Use this for initialization
	void Start () {
        bone = GameObject.Find("Bone");

        numPlanes = 0;
        coneList = new System.Collections.Generic.List<Transform>();
        markerList = new System.Collections.Generic.List<Transform>();

        alreadyClicked = false;
        meshRenderer = this.GetComponentInChildren<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // if mouse is clicked 
        if(Input.GetMouseButton(0) && placementEnabled.value == 2)
        {
            if(planeGenerator.GetComponent<PlaneGenerator>().planes.Count != 0)
            {
                GameObject currPlane = planeGenerator.GetComponent<PlaneGenerator>().planes[0];
                Vector3 rotation = currPlane.transform.InverseTransformDirection(new Vector3(0, 0, 0));
                coneRotation = currPlane.transform.rotation;
            }           
            RaycastHit hitInfo;
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                // only drop markers on the bone
                if (hitInfo.collider.CompareTag("Bone") && !alreadyClicked)
                {
                    GameObject newMarker = Instantiate(drillMarkers, hitInfo.point,
                        Quaternion.FromToRotation(Vector3.up, hitInfo.normal)) as GameObject;

                    GameObject newCone = Instantiate(cones, hitInfo.point, coneRotation) as GameObject;
                    Debug.Log(newCone.transform.localScale);
                   newCone.transform.parent = bone.transform;

                    Debug.Log(newCone.transform.localScale);
                    coneList.Add(newCone.transform);
                    markerList.Add(newMarker.transform);
                    // set the cone and marker up as a pair
                    newMarker.GetComponent<PairScript>().pair = newCone;
                    newCone.GetComponent<PairScript>().pair = newMarker;
                }
            }
            alreadyClicked = true;

        } else {
            // reset ability to place marker
            alreadyClicked = false;
        }  

        if(planeGenerator.GetComponent<PlaneGenerator>().planes.Count != numPlanes)
        {
            numPlanes = planeGenerator.GetComponent<PlaneGenerator>().planes.Count;
            if(numPlanes != 0)
            {
                fixConeRotation();
            }
        } 
	}

    void fixConeRotation()
    {
        Quaternion planeNor = planeGenerator.GetComponent<PlaneGenerator>().planes[0].transform.rotation;
        foreach (Transform t in coneList)
        {
             t.rotation = planeNor;
        }
    }
}
