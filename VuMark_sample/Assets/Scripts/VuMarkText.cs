using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VuMarkText : MonoBehaviour
{
    private Text text;
    public GameObject VumarkManager;
    private bool detected;
    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<Text>();
        detected = false;
    }

    // Update is called once per frame
    void Update()
    {
        detected = VumarkManager.GetComponent<VumarkSensor1>().Target_Detected;
        if (detected)
        {
            text.color = new Color32(0, 193, 34, 255);
        }
        if (!detected)
        {
            text.color = new Color32(50, 50, 50,255);
        }
    }
}
