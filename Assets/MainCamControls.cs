using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamControls : MonoBehaviour
{
    // Start is called before the first frame update
    float initialFOV;
    Camera myCam;
    void Start()
    {
        myCam = gameObject.GetComponent<Camera>();
        initialFOV = myCam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        SetZoomBasedOnMouse();
    }

    void SetZoomBasedOnMouse()
    {
        myCam.fieldOfView += Input.mouseScrollDelta.y;
    }
}
