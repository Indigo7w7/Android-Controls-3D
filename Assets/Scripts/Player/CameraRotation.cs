using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
  
    [SerializeField]
    private Camera MainCamera;
    [NonSerialized]
    public Vector3 camForward;
    [NonSerialized]
    public Vector3 camRigth;

     public void CamDirection()
    {
        camForward = MainCamera.transform.forward;
        camRigth = MainCamera.transform.right;

        camForward.y = 0;
        camRigth.y = 0;

        camForward = camForward.normalized;
        camRigth = camRigth.normalized;
    }

}
