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

    private float zoom;
    void Start(){
        _inputManager = InputManager.Instance;
        _camXZ = _camera.transform.localPosition;
        _camXZ.y = 0;

        zoom = PlayerPrefs.GetFloat("Zoom", 15f);
        AddZoom(0);
    }

    void LateUpdate(){
        if(_inputManager.Scroll == 0) return;

        AddZoom(-_inputManager.Scroll);
    }

    public void AddZoom(float amount){
        zoom += amount;

        if(zoom < _minDistance) zoom = _minDistance;
        else if(zoom > _maxDistance) zoom = _maxDistance;
        
        PlayerPrefs.SetFloat("Zoom", zoom);
        _camera.transform.localPosition = _camXZ + Vector3.up * zoom;
        _camera.transform.LookAt(transform);
    }
}
