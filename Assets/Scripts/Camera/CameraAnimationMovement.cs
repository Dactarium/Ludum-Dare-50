using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimationMovement : MonoBehaviour
{
    [Range(0f, 1f)] [SerializeField] private float _Progress;

    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _targetPosition;

    void Awake(){
        _targetPosition.y = PlayerPrefs.GetFloat("Zoom");
    }

    void Update(){

        transform.localPosition =  _Progress * (_targetPosition - _startPosition) + _startPosition;
    }
}
