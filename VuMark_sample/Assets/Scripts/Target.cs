using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Vector3 offset;
    public GameObject SensorManager;
    public bool visible;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;
        visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
            transform.Translate(-transform.position+offset);
            SensorManager.GetComponent<SensorManager>().Reset();
        }
        if (Input.GetKeyDown("w"))
        {
            transform.Translate(new Vector3(0, 0, 1));
        }
        this.GetComponent<MeshRenderer>().enabled = visible;
    }
    public void mode_change()
    {
        visible = !visible;
    }
}
