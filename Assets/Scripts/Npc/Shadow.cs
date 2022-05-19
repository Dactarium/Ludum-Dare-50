using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] private float _moveSpeedMultiplier;
    [SerializeField] private float _instability;
    [SerializeField] private float _damage;

    [SerializeField] private GameObject _onDestroyEffect;

    private Transform _target;
    private Vector3 _direction => (_target.position - transform.position).normalized + _instabilityVector;
    private Vector3 _instabilityVector {
        get{
            Vector3 random = Random.insideUnitSphere;
            random.y = 0;
            return random * _instability;
        }
    }

    private float _baseMoveSpeed;
    private float _startLifespan;
    private float _lifeSpan;

    private float LifeSpan{
        get => _lifeSpan;
        set{
            _lifeSpan = value;
            _emissionModule.rateOverTime = _lifeSpan / _startLifespan * _startEmissionRate;

            if(_lifeSpan > 0) return;

            Destroy(gameObject);
            Instantiate<GameObject>(_onDestroyEffect, transform.position, transform.rotation);
        }
    }

    private float _startEmissionRate;
    private bool _readyForAttack = true;

    private Rigidbody _rigidbody;
    private ParticleSystem _particleSystem;
    private ParticleSystem.EmissionModule _emissionModule;
    
    void Awake(){
        _rigidbody = GetComponent<Rigidbody>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _emissionModule = _particleSystem.emission;
    }

    IEnumerator Start(){
        _startLifespan = ConfigManager.Instance.CollectingSoulTooSoon_TimeGain * 10f;
        _startEmissionRate = _particleSystem.emission.rateOverTime.constant;
        _lifeSpan = _startLifespan;
        _baseMoveSpeed = PlayerController.Instance.BaseMoveSpeed;

        yield return new WaitForSeconds(1f);

        _target = PlayerController.Instance.transform;
    }

    void Update(){
        if(!_target) return;
        LifeSpan -= Time.deltaTime;
        Move();
        if(_readyForAttack && Vector3.Distance(_target.position, transform.position) < 1f) StartCoroutine(AttackTarget());
    }

    private void Move(){
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.position += _direction * _moveSpeedMultiplier * Time.deltaTime * 10f;
    }

    private IEnumerator AttackTarget(){
        _readyForAttack = false;

        Timer.Instance.Add(-_damage);
        LifeSpan -= _damage * 2f;

        yield return new WaitForSeconds(1);

        _readyForAttack = true;
    }

    
}
