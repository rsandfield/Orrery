using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandle : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    RectTransform knob;
    float angle;
    [SerializeField]
    float maxSlide = 5;

    void Awake()
    {
        angle = GetComponent<RectTransform>().rotation.eulerAngles.z;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {

    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.delta.Rotate(angle);
        knob.anchoredPosition += new Vector2(0, delta.y);
        if(knob.anchoredPosition.y < 0) knob.anchoredPosition = Vector2.zero;
        if(knob.anchoredPosition.y > maxSlide) knob.anchoredPosition = new Vector2(0, maxSlide);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        knob.anchoredPosition = Vector2.zero;
    }
}
