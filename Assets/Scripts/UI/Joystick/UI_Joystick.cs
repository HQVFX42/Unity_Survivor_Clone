using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Joystick : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField]
    Image _background;

    [SerializeField]
    Image _handler;

    float _joystickRadius;
    Vector2 _touchPosition;
    Vector2 _moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        _joystickRadius = _background.gameObject.GetComponent<RectTransform>().sizeDelta.x * 0.6f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _background.transform.position = eventData.position;
        _handler.transform.position = eventData.position;
        _touchPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _handler.transform.position = _touchPosition;
        _moveDirection = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchDirection = eventData.position - _touchPosition;
        
        float moveDistance = Mathf.Min(touchDirection.magnitude, _joystickRadius);
        _moveDirection = touchDirection.normalized;
        Vector2 newPosition = _touchPosition + _moveDirection * moveDistance;
        _handler.transform.position = newPosition;
    }
}
