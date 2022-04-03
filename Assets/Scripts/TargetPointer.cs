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

    void Update()
    {
        PointTarget();
    }

    void PointTarget(){
        transform.LookAt(_targetManager.Target);
        transform.eulerAngles = Vector3.up * transform.eulerAngles.y;
    }

}
