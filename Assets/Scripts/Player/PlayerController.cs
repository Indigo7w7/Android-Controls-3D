using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController player;
    private Vector3 PlayerInput;
    [SerializeField]
    private float speed = 5;
    Joystick joystick;

    private void Start()
    {
        player = GetComponent<CharacterController>();
        joystick = FindObjectOfType<Joystick>();
    }

    private void Update()
    {
        movement();
    }

    private void movement()
    {
        PlayerInput = new Vector3(joystick.Axis.x, 0, joystick.Axis.y);
        PlayerInput = Vector3.ClampMagnitude(PlayerInput, 1);
        player.Move(PlayerInput * speed * Time.deltaTime);
        Debug.Log(player.velocity.magnitude);
    }
}
   

