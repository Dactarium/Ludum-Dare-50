using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointer : MonoBehaviour
{   
    public static TargetPointer Instance {get; private set;}

    public Transform Target;
    
    void Awake()
    {
        Instance = this;
        Instance.gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        if(Target)PointTarget();
        else Unknown();
    }

    void PointTarget(){
        transform.LookAt(Target);
        transform.eulerAngles = Vector3.up * transform.eulerAngles.y;
    }

    void Unknown(){
        float angle = (transform.eulerAngles.y + Time.deltaTime * 360f) % 360;
        transform.eulerAngles = Vector3.up * angle;
    }

}
