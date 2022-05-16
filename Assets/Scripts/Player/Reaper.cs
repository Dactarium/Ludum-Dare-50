using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : MonoBehaviour
{
    public static Reaper Instance {get; private set;}

    public event Action<Reaper, Transform> OnPersonCollect;
    public event Action<Reaper> OnLostCollect;
    [SerializeField] private GameObject _bloodEffect;
    [SerializeField] private GameObject _sparkEffect;

    private int _activeCoroutineCount;
    private List<Collider> _willDie = new List<Collider>();
    public LostBuff LastCollectedLostBuff{get; private set;}
    void Awake(){
        Instance = this;
    }

    void OnTriggerEnter(Collider other){
        if(!other.GetComponent<Npc>()) return;
        

        Npc npc = other.GetComponent<Npc>();

        if(npc.GetType() != typeof(Lost)){
            OnPersonCollect?.Invoke(this, other.transform);
            KillEffect(_bloodEffect, other.ClosestPoint(transform.position));
            CameraShake.Instance.AddTrauma(ConfigManager.Instance.Human_Trauma);
        }else{
            LastCollectedLostBuff = (npc as Lost).Buff;
            OnLostCollect?.Invoke(this);
            KillEffect(_sparkEffect, other.ClosestPoint(transform.position));
            CameraShake.Instance.AddTrauma(ConfigManager.Instance.Lost_Trauma);
            GetComponentInParent<PlayerController>().StartLostBuff(LastCollectedLostBuff);
        }
        
        npc.Kill();
    }

    void KillEffect(GameObject effect, Vector3 position){
        GameObject newEffect = Instantiate(effect);

        newEffect.transform.position = position;
        newEffect.transform.right = transform.forward;
    }

}
