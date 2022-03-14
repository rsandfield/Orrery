using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SlideToggle : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Color slideColor = Color.gray;
    public Color knobColor = Color.white;
    public Transform slidingArea;
    public Transform knob;
    public UnityEvent onChange;
    public float magnitude = 0.1f;
    public float scrollFactor = 10f;
    public float value { get => knob.localPosition.x * magnitude; }
    Camera cam;
    float downAngle;
    Vector2 downPosition;

    void Awake()
    {
        cam = Camera.main;
    }

    void Reset()
    {
        slidingArea = transform.Find("Sliding Area").GetComponent<Transform>();
        knob = transform.Find("Sliding Area/Handle").GetComponent<Transform>();
    }

    void Update()
    {
        if(knob.transform.localPosition.x != 0) onChange.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        downAngle = Vector3.Angle(Camera.main.transform.up, transform.up);
        Debug.Log(transform.rotation.eulerAngles);
        Debug.Log(downAngle);
        downPosition = cam.WorldToScreenPoint(transform.position);
        ClickPosition(eventData);
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 relativePosition = ClickPosition(eventData);
        relativePosition.y = 0;
        float halfWidth = slidingArea.localScale.y;
        relativePosition.x = Mathf.Clamp(relativePosition.x, -halfWidth, halfWidth);
        knob.localPosition = relativePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        knob.localPosition = Vector3.zero;
    }

    Vector2 ClickPosition(PointerEventData eventData)
    {
        Vector2 vec = eventData.position - downPosition;
        vec = vec.Rotate(downAngle);
        vec /= (float) cam.pixelWidth / scrollFactor;
        return vec;
    }
}
