using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MarkerSelect : MonoBehaviour {

    // the currently selected game object pair
    private GameObject currCone;
    private GameObject currMarker;

    public Material originalMarkerMat;
    public Material selectedMarkerMat;
    public Material originalConeMat;
    public Material selectedConeMat;

    public Dropdown drillPointMode;
    public Slider coneSlider;
    public Slider heightSlider;

    public GameObject DrillPoints;
    private DrillPointScript drillPoints;

    private Gizmo gizmo;
    private bool alreadyClicked;

	// Use this for initialization
	void Start () {
        alreadyClicked = false;
        gizmo = GameObject.Find("Gizmo").GetComponent<Gizmo>();

        drillPoints = DrillPoints.GetComponent<DrillPointScript>();

        coneSlider.onValueChanged.AddListener(delegate { OnSliderChanged(); });
        heightSlider.onValueChanged.AddListener(delegate { OnHeightSliderChanged(); });
    }
	
	// Update is called once per frame
	void Update () {
        //Create a ray from the Mouse click position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // drag marker/cone 
        if (alreadyClicked && currCone != null)
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray.origin, ray.direction, 1000.0F);

            for(int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                if(hit.collider.CompareTag("Bone"))
                {
                    if(currCone != null && currMarker != null)
                    {
                        currMarker.transform.position = hit.point;
                        currMarker.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                        currCone.transform.position = hit.point;
                    }
                    break;
                }
            }
            alreadyClicked = false;
        } 
        // if non-UI element is clicked, clear the selection
        else if (Input.GetMouseButton(0) && !(drillPointMode.value == 2) && 
            !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit enter;
            if (Physics.Raycast(ray, out enter))
            {
                // handle selection events
                onConeClicked(enter);
                onMarkerClicked(enter);
            } else 
            { // hit nothing, reset everything
                changeColor(originalConeMat, originalMarkerMat);
                currCone = null;
                currMarker = null;
             //   gizmo.ClearSelection();
            }
            alreadyClicked = true;
        } else { // mouse not down
            alreadyClicked = false;
        }

        // change selection to highlight colors
        changeColor(selectedConeMat, selectedMarkerMat);

        // code for deleting markers
        if(Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace))
        {
            if(currCone != null)
            {
                drillPoints.coneList.Remove(currCone.transform);
                drillPoints.markerList.Remove(currMarker.transform);

                Destroy(currCone);
                Destroy(currMarker);
                currCone = null;
                currMarker = null;
            }
        }

        if((drillPointMode.value == 1))
        {
            changeColor(originalConeMat, originalMarkerMat);
            currCone = null;
            currMarker = null;
        }

    }

    void onConeClicked(RaycastHit enter)
    {
        // if you hit a cone
        if (enter.collider.CompareTag("Cone"))
        {
            Debug.Log("Cone");
            // if you're not already clicked (prohibit drag selecting)
            if (!alreadyClicked)
            {
                changeColor(originalConeMat, originalMarkerMat);

                currCone = enter.collider.gameObject;
                currMarker = currCone.GetComponent<PairScript>().pair;
            }
        }     
    }

    void onMarkerClicked(RaycastHit enter)
    {
        // if you hit a cone
        if (enter.collider.CompareTag("Marker"))
        {
            Debug.Log("Marker");
            // if you're not already clicked (prohibit drag selecting)
            if (!alreadyClicked)
            {
                changeColor(originalConeMat, originalMarkerMat);

                currMarker = enter.collider.gameObject;
                currCone = currMarker.GetComponent<PairScript>().pair;
            }
        }
    }

    void OnSliderChanged()
    {
        if(currCone != null)
        {
            Vector3 currScale = currCone.transform.localScale;
            currCone.transform.localScale = new Vector3(coneSlider.value, currScale[1], coneSlider.value);
        }

    }

    void OnHeightSliderChanged()
    {
        if (currCone != null)
        {
            Vector3 currScale = currCone.transform.localScale;
            currCone.transform.localScale = new Vector3(currScale[0], heightSlider.value, currScale[2]);
        }

    }

    void changeColor(Material coneColor, Material markerColor)
    {
        if (currCone != null)
        {
            heightSlider.value = currCone.transform.localScale[1];
            coneSlider.value = currCone.transform.localScale[0];
            currCone.GetComponentInChildren<MeshRenderer>().material = coneColor;
        }

        if (currMarker != null)
        {
            currMarker.GetComponentInChildren<MeshRenderer>().material = markerColor;
        }
    }
}
