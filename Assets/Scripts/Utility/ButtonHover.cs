using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    
    [SerializeField] private Color hoverColor;
    private TextMeshProUGUI _text;
    private Color _default;

    void Start (){
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _default = _text.color;
    }

    void OnDisable(){
        _text.color = _default;
    }

    public void OnPointerEnter (PointerEventData eventData)
    {
        _text.color = hoverColor;
    }

    public void OnPointerExit (PointerEventData eventData)
    {
        _text.color = _default;
    }


 }
