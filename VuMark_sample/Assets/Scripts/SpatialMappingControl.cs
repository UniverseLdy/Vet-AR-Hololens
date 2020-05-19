using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

public class SpatialMappingControl : MonoBehaviour {
    bool status = true;
    protected SpatialMappingManager spatialMappingManager;
    // Use this for initialization
    void Start()
    {
        spatialMappingManager = SpatialMappingManager.Instance;
        if (spatialMappingManager == null)
        {
            Debug.LogError("This script expects that you have a SpatialMappingManager component in your scene.");

        }
    }

        // Update is called once per frame
        void Update () {
		
	}

    public void ChangeStatus() {
        status = !status;
        spatialMappingManager.DrawVisualMeshes = status;
    }
}
