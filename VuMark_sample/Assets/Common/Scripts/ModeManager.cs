using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    public bool gamemode;
    public GameObject cube1;
    public GameObject cube2;
    public Material Mode2;
    public Material Mode1;
    // Start is called before the first frame update
    void Start()
    {
        gamemode = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            gamemode = !gamemode;
        }
        if(gamemode == true)
        {
            cube1.transform.position = new Vector3(-3, 1, 0);
            cube2.transform.position = new Vector3(3, 1, 0);
            cube2.GetComponent<MeshRenderer>().material = Mode1;
        }
        if(gamemode == false)
        {
            cube1.transform.position = new Vector3(0, 1, 0);
            cube2.transform.position = new Vector3(0, 1, 0);
        }
    }
}
