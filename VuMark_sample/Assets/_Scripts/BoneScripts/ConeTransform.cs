using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConeTransform : MonoBehaviour
{

    public GameObject plane;
    public Plane p;
    public Slider slider;
    float sliderValue;
    private MeshRenderer meshRenderer;

    // Use this for initialization
    void Start()
    {
        /*
        plane = GameObject.Find("Plane");
    //    slider = GameObject.Find("Canvas/Cone Slider");
        meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
      //  slider = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Slider>();
        if (GameObject.FindGameObjectWithTag("slider"))
        {
            slider = (Slider)FindObjectOfType(typeof(Slider));
        }
        */
    }


    // Update is called once per frame
    void Update()
    {
        /*
        //Create a ray from the Mouse click position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Initialise the enter variable

        RaycastHit enter;
        if (Physics.Raycast(ray.origin, ray.direction, out enter))
        {
            Debug.Log("normal " + enter.normal);
        }

        Vector3 normal = plane.GetComponentInChildren<MeshFilter>().mesh.normals[0];
        normal = plane.transform.rotation * normal;
        Debug.Log("normal " + normal);
        Debug.Log("rotation " + plane.transform.rotation);
        //  meshRenderer.transform.rotation = Quaternion.FromToRotation(meshRenderer.transform.up, plane.normal);
        //  transform.rotation = Quaternion.FromToRotation(transform.up, plane.normal);
        // transform.up = plane.normal;
        // Debug.Log("normal" + plane.normal);
        */
    }

    public void OnSliderChanged()
    {
        /*
        // widen the cone based on the slider value
        sliderValue = slider.value;

        transform.localScale = new Vector3(sliderValue, 1, sliderValue);
        */
    }
}
