using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] private float _moveSpeedMultiplier;
    [SerializeField] private float _instability;
    [SerializeField] private float _damage;

    private Rigidbody _rigidbody;
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

    private float _lifespan;
    private bool _readyForAttack = true;
    
    void Awake(){
        _rigidbody = GetComponent<Rigidbody>();
    }

    IEnumerator Start(){
        _lifespan = ConfigManager.Instance.CollectingSoulTooSoon_TimeGain * 10f;
        _baseMoveSpeed = PlayerController.Instance.BaseMoveSpeed;

        yield return new WaitForSeconds(1f);

        _target = PlayerController.Instance.transform;
    }

    void Update(){
        if(!_target) return;
        SpendLifeSpan(Time.deltaTime);
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
        SpendLifeSpan(_damage * 2f);

        yield return new WaitForSeconds(1);

        _readyForAttack = true;
    }

    private void SpendLifeSpan(float amount){
        _lifespan -= Time.deltaTime;

        if(_lifespan > 0) return;

        Destroy(gameObject);
    }
}
