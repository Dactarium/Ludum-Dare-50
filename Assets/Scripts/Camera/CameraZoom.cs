using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private GameObject _camera;

    [SerializeField] private float _minDistance;
    [SerializeField] private float _maxDistance;

    private InputManager _inputManager;

    private Vector3 _camXZ;
    void Start(){
        _inputManager = InputManager.Instance;
        _camXZ = _camera.transform.localPosition;
        _camXZ.y = 0;
    }

    void LateUpdate(){
        if(_inputManager.Scroll == 0) return;
        
        float scrollDelta = _inputManager.Scroll;
    
        _camera.transform.localPosition -= Vector3.up * scrollDelta;

        if(_camera.transform.localPosition.y < _minDistance){

            _camera.transform.localPosition = _camXZ + Vector3.up * _minDistance;

        }else if(_camera.transform.localPosition.y > _maxDistance){

            _camera.transform.localPosition = _camXZ + Vector3.up * _maxDistance;

        }

        _camera.transform.LookAt(transform);
    }
}
