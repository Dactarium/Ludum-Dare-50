using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator _animator;

    [HideInInspector] public bool IsRunning{
        set{
            _isRunning = value;
            _animator.SetBool("IsRunning", _isRunning);
        }
        get{
            return _isRunning;
        }
    }
    private bool _isRunning = false;
    void Start(){
        _animator = GetComponent<Animator>();
    }

    public void TriggerAttack(){
        _animator.SetTrigger("IsAttack");
        IsRunning = false;
    }
}
