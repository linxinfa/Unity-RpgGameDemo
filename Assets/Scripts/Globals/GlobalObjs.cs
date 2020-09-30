using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalObjs : MonoBehaviour
{
    public static void Init()
    {
        var camObj = GameObject.Find("Main Camera");
        mainCam3D = camObj.GetComponent<Camera>();
        camObj = GameObject.Find("UICamera");
        mainCam2D = camObj.GetComponent<Camera>();
    }

    public static Camera mainCam3D;
    public static Camera mainCam2D;  
}
