using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube_control : Photon.PunBehaviour, IPunObservable {
    private Vector3 test = new Vector3(0f, 0f, 0f);
    public float speed = 30;
    public GameObject data_source;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Rotate(Vector3.right * v);
        transform.Rotate(Vector3.up * h);

        if (Input.GetButtonDown("Fire1"))
        {
            //test.x += 0.1f;
        }

        if (test.x != 0)
        {
            transform.localScale += test;
            //Debug.Log("(" + test.x + ", " + test.y + ", " + test.z + ")");
            test.x = 0;
        }
        
        
        if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.Rotate(Vector3.left, speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.Rotate(Vector3.right, speed * Time.deltaTime);
        }
        

        if (Input.GetKey(KeyCode.W))
        {
            this.gameObject.transform.Rotate(Vector3.forward, speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.gameObject.transform.Rotate(Vector3.back, speed * Time.deltaTime);
        }
        */
        if (data_source != null)
        {
            Vector3 temp_angle = data_source.transform.eulerAngles;
            //temp_angle.z += 90;
            //reverse
            //temp_angle.x = -1 * temp_angle.x;
            //temp_angle.y = -1 * temp_angle.y;
            //temp_angle.z = -1 * temp_angle.z;
            //gameObject.transform.eulerAngles = temp_angle;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //we own this player: send the others our data
            Debug.Log("sending test data ( " + test.x + ", " + test.y + ", " + test.z + ")");
            stream.SendNext(test);
        }
        else
        {
            //network players, receive data
            this.test = (Vector3)stream.ReceiveNext();
            Debug.Log("receive test data ( " + test.x + ", " + test.y + ", " + test.z + ")");
        }
    }

}
