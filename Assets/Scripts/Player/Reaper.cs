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
        if(_willDie.Contains(other)) return;
        _willDie.Add(other);

        Npc npc = other.GetComponent<Npc>();
        float  integrity = 0;
        if(npc.GetType() != typeof(Lost)){
            OnPersonCollect?.Invoke(this, other.transform);
            KillEffect(_bloodEffect, other.ClosestPoint(transform.position));
            integrity = ConfigManager.Instance.Human_Integrity;
            CameraShake.Instance.AddTrauma(0.33f);
        }else{
            LastCollectedLostBuff = (npc as Lost).Buff;
            OnLostCollect?.Invoke(this);
            KillEffect(_sparkEffect, other.ClosestPoint(transform.position));
            integrity = ConfigManager.Instance.Lost_Integrity;
            CameraShake.Instance.AddTrauma(0.1f);
            GetComponentInParent<PlayerController>().StartLostBuff(LastCollectedLostBuff);
        }
        
        StartCoroutine(SlowMotionKill(npc,  integrity));
    }

    void KillEffect(GameObject effect, Vector3 position){
        GameObject newEffect = Instantiate(effect);

        newEffect.transform.position = position;
        newEffect.transform.right = transform.forward;
    }

    IEnumerator SlowMotionKill(Npc npc, float integrity){
        _activeCoroutineCount++;
        
        Time.timeScale = 1 - integrity;

        yield return new WaitForSecondsRealtime(0.07f);
        
        npc.Kill();
        _willDie.RemoveAt(0);

        _activeCoroutineCount--;
        if(_activeCoroutineCount < 1) Time.timeScale = 1f;
    }

}
