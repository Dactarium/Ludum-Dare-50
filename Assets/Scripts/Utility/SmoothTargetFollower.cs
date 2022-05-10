using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothTargetFollower : MonoBehaviour
{
    public Transform Target;
    
    [SerializeField] private float _smoothSpeed;
    [SerializeField] private Vector3 _baseOffset;
    
    [SerializeField] private bool _forwardOffsetWhileMoving;
    [SerializeField] private float _forwardOffsetLength;

    [SerializeField] private bool _areaFollow;
    [SerializeField] private float _areaRange;

    void LateUpdate(){
        if(!Target) return;

        Vector3 forwardOffset = Vector3.zero;

        if(_areaFollow && Vector3.Distance(transform.position, Target.position) < _areaRange) return;

        if(_forwardOffsetWhileMoving) forwardOffset =  Target.forward * _forwardOffsetLength * InputManager.Instance.Movement.magnitude;

        Vector3 desiredPosition = Target.position + Target.TransformDirection(_baseOffset) + forwardOffset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        
        transform.position = smoothPosition;
    }
}
