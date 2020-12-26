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
    private float gravity = 150f;

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
        movement();
    }

    private void setGravity()
    {
        PlayerView.y = -gravity * Time.deltaTime;
    }

    private void movement()
    {
        PlayerInput = new Vector3(joystick.Axis.x, 0, joystick.Axis.y);
        player.Move(PlayerView * speed * Time.deltaTime);
        PlayerInput = Vector3.ClampMagnitude(PlayerInput, 1);

        cameraRotation.CamDirection();
        PlayerView = PlayerInput.x * cameraRotation.camRigth + PlayerInput.z * cameraRotation.camForward ;
        player.transform.LookAt(player.transform.position + PlayerView);
    }

   
}
   

