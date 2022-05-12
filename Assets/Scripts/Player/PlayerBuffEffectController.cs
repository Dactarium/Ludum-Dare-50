using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffEffectController : MonoBehaviour
{
    [SerializeField] ParticleSystem[] _speedTrails;
    [SerializeField] ParticleSystem _slowEffect;


    public void SetActiveSpeedTrails(bool active){
        foreach(ParticleSystem speedTrail in _speedTrails){
            if(active)speedTrail.Play();
            else speedTrail.Stop();
        }
    }

    public void SetActiveSlowEffect(bool active){
        if(active)_slowEffect.Play();
        else _slowEffect.Stop();
    }
}
