using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{   
    public static TargetManager Instance {get; private set;}
    [SerializeField] private Transform _player;
    [SerializeField] private NpcSpawner _spawner;
    [SerializeField] private GameObject _targetIcon;
    [Range(0f, 1f)] [SerializeField] private float PlayerPercentage = 0.25f;

    public float InitialDistance {get; private set;}
    [HideInInspector] public Transform Target;
    [HideInInspector] public bool IsPlayer = false;
    void Awake(){
        Instance = this;
    }

    public void pickTarget(){

        IsPlayer = false;

        Transform newTarget;

        do{
            newTarget = (Random.Range(0f, 1f) > PlayerPercentage)? _spawner.GetRandomNpc.transform: _player;
        }while(Target == newTarget);

        Target = newTarget;

        UIManager.Instance.TargetName = Target.name;

        if(Target == _player){
            IsPlayer = true;
            UIManager.Instance.ShowSoulToDelay(true);
            InitialDistance = 150f;
            return;
        }

        InitialDistance = (Target.transform.position - _player.transform.position).magnitude;

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
