using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance {get; private set;}

    [SerializeField] private Vector3 RotationLimit;
    [SerializeField] private Vector3 PositionLimit;

    private float _trauma = 0;
    private float _shake => _trauma * _trauma;

    private ChromaticAberration _chromaticAberration;
    private float seed;
    void Awake(){
        Instance = this;

        seed = Random.Range(0, 100000);
    }

    void Update(){
        AddTrauma(-Time.deltaTime);
    }

    void LateUpdate(){
        Vector3 rotation = Vector3.zero;
        rotation.x = RotationLimit.x * _shake * PerlinNoiseNegOneToOne(seed);
        rotation.y = RotationLimit.y * _shake * PerlinNoiseNegOneToOne(seed + 1);
        rotation.z = RotationLimit.z * _shake * PerlinNoiseNegOneToOne(seed + 2);

        Vector3 position = Vector3.zero;
        position.x = PositionLimit.x * _shake * PerlinNoiseNegOneToOne(seed + 3);
        position.y = PositionLimit.y * _shake * PerlinNoiseNegOneToOne(seed + 4);
        position.z = PositionLimit.z * _shake * PerlinNoiseNegOneToOne(seed + 5);

        transform.localEulerAngles = rotation;
        transform.localPosition = position;

        if(!GameStateManager.Instance.GlobalVolume.profile.TryGet(out _chromaticAberration)) throw new System.Exception(nameof(_chromaticAberration));
        _chromaticAberration.intensity.Override(_trauma);
    }

    private float PerlinNoiseNegOneToOne(float seed){
        float noise = (Mathf.PerlinNoise(seed, Time.time) - 0.5f) / 0.5f;
        return noise;
    }

    public void AddTrauma(float trauma){
        _trauma += trauma;
        if(_trauma > 1) _trauma = 1;
        else if(_trauma < 0) _trauma = 0;
    }
}
