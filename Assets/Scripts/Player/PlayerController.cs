using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController player;
    CameraRotation cameraRotation;
    Joystick joystick;


    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float gravity = 9.8f;
    private float fallVelocity;

    [SerializeField]
    private bool isOnSlope = false;
    private Vector3 hitNormal;
    [SerializeField]
    private float slideVelocity;
    [SerializeField]
    private float slopeForceDown;

    private Vector3 PlayerInput;
    private Vector3 PlayerView;


    private void Start()
    {
        player = GetComponent<CharacterController>();
        cameraRotation = FindObjectOfType<CameraRotation>();
        joystick = FindObjectOfType<Joystick>();

    }

    private void Update()
    {

        setGravity();
        slideDown();
        movement();
        
    }

    private void slideDown()
     {
         isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit;

         if (isOnSlope)
         {
             PlayerView.x += ((1f - hitNormal.y) * hitNormal.x) * slideVelocity;
             PlayerView.z += ((1f - hitNormal.z) * hitNormal.z) * slideVelocity;

             PlayerView.y += slopeForceDown;
         }
   }

     private void OnControllerColliderHit(ControllerColliderHit hit)
     {
         hitNormal = hit.normal;
     }

    private void setGravity()
     {
         if (player.isGrounded)
         {
             fallVelocity = -gravity * Time.deltaTime;
             PlayerView.y = fallVelocity;
         }

         else
         {
             fallVelocity -= gravity * Time.deltaTime;
             PlayerView.y = fallVelocity;
         }

     }
     

    private void movement()
    {
        PlayerInput = new Vector3(joystick.Axis.x, 0, joystick.Axis.y);
        player.Move(PlayerView * speed * Time.deltaTime);
        PlayerInput = Vector3.ClampMagnitude(PlayerInput, 1);

        cameraRotation.CamDirection();
        PlayerView = PlayerInput.x * cameraRotation.camRigth + PlayerInput.z * cameraRotation.camForward ;
        PlayerView = PlayerView * speed;
        player.transform.LookAt(player.transform.position + PlayerView);
    }

   
}
   

