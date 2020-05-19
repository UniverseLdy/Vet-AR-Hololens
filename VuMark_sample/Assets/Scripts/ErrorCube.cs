using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorCube : MonoBehaviour
{
    public GameObject error;
    public bool visible;
    // Start is called before the first frame update
    void Start()
    {
        visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 errorVector = error.GetComponent<Error>().errorvector;
        this.transform.rotation = Quaternion.Euler(errorVector);
        this.GetComponent<MeshRenderer>().enabled = visible;
    }
    public void mode_change()
    {
        visible = !visible;
    }
}
