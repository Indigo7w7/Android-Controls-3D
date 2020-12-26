using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController player;
    CameraRotation cameraRotation;
    private Vector3 PlayerInput;
    [SerializeField]
    private float speed = 5;
    Joystick joystick;

    private void Start()
    {
        player = GetComponent<CharacterController>();
        cameraRotation = FindObjectOfType<CameraRotation>();
        joystick = FindObjectOfType<Joystick>();
    }

    private void Update()
    {
        movement();
    }

    private void movement()
    {
        PlayerInput = new Vector3(joystick.Axis.x, 0, joystick.Axis.y);
        player.Move(cameraRotation.playerView * speed * Time.deltaTime);
        PlayerInput = Vector3.ClampMagnitude(PlayerInput, 1);

        cameraRotation.CamDirection();
        cameraRotation.playerView = PlayerInput.x * cameraRotation.camRigth + PlayerInput.z * cameraRotation.camForward ;
        player.transform.LookAt(player.transform.position + cameraRotation.playerView);
    }
}
   

