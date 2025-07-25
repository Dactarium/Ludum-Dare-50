using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    public static PlayerController Instance {get; private set;} 

    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private float _minMoveSpeed;
    [SerializeField] private float _baseMoveSpeed;
    
    public float BaseMoveSpeed => _baseMoveSpeed;

    private PlayerBuffEffectController _buffController;

    private float MoveSpeed{
        set{
            _buffController.SetActiveSpeedTrails(value > _baseMoveSpeed);
            _buffController.SetActiveSlowEffect(value < _baseMoveSpeed);
            _moveSpeed = value;
        }
        get{
            return _moveSpeed;
        }
    }

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
        Instance = this;

        _buffController = GetComponent<PlayerBuffEffectController>();

        MoveSpeed = _baseMoveSpeed;
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
        if(buff.SpeedMultiplier != 1)StartCoroutine(ApplySpeedBuff(buff));
    }

    IEnumerator ApplySpeedBuff(LostBuff buff){
        float speedBuff = _baseMoveSpeed * buff.SpeedMultiplier - _baseMoveSpeed;

        speedBuff = (speedBuff + _targetMoveSpeed > _maxMoveSpeed)? _maxMoveSpeed - _targetMoveSpeed: (speedBuff + _targetMoveSpeed < _minMoveSpeed)? _minMoveSpeed - _targetMoveSpeed: speedBuff; 

        _targetMoveSpeed += speedBuff;
        for(float i = 0; i < buff.InOutDelay ; i += .1f){
            yield return new WaitForSeconds(0.1f);
            MoveSpeed += speedBuff / buff.InOutDelay / 10f;
        }

        yield return new WaitForSeconds(buff.Duration);
        
        _targetMoveSpeed -= speedBuff;
        for(float i = 0; i < buff.InOutDelay; i += .1f){
            yield return new WaitForSeconds(0.1f);
            MoveSpeed -= speedBuff / buff.InOutDelay / 10f ;
        }
        
    }
}
