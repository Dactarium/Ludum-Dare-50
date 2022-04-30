using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonHover : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler{
    
    [SerializeField] private Color hoverColor;
    [SerializeField] private TextMeshProUGUI _text;
    private Color _default;

    void Awake (){
        _default = _text.color;
    }

    void OnDisable(){
        _text.color = _default;
    }

    public void OnSelect (BaseEventData eventData)
    {
        _text.color = hoverColor;
    }

    public void OnDeselect (BaseEventData eventData)
    {
        _text.color = _default;
    }

    public void OnPointerEnter (PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

 }
