using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    public Transform Target;

    [SerializeField] private Vector3 _offset;
    void Update(){
        if(Target) transform.position = Target.position + Target.TransformDirection(_offset);
    }
}
