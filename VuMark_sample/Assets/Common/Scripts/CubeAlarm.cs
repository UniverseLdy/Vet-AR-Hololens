using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAlarm : MonoBehaviour
{
    private bool gamemode;
    public Vector3 errorvector;
    public GameObject ModeManager;
    public GameObject target1;
    public GameObject target2;
    public Material alarm;
    public Material normal;
    // Start is called before the first frame update
    void Start()
    {
        gamemode = ModeManager.GetComponent<ModeManager>().gamemode;
        errorvector = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(gamemode == false)
        {
            errorvector = new Vector3(target1.transform.rotation.eulerAngles.x - target2.transform.rotation.eulerAngles.x, target1.transform.rotation.eulerAngles.y - target2.transform.rotation.eulerAngles.y, target1.transform.rotation.eulerAngles.z - target2.transform.rotation.eulerAngles.z);
            if (errorvector.x * errorvector.x >= 1.0 || errorvector.y * errorvector.y >= 1.0 || errorvector.z * errorvector.z >= 1.0)
            {
                this.GetComponent<MeshRenderer>().material = alarm;
            }
            else{
                this.GetComponent<MeshRenderer>().material = normal;
            }
        }
        
    }
}
