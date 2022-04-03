using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    [SerializeField] private float _moveSpeed = 1f;

    private readonly  float _gravityValue = -9.81f;

    private InputManager _inputManager;
    private CharacterController _characterController;
    private PlayerAnimationManager _animationManager;

    [Header("Weapon Attack")]
    [SerializeField] private NpcDestroyer _weapon;
    [HideInInspector] public bool OnAttack = false; 
    void Start()
    {
        _inputManager = InputManager.Instance;
        _characterController = GetComponent<CharacterController>();
        _animationManager = GetComponent<PlayerAnimationManager>();
    }

    void Update()
    {
        if(_inputManager.Attack){
            Attack();
            return;
        }

        _weapon.GetComponent<BoxCollider>().enabled = OnAttack;

        if(!OnAttack) Move();
    }

    void LateUpdate(){
        if(transform.position.y < -10) transform.position = Vector3.up;
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
}
