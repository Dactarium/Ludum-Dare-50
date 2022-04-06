using System;
using UnityEngine;

public class NpcDestroyer : MonoBehaviour
{
    public static NpcDestroyer Instance {get; private set;}

    [SerializeField] private GameObject _effect;

    public event Action<NpcDestroyer> OnTargetDestroy;

    void Awake(){
        Instance = this;
    }

    void OnTriggerEnter(Collider other){
        if(!other.GetComponent<Npc>()) return;

        other.GetComponent<Npc>().Kill();
        
        GameObject effect = Instantiate(_effect);
        effect.transform.position = other.ClosestPoint(transform.position);
        effect.transform.right = transform.forward;

        bool isTarget = other.CompareTag("Target");
        if(TargetManager.Instance.IsPlayer || isTarget){
            UIManager.Instance.DeathInfo = other.name + " died because of <color=#FF0000>" + DeathCausePicker.Instance.RandomCause + "</color>";

            OnTargetDestroy?.Invoke(this);

            if(isTarget) TargetManager.Instance.pickTarget();
        }
    }

    

}
