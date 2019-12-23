using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class control : MonoBehaviour, IVirtualButtonEventHandler { 
    private GameObject dra;
    void Start() {
        VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>(); 
        for (int i = 0; i < vbs.Length; ++i) vbs[i].RegisterEventHandler(this);
        dra = transform.Find("dragon").gameObject;
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb){
        switch (vb.VirtualButtonName){
            case "vb1":
                dra.transform.position = new Vector3(0.2f, 0f, 0f);
                break;
            case "vb2":
                dra.transform.position = new Vector3(-0.2f, 0f, 0f);
                break;
        }
    }
    public void OnButtonReleased(VirtualButtonBehaviour vb){
        switch (vb.VirtualButtonName){
            case "b1":
                break;
            case "b2":
                break;
        }
    }
}