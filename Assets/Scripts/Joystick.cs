using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Hecho por RickRock666: ... 29/12/2020...
//siganlo en su canal :D

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    public RectTransform handle;
    public RectTransform outerHandle;

    private Vector2 pointA;
    public Vector2 drag;
    [SerializeField]
    private Vector2 axis;
    public Vector2 Axis { get { return axis; } }

    public float radioOffset = 10;
    private float x;
    private float y;
    //la base donde se detectan los clicks
    private Image baseClickHandler;
    private Vector2 tempPosition;
    public bool onTouch = false;
    private int id;

    void Start()
    {
        baseClickHandler = baseClickHandler ? GetComponent<Image>() : gameObject.AddComponent(typeof(Image)) as Image;
        baseClickHandler.color = new Color(0, 0, 0, 0);
        tempPosition = outerHandle.position;
        handle.sizeDelta = outerHandle.sizeDelta / 2;
        baseClickHandler.rectTransform.offsetMax = new Vector2(-640, 0);
    }

    //el joystick toma punto de referencia
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!onTouch)
        {
            id = eventData.pointerId;
            pointA = eventData.position;
            outerHandle.transform.position = pointA;
            onTouch = true;
            baseClickHandler.enabled = false;
        }
    }
    //movemos con sus limitaciones al joystick
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 local = Vector2.zero;
        if (onTouch && RectTransformUtility.ScreenPointToLocalPointInRectangle
            (handle, eventData.position, eventData.pressEventCamera, out local) && eventData.pointerId == id)
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
        if (onTouch && eventData.pointerId == id)
        {
            axis = Vector2.zero;
            handle.transform.localPosition = Vector2.zero;
            outerHandle.position = tempPosition;
            onTouch = false;
            drag = Vector2.zero;
            baseClickHandler.enabled = true;
        }

    }


}