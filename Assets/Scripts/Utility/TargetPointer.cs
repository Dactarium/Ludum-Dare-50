using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointer : MonoBehaviour
{   
    private TargetManager _targetManager;
    void Start()
    {
        _targetManager = TargetManager.Instance;
    }

    void LateUpdate()
    {
        PointTarget();
    }

    void PointTarget(){
        if(_targetManager.IsPlayer){
            Unknown();
        }
        transform.LookAt(_targetManager.Target);
        transform.eulerAngles = Vector3.up * transform.eulerAngles.y;
    }

    void Unknown(){
        float angle =  (transform.eulerAngles.y + Time.deltaTime * 360f) % 360;
        transform.eulerAngles = Vector3.up * angle;
    }

}
