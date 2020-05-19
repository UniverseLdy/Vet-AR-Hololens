using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            transform.Translate(-transform.position+offset);
        }
        if (Input.GetKeyDown("w"))
        {
            transform.Translate(new Vector3(0, 0, 1));
        }
    }
}
