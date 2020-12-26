using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CaracterController : MonoBehaviour
{
    private CharacterController player;
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
        player.Move(new Vector3(joystick.Axis.x, 0, joystick.Axis.y) * speed * Time.deltaTime);
    }
}
   

