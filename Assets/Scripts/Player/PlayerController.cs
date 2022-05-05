using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private float _minMoveSpeed;
    [SerializeField] private float _baseMoveSpeed;
    private float _moveSpeed;
    private float _targetMoveSpeed;
    private readonly  float _gravityValue = -9.81f;

    private InputManager _inputManager;
    private CharacterController _characterController;
    private PlayerAnimationManager _animationManager;

    [Header("Weapon Attack")]
    [SerializeField] private Reaper _reaper;
    public bool OnAttack{
        set{
            if(value)_reaper.GetComponent<AudioSource>().Play();
        }
    }

    void Awake(){
        _moveSpeed = _baseMoveSpeed;
        _targetMoveSpeed = _moveSpeed;
    }

    void Start()
    {
        name = "You";
        _inputManager = InputManager.Instance;
        _characterController = GetComponent<CharacterController>();
        _animationManager = GetComponent<PlayerAnimationManager>();
    }

    void Update()
    {
        if(_inputManager.Attack){
            Attack();
        }

       Move();
    }

    void LateUpdate(){
        if(transform.position.y < -10) transform.position = Vector3.up * .12f;
    }

    void Attack(){
        _animationManager.TriggerAttack();
    }

    void Move(){
        Vector2 movement = _inputManager.Movement;

        if(movement.magnitude <= 0f){
            _animationManager.IsRunning = false;
            return;
        }

        _animationManager.IsRunning = true;

        RotateDirection(movement);

        Vector3 motion = transform.forward * ((movement.magnitude > 1f)? 1: movement.magnitude) * _moveSpeed;
        motion.y = _gravityValue;
        motion *= Time.deltaTime;

        _characterController.Move(motion);
    }

    void RotateDirection(Vector2 direction){
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.eulerAngles = Vector3.up * angle;
    }

    public void StartLostBuff(LostBuff buff){
        StartCoroutine(ApplyBuff(buff));
    }

    IEnumerator ApplyBuff(LostBuff buff){
        float speedBuff = _baseMoveSpeed * buff.SpeedMultiplier - _baseMoveSpeed;

        speedBuff = (speedBuff + _targetMoveSpeed > _maxMoveSpeed)? _maxMoveSpeed - _targetMoveSpeed: (speedBuff + _targetMoveSpeed < _minMoveSpeed)? _minMoveSpeed - _targetMoveSpeed: speedBuff; 

        _targetMoveSpeed += speedBuff;
        for(int i = 0; i < 10 ; i++){
            yield return new WaitForSeconds(0.1f);
            _moveSpeed += speedBuff * 0.1f;
        }

        yield return new WaitForSeconds(buff.Duration);
        
        _targetMoveSpeed -= speedBuff;
        for(int i = 0; i < 10 ; i++){
            yield return new WaitForSeconds(0.1f);
            _moveSpeed -= speedBuff * 0.1f;
        }
        
    }
}
