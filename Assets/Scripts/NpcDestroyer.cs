using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDestroyer : MonoBehaviour
{
    [SerializeField] private GameObject _effect;

    void OnTriggerEnter(Collider other){
        if(!other.CompareTag("Npc") && !other.CompareTag("Target")) return;

        if(other.CompareTag("Target"))OnTargetDestory(other.transform);
        
        GameObject effect = Instantiate(_effect);
        effect.transform.position = other.ClosestPoint(transform.position);
        effect.transform.right = transform.forward;

        Destroy(other.gameObject);
    }

    void OnTargetDestory(Transform target){
        TargetManager.Instance.pickTarget();
    }
}
