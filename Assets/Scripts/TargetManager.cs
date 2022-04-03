using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TargetManager : MonoBehaviour
{   
    public static TargetManager Instance {get; private set;}
    [SerializeField] private NpcSpawner _spawner;
    [SerializeField] private GameObject _targetIcon;
    [HideInInspector] public Transform Target;

    void Awake(){
        Instance = this;
    }

    void Start()
    {
        pickTarget();
    }

    public void pickTarget(){
        Target = _spawner.GetRandomNpc.transform;
        Target.tag = "Target";

        GameObject iconParent = new GameObject("Icon Parent");
        GameObject icon = Instantiate(_targetIcon);

        Vector3 position = Target.position;
        position.y += 2f;
        iconParent.transform.position = position;

        iconParent.transform.parent = Target;
        icon.transform.parent = iconParent.transform;
    }
}
