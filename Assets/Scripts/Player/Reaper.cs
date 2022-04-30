using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : MonoBehaviour
{
    public static Reaper Instance {get; private set;}

    public event Action<Reaper> OnHumanCollect;
    public event Action<Reaper> OnLostCollect;
    [SerializeField] private GameObject _bloodEffect;
    [SerializeField] private GameObject _sparkEffect;

    private int _activeCoroutineCount;
    private List<Collider> willDie = new List<Collider>();
    void Awake(){
        Instance = this;
    }

    void OnTriggerEnter(Collider other){
        if(!other.GetComponent<Npc>() || !(other.CompareTag("Human") || other.CompareTag("Lost"))) return;
        if(willDie.Contains(other)) return;
        willDie.Add(other);

        Npc npc = other.GetComponent<Npc>();
        float  integrity = 0;
        if(other.CompareTag("Human")){
            OnHumanCollect?.Invoke(this);
            KillEffect(_bloodEffect, other.ClosestPoint(transform.position));
            integrity = ConfigManager.Instance.Human_Integrity;
            CameraShake.Instance.AddTrauma(0.33f);
        }else{
            OnLostCollect?.Invoke(this);
            KillEffect(_sparkEffect, other.ClosestPoint(transform.position));
            integrity = ConfigManager.Instance.Lost_Integrity;
            CameraShake.Instance.AddTrauma(0.1f);
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
        willDie.RemoveAt(0);

        _activeCoroutineCount--;
        if(_activeCoroutineCount < 1) Time.timeScale = 1f;
    }

}
