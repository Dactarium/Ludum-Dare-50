using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 _offset;
    void Update(){
        transform.position = target.position + _offset;
    }
}
