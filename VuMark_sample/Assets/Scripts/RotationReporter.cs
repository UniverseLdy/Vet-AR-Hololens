using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationReporter : MonoBehaviour
{
    public Vector3 rotation;
    // Start is called before the first frame update
    void Start()
    {
        rotation = this.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        rotation = this.transform.rotation.eulerAngles;
    }
}
