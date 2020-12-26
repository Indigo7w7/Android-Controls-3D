using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    public RectTransform handle;
    public RectTransform outerHandle;

    private Vector2 pointA;
    private Vector2 drag;
    [SerializeField]
    private Vector2 axis;
    public Vector2 Axis { get { return axis; } }

    public float radioOffset = 10;
    private float x;
    private float y;

    private Image baseClickHandler;
    private Vector2 tempPosition;
    private bool onTouch;

    //la base donde se detectan los clicks
    void Start()
    {
        baseClickHandler = baseClickHandler ? GetComponent<Image>() : gameObject.AddComponent(typeof(Image)) as Image;
        baseClickHandler.color = new Color(0, 0, 0, 0);
        tempPosition = outerHandle.position;
    }
    //el joystick toma punto de referencia
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.position.x < Screen.width / 2)
        {
            pointA = eventData.position;
            handle.sizeDelta = outerHandle.sizeDelta / 2;
            outerHandle.transform.position = pointA;
            onTouch = true;
        }
    }

    //movemos con sus limitaciones al joystick
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.position.x < Screen.width / 2 && onTouch)
        {
            drag = eventData.position;
            Vector2 offset = drag - pointA;

            float radio = (outerHandle.sizeDelta.x - handle.sizeDelta.x + outerHandle.sizeDelta.y - handle.sizeDelta.y)
                / Screen.width + Screen.height / radioOffset;

            Vector2 direccion = Vector2.ClampMagnitude(offset, radio);

            x = (drag.x - pointA.x) / radio;
            x = Mathf.Clamp(x, -1, 1);

            y = (drag.y - pointA.y) / radio;
            y = Mathf.Clamp(y, -1, 1);

            axis = new Vector2(x, y);
            handle.transform.position = new Vector2(pointA.x + direccion.x, pointA.y + direccion.y);
        }
    }


    //reseteamos todo al inicio
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.position.x < Screen.width / 2)
        {
            axis = Vector2.zero;
            handle.transform.localPosition = Vector2.zero;
            outerHandle.position = tempPosition;
            onTouch = false;
        }

    }
}
